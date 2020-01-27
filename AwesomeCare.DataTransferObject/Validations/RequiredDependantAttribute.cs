using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredDependantAttribute : ValidationAttribute
    {
        private string CompareValue { get; set; }
        private string CompareProperty { get; set; }
        public RequiredDependantAttribute(string compareValue, string compareProperty)
        {
            CompareValue = compareValue;
            CompareProperty = compareProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var otherProperty = validationContext.ObjectType.GetProperty(CompareProperty);
            if (otherProperty == null)
                return new ValidationResult(String.Format("Unknown property: {0}.", CompareProperty));

            var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance, null);

            if (string.Equals(CompareValue, otherPropertyValue?.ToString(), StringComparison.Ordinal))
            {

                return value == null ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
            }
            return ValidationResult.Success;
        }
    }
}
