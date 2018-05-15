namespace ImdbLite.Web.Infrastructure.Attributes
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class RequiredMinimumElementsAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly int _minElements;
        public RequiredMinimumElementsAttribute(int minElements)
        {
            _minElements = minElements;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(this.ErrorMessageString);
            rule.ValidationType = "requiredminimumelements";
            rule.ValidationParameters.Add("min", _minElements);
            yield return rule;
        }

        public override bool IsValid(object value)
        {
            var list = value as ICollection;
            if (list != null)
            {
                return list.Count >= _minElements;
            }
            return false;
        }
    }
}
