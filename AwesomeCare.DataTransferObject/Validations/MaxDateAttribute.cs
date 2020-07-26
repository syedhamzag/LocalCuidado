using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.Validations
{
    public class MaxDateAttribute : ValidationAttribute
    {
        public MaxDateAttribute(DateTime maximumDateTime)
        {

        }
        public override bool IsValid(object value)
        {
            return base.IsValid(value);
        }
    }
}
