
/*
This file was generated by Arp.Generator
*/
#region Imports
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.ReSharper.Psi.Tree;
using System.Collections.Generic;
#endregion

namespace Arp.NH.Psi.Tree
{
    public partial interface IPropertyElement : INHElement 
    {
        #region Attributes


        IXmlAttribute NameAttribute {get;}

        IXmlAttribute AccessAttribute {get;}

        IXmlAttribute TypeAttribute {get;}

        IXmlAttribute ColumnAttribute {get;}

        IXmlAttribute LengthAttribute {get;}

        IXmlAttribute NotNullAttribute {get;}

        IXmlAttribute UniqueAttribute {get;}

        IXmlAttribute UpdateAttribute {get;}

        IXmlAttribute InsertAttribute {get;}

        IXmlAttribute OptimisticLockAttribute {get;}

        IXmlAttribute FormulaAttribute {get;}

        IXmlAttribute UniqueKeyAttribute {get;}

        IXmlAttribute IndexAttribute {get;}

        #endregion

        #region Elements


        IMetaElement Meta {get;}
        IColumnElement Column {get;}
        ITypeElement Type {get;}
        #endregion

 
    }
}
