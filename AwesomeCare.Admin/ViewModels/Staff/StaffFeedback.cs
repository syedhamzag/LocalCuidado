using AwesomeCare.DataTransferObject.DTOs.StaffRating;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Staff
{
    public class StaffFeedback: PostStaffRating
    {
        public StaffFeedback()
        {
            StaffRatings = new List<GetStaffRating>();
            RatingSelectList = new List<SelectListItem>
            {
                new SelectListItem("1","1"),
                new SelectListItem("2","2"),
                new SelectListItem("3","3"),
                new SelectListItem("4","4"),
                new SelectListItem("5","5")
            };
            ClientSelectLists = new List<SelectListItem>();
        }
      
        public List<GetStaffRating> StaffRatings { get; set; }
        public List<SelectListItem> RatingSelectList { get; set; }
        public List<SelectListItem> ClientSelectLists { get; set; }
    }
}
