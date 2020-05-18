using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace AwesomeCare.DataTransferObject.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ValidDateTimeAttribute : ValidationAttribute
    {
        public ValidDateTimeAttribute(string dateFormat)
        {
            format = dateFormat;
        }
        string format { get; set; }
        public override bool IsValid(object value)
        {
            if (value == null) return false;

            var isValid = DateTime.TryParseExact(value.ToString(), format, CultureInfo.GetCultureInfo("en-Us"), DateTimeStyles.None, out DateTime sdate);
            return isValid;
        }

        public override string FormatErrorMessage(string name)
        {
            ErrorMessage = string.Format(ErrorMessage, name, format);
            return base.FormatErrorMessage(name);
        }
    }
}
