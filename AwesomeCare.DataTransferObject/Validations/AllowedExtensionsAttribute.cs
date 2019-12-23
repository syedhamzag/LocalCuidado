using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.DataTransferObject.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _Extensions;
        public AllowedExtensionsAttribute(string[] Extensions)
        {
            _Extensions = Extensions;
        }

        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            if (file == null)
            {
                ErrorMessage = "Invalid file";
                return false;
            }
            var extension = Path.GetExtension(file.FileName);
            if (!_Extensions.Contains(extension.ToLower()))
            {
                ErrorMessage = $"This file extension, {extension} is not allowed!";
                return false;
            }
            return true;
        }


    }
}
