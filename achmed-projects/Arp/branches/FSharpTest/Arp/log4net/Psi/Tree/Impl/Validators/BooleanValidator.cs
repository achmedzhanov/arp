using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl.Validators
{
    public class BooleanValidator : IParameterStringValueValidator
    {
        #region IParameterStringValueValidator Members

        public ValidationResult Validate(string value)
        {
            if(value != "true" && value != "false")
                return new ValidationResult(new TextRange(0, value.Length), "Expected 'true' or 'false'");

            return ValidationResult.Ok;
        }

        #endregion
    }
}