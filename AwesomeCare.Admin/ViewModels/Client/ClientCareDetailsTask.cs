using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Client
{
    public class ClientCareDetailsTask
    {
      //  public int CareDetailsTaskId { get; set; }
        public int CareDetailsHeadingId { get; set; }
        public string Task { get; set; }
        public string Description { get; set; }
        public string Risk { get; set; }
        public string Mitigation { get; set; }
        public string Location { get; set; }
        public string Remark { get; set; }
        public bool IsSelected { get; set; }
    }
}
