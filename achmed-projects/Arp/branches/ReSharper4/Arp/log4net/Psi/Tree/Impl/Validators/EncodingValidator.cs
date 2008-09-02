using System;
using System.Text;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl.Validators
{
    public class EncodingValidator : IParameterStringValueValidator
    {
        #region IParameterStringValueValidator Members

        public ValidationResult Validate(string value)
        {
            try
            {
                Encoding encoding = Encoding.GetEncoding(value);
            }
            catch (Exception ex)
            {
                return new ValidationResult(new TextRange(0, value.Length), "It is not a valid code page name");
            }

            return ValidationResult.Ok;
        }

        #endregion
    }
}