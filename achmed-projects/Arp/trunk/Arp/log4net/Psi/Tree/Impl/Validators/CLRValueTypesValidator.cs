using System;
using System.Reflection;
using Arp.Assertions;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl.Validators
{
    public class CLRValueTypesValidator : IParameterStringValueValidator
    {
        public string clrName;


        public CLRValueTypesValidator(string clrName)
        {
            if (clrName == null) throw new ArgumentNullException("clrName");
            this.clrName = clrName;
        }

        #region IParameterStringValueValidator Members

        public ValidationResult Validate(string value)
        {
            Type target = GetCLRType();
            Assert.CheckNotNull(target);
            try
            {
                // this code is based on  OptionConverter.ConvertStringTo
                MethodInfo method = target.GetMethod("Parse", new Type[] { typeof(string) });
                if (method != null)
                {
                    object parsed =  method.Invoke(null, BindingFlags.InvokeMethod, null, new object[] { value }, System.Globalization
.CultureInfo.InvariantCulture);
                }

            }
            catch (Exception ex)
            {
//                string message = ex.Message;
                string message = string.Format("Invalid value for type {0}", clrName);
                return new ValidationResult(new TextRange(0, value.Length), message);
            }
            return ValidationResult.Ok;
        }

        #endregion

        protected Type GetCLRType()
        {
            return Type.GetType(clrName);
        }
    }
}