using AwesomeCare.DataTransferObject.DTOs.ClientRotaTask;
using AwesomeCare.DataTransferObject.DTOs.RotaTask;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.RotaTask
{
    public class RotaTaskViewModel
    {

        public RotaTaskViewModel()
        {
            RotaTasks = new List<GetRotaTask>();
        }
        public string SubTitle { get; set; } = "Add Task";
        [Required]
        public int RotaTaskId { get; set; }
        [Required]
        [Display(Name = "Task")]
        [MaxLength(125)]
        public string TaskName { get; set; }
        [Required]
        [MaxLength(50)]
        public string GivenAcronym { get; set; }
        [Required]
        [MaxLength(50)]
        public string NotGivenAcronym { get; set; }
        [MaxLength(125)]
        public string Remark { get; set; }
        public bool Deleted { get; set; } = false;
        public List<GetRotaTask> RotaTasks { get; set; }
    }
}
