using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.DataTransferObject.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        /// <summary>
        /// file length in MB
        /// </summary>
        public long Lenght { get; set; }
        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            //if (file == null)
            //{
            //    ErrorMessage = "Invalid file";
            //    return false;
            //}
            if(file != null)
            {
                if (!(file.Length <= (Lenght * 1024 * 1024)))
                {
                    ErrorMessage = $"Invalid file length, max length is {Lenght}";
                    return false;
                }
            }
            return true;
        }

    }
}
