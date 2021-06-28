using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class KeyWorkerWorkteam
    {
        public int KeyWorkerWorkteamId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int KeyWorkerId { get; set; }

        public virtual StaffKeyWorkerVoice KeyWorker { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
