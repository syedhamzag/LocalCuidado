using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Models
{
    public class ApiExceptionModel
    {
        public ApiExceptionModel()
        {
            Errors = new List<string>();
        }
        public string Key { get; set; }
        public List<string> Errors { get; set; }
    }
}
