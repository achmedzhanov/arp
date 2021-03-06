
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
    public partial class KeyPropertyElementImpl : XmlTag,  IKeyPropertyElement
    {

        #region Role ids

        private const int Column_ROLE_ID = 20;

        #endregion

        #region Constructor 

        public KeyPropertyElementImpl() : base(ElementType.KeyPropertyElementImpl_ELEMENT) {}

        #endregion

        #region Attributes


        public IXmlAttribute NameAttribute 
        {
            get
            {
                return base.GetAttribute("name");
            }
        }

        public IXmlAttribute AccessAttribute 
        {
            get
            {
                return base.GetAttribute("access");
            }
        }

        public IXmlAttribute TypeAttribute 
        {
            get
            {
                return base.GetAttribute("type");
            }
        }

        public IXmlAttribute ColumnAttribute 
        {
            get
            {
                return base.GetAttribute("column");
            }
        }

        public IXmlAttribute LengthAttribute 
        {
            get
            {
                return base.GetAttribute("length");
            }
        }

        #endregion

        #region Elements


        public IColumnElement Column
        {
            get
            {
                return (IColumnElement)FindChildByRole(Column_ROLE_ID);
            }
        }


        #endregion

 

        public override short GetChildRole(TreeElement child)
        {

                if (child is IColumnElement && ((XmlTag)child).TagName == "column")
                    return Column_ROLE_ID;
                else 
                
        return base.GetChildRole(child);
        }
    }
}
