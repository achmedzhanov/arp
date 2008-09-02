using System.Collections.Generic;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl.Validators
{
    public class EnumerableValidator : IParameterStringValueValidator
    {
        private IEnumerable<string> possibleValues;


        public EnumerableValidator(IEnumerable<string> possibleValues)
        {
            this.possibleValues = possibleValues;
        }

        #region IParameterStringValueValidator Members

        public ValidationResult Validate(string value)
        {
            foreach (string possibleValue in possibleValues)
            {
                if(value == possibleValue)
                    return ValidationResult.Ok;
            }

            string valuesString = string.Join(", ", new List<string>(possibleValues).ToArray());

            return new ValidationResult(new TextRange(0, value.Length), "Expected values: " + valuesString);
        }

        #endregion
    }
}