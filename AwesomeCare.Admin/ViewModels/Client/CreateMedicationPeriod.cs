﻿using AwesomeCare.DataTransferObject.DTOs.ClientMedicationPeriod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Client
{
    public class CreateMedicationPeriod: PostClientMedicationPeriod
    {
        public bool IsSelected { get; set; } 
        public string RotaType { get; set; }
    }
}
