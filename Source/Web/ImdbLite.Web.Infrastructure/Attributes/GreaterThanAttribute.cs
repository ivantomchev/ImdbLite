namespace ImdbLite.Web.Infrastructure.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;
    using System.Web.Mvc;

    public class GreaterThanAttribute : ValidationAttribute, IClientValidatable
    {
        public readonly string otherProperty;

        private readonly string _reqPropertyName;
        private readonly object _desiredValue;

        public GreaterThanAttribute(string otherProperty, string reqPropertyName = null, object desiredvalue = null)
            : base("{0} must be greater than {1}")
        {
            this.otherProperty = otherProperty;
            this._reqPropertyName = reqPropertyName;
            this._desiredValue = desiredvalue;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, this.otherProperty);
        }

        protected override ValidationResult IsValid(object firstValue, ValidationContext validationContext)
        {
            if (_reqPropertyName != null)
            {
                var instance = validationContext.ObjectInstance;
                var type = instance.GetType();
                var propertyValue = type.GetProperty(_reqPropertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance, null);
                if (propertyValue.ToString() != _desiredValue.ToString())
                {
                    return ValidationResult.Success;
                }
            }

            var firstComparable = firstValue as IComparable;
            var secondComparable = GetSecondComparable(validationContext);

            if (firstComparable != null && secondComparable != null)
            {
                if (firstComparable.CompareTo(secondComparable) < 1)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;
        }

        protected IComparable GetSecondComparable(ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(this.otherProperty);
            if (propertyInfo != null)
            {
                var secondValue = propertyInfo.GetValue(validationContext.ObjectInstance, null);

                return secondValue as IComparable;
            }
            return null;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(this.ErrorMessageString);
            rule.ValidationType = "greaterthan";
            rule.ValidationParameters.Add("desired", _desiredValue);
            yield return rule;
        }
    }
}
