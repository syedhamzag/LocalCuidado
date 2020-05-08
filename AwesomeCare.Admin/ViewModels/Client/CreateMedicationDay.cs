using AwesomeCare.DataTransferObject.DTOs.ClientMedicationDay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Client
{
    public class CreateMedicationDay:PostClientMedicationDay
    {
        public CreateMedicationDay()
        {
            RotaTypes = new List<CreateMedicationPeriod>();
        }
        public bool IsSelected { get; set; }
        public string DayOfWeek { get; set; }

        public List<CreateMedicationPeriod> RotaTypes { get; set; }
    }
}
