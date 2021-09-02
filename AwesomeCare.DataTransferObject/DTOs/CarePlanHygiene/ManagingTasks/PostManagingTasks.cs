using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.CarePlanHygiene.ManagingTasks
{
    public class PostManagingTasks
    {
        public int TaskId { get; set; }
        public int ClientId { get; set; }
        public int Task { get; set; }
        public string Help { get; set; }
        public int Status { get; set; }
    }
}
