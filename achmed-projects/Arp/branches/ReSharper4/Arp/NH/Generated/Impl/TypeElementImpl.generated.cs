
/*
This file was generated by Arp.Generator
*/
#region Imports
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using System.Collections.Generic;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
#endregion

namespace Arp.NH.Psi.Tree.Impl
{
    public partial class TypeElementImpl : XmlTag,  ITypeElement
    {

        #region Role ids

        private const int Param_ROLE_ID = 20;

        #endregion

        #region Constructor 

        public TypeElementImpl() : base(ElementType.TypeElementImpl_ELEMENT) {}

        #endregion

        #region Attributes


        public IXmlAttribute NameAttribute 
        {
            get
            {
                return base.GetAttribute("name");
            }
        }

        #endregion

        #region Elements


        public IParamElement Param
        {
            get
            {
                return (IParamElement)FindChildByRole(Param_ROLE_ID);
            }
        }


        #endregion

 

        public override short GetChildRole(TreeElement child)
        {

                if (child is IParamElement && ((XmlTag)child).TagName == "param")
                    return Param_ROLE_ID;
                else 
                
        return base.GetChildRole(child);
        }
    }
}
