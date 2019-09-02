using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Client
{
    public class CreateClient: DataTransferObject.DTOs.Client.PostClient
    {
        public CreateClient()
        {
            Gender = new List<SelectListItem> {
                new SelectListItem("Male","Male"),
                new SelectListItem("Female","Female")
            };
            
        }
        #region DropDowns
        public IEnumerable<SelectListItem> Gender { get; set; }
        #endregion

    }

}
