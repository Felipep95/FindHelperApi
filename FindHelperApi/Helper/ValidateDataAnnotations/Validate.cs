using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FindHelperApi.Helper.ValidateDataAnotations
{
    public static class Validate
    {
        public static IEnumerable<ValidationResult> getValidationErros(object obj)//use this method to validate obj before insert it in database
        {
            var validateResult = new List<ValidationResult>();
            var context = new ValidationContext(obj, null, null);
            Validator.TryValidateObject(obj, context, validateResult, true);
            return validateResult;
        }
    }
}
