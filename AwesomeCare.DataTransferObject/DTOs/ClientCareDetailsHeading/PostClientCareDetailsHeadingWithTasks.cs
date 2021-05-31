using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientCareDetailsHeading
{
   public class PostClientCareDetailsHeadingWithTasks
    {
        public PostClientCareDetailsHeadingWithTasks()
        {
            Tasks = new List<PostClientCareDetailsHeadingTask>();
        }
        [Required]
        [MaxLength(225)]
        public string Heading { get; set; }
        public List<PostClientCareDetailsHeadingTask> Tasks { get; set; }
    }
}
