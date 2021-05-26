using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientCleaning
{
    public class GetClientCleaning
    {
        public GetClientCleaning()
        {

        }

        public int CleaningId { get; set; }
        public int NutritionId { get; set; }
        public int AreasAndItems { get; set; }
        public string Details { get; set; }
        public string SafetyHazard { get; set; }
        public string LocationOfItem { get; set; }
        public string DescOfItem { get; set; }
        public DateTime MinuteAlloted { get; set; }
        public string Disposal { get; set; }
        public int WhereToGet { get; set; }
        public string WhereToKeep { get; set; }
        public string SEEVIDEO { get; set; }
        public string Image { get; set; }
        public string DAYOFCLEANING { get; set; }
        public DateTime DATEFROM { get; set; }
        public DateTime DATETO { get; set; }
        public int STAFFId { get; set; }
    }
}
