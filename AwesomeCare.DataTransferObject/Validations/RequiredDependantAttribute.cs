using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredDependantAttribute : ValidationAttribute
    {
        private object CompareValue { get; set; }
        private string CompareProperty { get; set; }
        private Type Type { get; set; }
        public RequiredDependantAttribute(string compareValue, string compareProperty, Type type)
        {
            CompareValue = compareValue;
            CompareProperty = compareProperty;
            Type = type;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var otherProperty = validationContext.ObjectType.GetProperty(CompareProperty);
            if (otherProperty == null)
                return new ValidationResult(String.Format("Unknown property: {0}.", CompareProperty));

            var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance, null);

            var convert = Convert.ChangeType(CompareValue, Type);
            if (object.Equals(convert, otherPropertyValue))
            {
                return value == null ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
            }
            //if (string.Equals(CompareValue, otherPropertyValue?.ToString(), StringComparison.Ordinal))
            //{

            //    return value == null ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
            //}
            return ValidationResult.Success;

        }
    }
}
