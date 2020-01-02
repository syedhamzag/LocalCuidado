using AwesomeCare.DataTransferObject.DTOs.ClientCareDetailsTask;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientCareDetailsHeading
{
   public class GetClientCareDetailsHeadingWithTasks:BaseDTO
    {
        public GetClientCareDetailsHeadingWithTasks()
        {
            Tasks = new List<GetClientCareDetailsTask>();
        }
        public int ClientCareDetailsHeadingId { get; set; }
        public string Heading { get; set; }
       // public List<string> Tasks { get; set; }
        public List<GetClientCareDetailsTask> Tasks { get; set; }
    }
}
