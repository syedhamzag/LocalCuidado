using AwesomeCare.Admin.Services.Chat;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Chat;
using AwesomeCare.DataTransferObject.DTOs.Chat;
using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class ChatController : BaseController
    {
        private IChatService _chatService;
        private IStaffService _staffservice;
        private IClientService _clientService;

        public ChatController(IChatService chatService, IFileUpload fileUpload, IClientService clientService, IStaffService staffService) : base(fileUpload)
        {
            _chatService = chatService;
            _clientService = clientService;
            _staffservice = staffService;

        }
        public async Task<IActionResult> VideoCall(int clientId)
        {
            if (User.Identity.Name == null)
            {
                return RedirectToAction("Dashboard", "Dashboard");
            }
            var clients = await _clientService.GetClient(clientId);

            var model = new ChatViewModel();
            model.ClientEmail = clients.Email;           
            return View(model);
        }
        public async Task<IActionResult> StaffVideoCall(int staffId)
        {
            if (User.Identity.Name == null)
            {
                return RedirectToAction("Dashboard", "Dashboard");
            }
            var staff = await _staffservice.GetStaff(staffId);

            var model = new ChatViewModel();
            model.StaffEmail = staff.Email;
            return View(model);
        }
        public async Task<IActionResult> StaffAudioCall(int staffId)
        {
            if (User.Identity.Name == null)
            {
                return RedirectToAction("Dashboard", "Dashboard");
            }
            var staff = await _staffservice.GetStaff(staffId);

            var model = new ChatViewModel();
            model.StaffEmail = staff.Email;
            return View(model);
        }
        public async Task<ActionResult> Index(int clientId)
        {
            if (User.Identity.Name == null)
            {
                return RedirectToAction("Dashboard", "Dashboard");
            }
            var model = new ChatViewModel();
            var clients = await _clientService.GetClients();
            var staffs = await _staffservice.GetStaffs();
            
            model.StaffPersonalInfoId = staffs.Where(s => s.Email == User.Identity.Name).FirstOrDefault().StaffPersonalInfoId;
            model.GetStaff = staffs.Where(s => s.StaffPersonalInfoId == model.StaffPersonalInfoId).FirstOrDefault();
            model.Chat =  await _chatService.Get();
            model.Clients = await _clientService.GetClients();
            model.ClientId = clientId;

            return View(model);
        }
        public async Task<ActionResult> StaffChat(int staffId)
        {
            if (User.Identity.Name == null)
            {
                return RedirectToAction("Dashboard", "Dashboard");
            }
            var model = new ChatViewModel();
            var staffs = await _staffservice.GetStaffs();

            model.StaffPersonalInfoId = staffs.Where(s => s.Email == User.Identity.Name).FirstOrDefault().StaffPersonalInfoId;
            model.GetStaff = staffs.Where(s => s.StaffPersonalInfoId == model.StaffPersonalInfoId).FirstOrDefault();
            model.Chat = await _chatService.Get();
            model.Staffs = await _staffservice.GetStaffs();
            model.ClientId = staffId;

            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Notify(int senderId)
        {
            var clients = await _clientService.GetClients();
            var chats = await _chatService.Get();
            var noti = new Dictionary<string, GetClient>();
                var notilist = new List<GetChat>();
                var receiver = chats.Where(s => s.SenderId == senderId && s.ReceiverId != senderId && s.Type == "Unread")
                            .OrderByDescending(s => s.Dated)
                            .Select(s => s.ReceiverId).Distinct();
                foreach (var item in receiver)
                {
                    var notify = chats
                        .Where(s => s.SenderId == senderId && s.ReceiverId != senderId && s.Type == "Unread")
                        .OrderByDescending(s => s.Dated).FirstOrDefault(s => s.ReceiverId == item);
                    notilist.Add(notify);

                }
                foreach (var notif in notilist)
                {
                    var getClient = new GetClient
                    {
                        Firstname = clients.FirstOrDefault(s => s.ClientId == notif.ReceiverId)?.Firstname,
                        Middlename = clients.FirstOrDefault(s => s.ClientId == notif.ReceiverId)?.Middlename,
                        Surname = clients.FirstOrDefault(s => s.ClientId == notif.ReceiverId)?.Surname,
                        PassportFilePath = clients.FirstOrDefault(s => s.ClientId == notif.ReceiverId)?.PassportFilePath
                    };

                    if (notif.Message.Contains("^"))
                    {
                        getClient.GetChat.Message = notif.Message.Split('^')[0] == "image" ? "Image Sent" : "Document Sent";
                    }
                    else
                    {
                        getClient.GetChat.Message = notif.Message;
                    }
                    getClient.GetChat.Dated = notif.Dated;
                    noti.Add(notif.ReceiverId.ToString(), getClient);
                }

                return Json(noti);
        }

        [HttpPost]
        public async Task<ActionResult> Read(int senderId, int receiverId)
        {
            var chats = await _chatService.Get();
            var chatlist = chats.Where(s => s.SenderId == senderId && s.ReceiverId == receiverId && s.Type == "Unread");
            var readMsgs = new List<PutChat>();
            foreach (var item in chatlist)
            {
                var readMsg = new PutChat();
                readMsg.ChatId = item.ChatId;
                readMsg.Type = "Read";
                readMsgs.Add(readMsg);
            }
            var result = _chatService.Put(readMsgs);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> Upload(IList<IFormFile> files)
        {
            var list = new List<string>();
            for (var i = 0; i < files.Count; i++)
            {
                string extention = System.IO.Path.GetExtension(files[i].FileName);
                string folder = "chatimages";
                string filename = string.Concat(folder, "_Attachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, files[i].OpenReadStream());
                list.Add(path);
            }
            return Json(list.ToArray());
        }
        public async Task<HttpResponseMessage> Save(string receiver, string sender, string message, DateTime dated)
        {
            var chat = new PostChat
            {
                ChatId = 0,
                ReceiverId = int.Parse(receiver),
                SenderId = int.Parse(sender),
                Message = message,
                Dated = dated,
                Type = "Unread"
            };
            var result = await _chatService.Create(chat);
            return result;

        }
        public class ChatHub : Hub
        {
            public Task SendMessage(string recevier, string sender, string message)
            {
                return Clients.All.SendAsync("ReceiveMessage", recevier, sender, message,
                DateTime.Now.ToString(CultureInfo.InvariantCulture));
            }
        }
    }
}
