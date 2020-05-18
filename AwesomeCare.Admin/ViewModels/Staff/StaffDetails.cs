﻿using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Staff
{
    public class StaffDetails:GetStaffProfile
    {
        public StaffDetails()
        {
            Statuses = new List<SelectListItem>();
            var items = Enum.GetNames(typeof(StaffRegistrationEnum)).ToList();
            foreach (var item in items)
            {
                var selectListItem = new SelectListItem
                {
                    Text = item,
                    Value = ((int)Enum.Parse(typeof(StaffRegistrationEnum),item)).ToString()
                };
                Statuses.Add(selectListItem);
            }
        }
        [Required]
        [MaxLength(250)]
        public string Comment { get; set; }
        public List<SelectListItem> Statuses { get; set; }
    }
}
