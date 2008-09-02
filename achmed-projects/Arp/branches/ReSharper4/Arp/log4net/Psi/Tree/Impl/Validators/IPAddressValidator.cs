using System;
using System.Net;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl.Validators
{
    public class IPAddressValidator : IParameterStringValueValidator
    {
        #region IParameterStringValueValidator Members

        public ValidationResult Validate(string value)
        {
            try
            {
                if(String.IsNullOrEmpty(value))
                    throw new Exception("Empty address");

                UriBuilder uriBuilder = new UriBuilder(value);
                if(uriBuilder.Host != value)
                    throw new Exception("");

// this code based on  IPAddressConverter but it's so slow ...
//                IPHostEntry entry = Dns.GetHostEntry(value);
//
//                if(entry.AddressList.Length != 1)
//                    throw new Exception("AddressList.Length is " + entry.AddressList.Length);

            }
            catch (Exception ex)
            {
                return new ValidationResult(new TextRange(0, value.Length), "Invalid address");
            }

            return ValidationResult.Ok;
        }

        #endregion

    }
}