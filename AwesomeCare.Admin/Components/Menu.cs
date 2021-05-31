using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Components
{
    public class Menu:ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
