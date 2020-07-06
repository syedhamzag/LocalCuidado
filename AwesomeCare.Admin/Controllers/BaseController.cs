using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AwesomeCare.Services.Services;

namespace AwesomeCare.Admin.Controllers
{
    public class BaseController : Controller
    {
        public const string cacheKey = "baserecord_key";
        public readonly IFileUpload _fileUpload;
        public BaseController(IFileUpload fileUpload)
        {
            _fileUpload = fileUpload;
        }


        public void SetOperationStatus(OperationStatus operationStatus)
        {
            TempData["OperationStatus"] = JsonConvert.SerializeObject(operationStatus);
           
        }


        public async Task<IActionResult> DownloadFile(string file)
        {
            var filestream = await _fileUpload.DownloadFile(file);
            filestream.Item1.Position = 0;
            return File(filestream.Item1, filestream.Item2);
        }
    }
}