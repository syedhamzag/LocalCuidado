using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AwesomeCare.Admin.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Dropbox.Api.Files;
using AwesomeCare.Admin.ViewModels;
using Dropbox.Api;

namespace AwesomeCare.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly DropboxClient _dropboxClient;
        public HomeController(DropboxClient dropboxClient)
        {
             _dropboxClient= dropboxClient;
        }
        public IActionResult Index()
        {

            var model = new HomeViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(HomeViewModel model)
        {
            
            string fileName = string.Concat(DateTime.Now.ToString("yyyyMMddHHmmss"), Path.GetExtension(model.ClientImage.FileName));
            string path = $"/ClientPassport/{fileName}";
            await _dropboxClient.Files.UploadAsync(path,body: model.ClientImage.OpenReadStream());

            //update
            //await dropbox.Files.UploadAsync(path, WriteMode.Overwrite.Instance, body: model.ClientImage.OpenReadStream());
            var sharedsettings = new Dropbox.Api.Sharing.SharedLinkSettings(Dropbox.Api.Sharing.RequestedVisibility.Public.Instance, audience: Dropbox.Api.Sharing.LinkAudience.Public.Instance);
            
            var link = await _dropboxClient.Sharing.CreateSharedLinkWithSettingsAsync(path, sharedsettings);
            string url = link.Url;
            return RedirectToAction("Index");
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
