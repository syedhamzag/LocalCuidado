using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientRota
{
  public  class GetClientRota:BaseDTO
    {
        public int RotaId { get; set; }
        public int NumberOfStaff { get; set; }
        public string RotaName { get; set; }
        public string Gender { get; set; }
        public string Area { get; set; }
    }
}
