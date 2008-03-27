
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
    public partial class QueryElementImpl : XmlTag,  IQueryElement
    {

        #region Role ids
        #endregion

        #region Constructor 

        public QueryElementImpl() : base(ElementType.QueryElementImpl_ELEMENT) {}

        #endregion

        #region Attributes


        public IXmlAttribute NameAttribute 
        {
            get
            {
                return base.GetAttribute("name");
            }
        }

        public IXmlAttribute FlushModeAttribute 
        {
            get
            {
                return base.GetAttribute("flush-mode");
            }
        }

        #endregion

        #region Elements


        #endregion

 

        public override short GetChildRole(TreeElement child)
        {
        return base.GetChildRole(child);
        }
    }
}
