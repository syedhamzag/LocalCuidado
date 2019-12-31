using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels
{
    public class HomeViewModel
    {
        public IFormFile ClientImage { get; set; }
    }
}
