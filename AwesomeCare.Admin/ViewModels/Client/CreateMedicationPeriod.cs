using AwesomeCare.DataTransferObject.DTOs.ClientMedicationPeriod;
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
        public int CRotaId { get; set; }
        public string CStartTime { get; set; }
        public string CStopTime { get; set; }
    }
}
