using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AwesomeCare.Admin.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
           
        }

      
      public  void SetOperationStatus(OperationStatus operationStatus)
        {
            TempData["OperationStatus"] = JsonConvert.SerializeObject(operationStatus);
        }

    }
}