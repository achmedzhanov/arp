using System;
using System.Collections.Generic;
using Arp.Assertions;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class ParameterDescriptorImpl : IParameterDescriptor
    {
        private readonly IProperty property;
        private readonly string[] boolvalues = new string[] {"true", "false"};
        private bool isEnumerable = false;
        private string [] possibleValues;

        public ParameterDescriptorImpl(IProperty property)
        {
            if (property == null) throw new ArgumentNullException("property");
            this.property = property;

            SetEnumerable();

        }

        private void SetEnumerable()
        {
            IDeclaredType declaredType = property.Type as IDeclaredType;
            if(declaredType == null)
                return;
            ITypeElement typeElement = declaredType.GetTypeElement();
            if(typeElement == null)
                return;
            // bool
            if(typeElement.CLRName == "System.Boolean")
            {
                isEnumerable = true;
                possibleValues = new string[] {"true", "false"};
                return;
            }

            // enum
            IEnum @enum = typeElement as IEnum;
            if(@enum != null)
            {
                IField[] members = @enum.EnumMembers;
                if(members.Length > 0)
                {
                    isEnumerable = true;
                    List<string> names = new List<string>();
                    foreach (IField field in members)
                    {
                        names.Add(field.ShortName);
                    }
                    possibleValues = names.ToArray();
                }
            }
        }

        public string Name
        {
            get
            {
                return Normalize(property.ShortName);
            }
        }

        private string Normalize(string name)
        {
            Assert.Check(name.Length > 0);
            if(char.IsUpper(name[0]))
            {
                return char.ToLower(name[0]) + name.Substring(1);
            }
            
            return name;
        }

        public IType Type
        {
            get
            {
                return property.Type;
            }
        }


        public IDeclaredElement DecraedElement
        {
            get { return property; }
        }

        ///<summary>
        ///Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Format("[Name:{0},Type:{1}]", Name, Type);
        }

        #region IParameterDescriptor Members

        public bool IsRequired
        {
            get { return false; }
        }

        public string RequredBefore
        {
            get { throw new System.NotImplementedException(); }
        }

        public string RequredAfter
        {
            get { throw new System.NotImplementedException(); }
        }

        public bool IsAttribute
        {
            get { return false; }
        }


        public string[] Conflicts
        {
            get { throw new System.NotImplementedException(); }
        }


        public bool IsEnumerable
        {
            get
            {
                return isEnumerable;
            }
        }

        public string[] PossibleValues
        {
            get
            {
                return possibleValues;
            }
        }


        public bool RequredType
        {
            get
            {
                // TODO inctroduce class ParameterDescriptorManager as solution component

                IDeclaredType type = this.Type as IDeclaredType;

               bool stringValueReuqred = true;
                if (type != null)
                {
                    ITypeElement typeElement = type.GetTypeElement();
                    IModifiersOwner modifiersOwner = typeElement as IModifiersOwner;
                    if (modifiersOwner != null && modifiersOwner.IsAbstract)
                        stringValueReuqred = false;

                    IInterface @interface = typeElement as IInterface;
                    if (@interface != null)
                        stringValueReuqred = false;
                    // TODO check register converters
                }

                return !stringValueReuqred;
            }
        }

        #endregion
    }
}