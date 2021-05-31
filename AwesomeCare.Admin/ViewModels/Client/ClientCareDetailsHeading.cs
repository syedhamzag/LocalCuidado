using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Client
{
    public class ClientCareDetailsHeading
    {
        public ClientCareDetailsHeading()
        {
            Tasks = new List<ClientCareDetailsTask>();
        }
        public int CareDetailsHeadingId { get; set; }
        public string Heading { get; set; }
       // public string Header { get; set; }
        public List<ClientCareDetailsTask> Tasks { get; set; }
    }
}
