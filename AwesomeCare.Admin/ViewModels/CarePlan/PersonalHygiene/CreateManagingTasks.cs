using AwesomeCare.DataTransferObject.DTOs.CarePlanHygiene.ManagingTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.CarePlan.PersonalHygiene
{
    public class CreateManagingTasks
    {
        public CreateManagingTasks()
        {
            GetManagingTasks = new List<GetManagingTasks>();
        }

        public List<GetManagingTasks> GetManagingTasks { get; set; }

        public string ClientName { get; set; }
        public int TaskCount { get; set; } = 0;
        public int ClientId { get; set; }


        #region List Items for Get
        public int TaskId { get; set; }
        public int Task { get; set; }
        public string Help { get; set; }
        public int Status { get; set; }
        #endregion
    }
}
