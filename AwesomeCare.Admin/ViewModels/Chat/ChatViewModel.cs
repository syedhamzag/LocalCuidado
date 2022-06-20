using AwesomeCare.DataTransferObject.DTOs.Chat;
using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Chat
{
    public class ChatViewModel
    {
        public string ClientEmail { get; set; }
        public string StaffEmail { get; set; }
        public int ClientId { get; set; }
        public int StaffPersonalInfoId { get; set; }

        public GetStaffs GetStaff { get; set; }
        public List<GetClient> Clients { get; set; }
        public List<GetStaffs> Staffs { get; set; }
        public List<GetChat> Chat { get; set; }
        public List<GetClient> ChatList { get; set; }
        public List<GetStaffs> StaffChatList { get; set; }
        public List<GetClient> UnChatList { get; set; }
        public List<GetStaffs> StaffUnChatList { get; set; }
    }
}
