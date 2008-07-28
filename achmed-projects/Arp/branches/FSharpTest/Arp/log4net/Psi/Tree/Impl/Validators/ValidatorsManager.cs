using System;
using System.Collections.Generic;
using JetBrains.CommonControls.Validation;
using JetBrains.ReSharper.Psi;

namespace Arp.log4net.Psi.Tree.Impl.Validators
{
    public class ValidatorsManager
    {
        #region Singletone implementation

        private static ValidatorsManager _instance;

        public static ValidatorsManager Instance()
        {
            if (_instance == null)
            {
                lock (typeof (ValidatorsManager))
                {
                    if (_instance == null)
                    {
                        _instance = new ValidatorsManager();
                    }
                }
            }

            return _instance;
        }

        #endregion

        private IDictionary<string, IParameterStringValueValidator> validators =
            new Dictionary<string, IParameterStringValueValidator>();

        public ValidatorsManager()
        {
            AddClrNameValidator("System.Int16");
            AddClrNameValidator("System.Int32");
            AddClrNameValidator("System.Int64");
            AddClrNameValidator("System.Decimal");
            AddClrNameValidator("System.Double");
            AddClrNameValidator("System.DateTime");

            validators["System.Boolean"] = new BooleanValidator();
            validators["System.Text.Encoding"] = new EncodingValidator();
            validators["System.Net.IPAddress"] = new IPAddressValidator();
        }

        private void AddClrNameValidator(string typeName)
        {
            if (validators.ContainsKey(typeName))
                throw new ArgumentException(string.Format("{0} already exists", typeName));

            validators[typeName] = new CLRValueTypesValidator(typeName);
        }

        public IParameterStringValueValidator GetValidator(IParameterDescriptor descriptor)
        {
            IParameterStringValueValidator byType = GetValidator(descriptor.Type);
            if (byType != null)
                return byType;

            if (descriptor.IsEnumerable)
                return GetValidator(descriptor.PossibleValues);

            return null;
        }


        public IParameterStringValueValidator GetValidator(IType type)
        {
            IDeclaredType declaredType = type as IDeclaredType;
            if(declaredType == null)
                return null;

            string name = declaredType.GetCLRName();
            if (validators.ContainsKey(name))
                return validators[name];
            else
                return null;
        }

        public IParameterStringValueValidator GetValidator(IEnumerable<string> possibleValues)
        {
            return new EnumerableValidator(possibleValues);
        }
    }
}