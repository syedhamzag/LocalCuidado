using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.RotaTask
{
   public class GetRotaTask:BaseDTO
    {
        public int RotaTaskId { get; set; }
        public string TaskName { get; set; }
        public string GivenAcronym { get; set; }
        public string NotGivenAcronym { get; set; }
        public string Remark { get; set; }
    }
}
