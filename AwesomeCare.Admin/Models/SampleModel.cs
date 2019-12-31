using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Models
{
    public class SampleModel
    {

        public SampleModel()
        {
            Items = new List<SelectListItem>
            {
                new SelectListItem{Text = "One",Value="1"},
                new SelectListItem{Text = "Two",Value="2"},
                new SelectListItem{Text = "Three",Value="3"},
                new SelectListItem{Text = "Four",Value="4"}
            };
        }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string SelectedItem { get; set; }
        public List<SelectListItem> Items { get; set; }
    }
}
