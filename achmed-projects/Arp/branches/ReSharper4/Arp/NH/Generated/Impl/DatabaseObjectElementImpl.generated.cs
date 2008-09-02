
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
    public partial class DatabaseObjectElementImpl : XmlTag,  IDatabaseObjectElement
    {

        #region Role ids

        private const int Create_ROLE_ID = 20;

        private const int Drop_ROLE_ID = 21;

        private const int DialectScope_ROLE_ID = 22;

        #endregion

        #region Constructor 

        public DatabaseObjectElementImpl() : base(ElementType.DatabaseObjectElementImpl_ELEMENT) {}

        #endregion

        #region Attributes

        #endregion

        #region Elements


        public ICreateElement Create
        {
            get
            {
                return (ICreateElement)FindChildByRole(Create_ROLE_ID);
            }
        }

        public IDropElement Drop
        {
            get
            {
                return (IDropElement)FindChildByRole(Drop_ROLE_ID);
            }
        }

        public IDialectScopeElement DialectScope
        {
            get
            {
                return (IDialectScopeElement)FindChildByRole(DialectScope_ROLE_ID);
            }
        }


        #endregion

 

        public override short GetChildRole(TreeElement child)
        {

                if (child is ICreateElement && ((XmlTag)child).TagName == "create")
                    return Create_ROLE_ID;
                else 
                
                if (child is IDropElement && ((XmlTag)child).TagName == "drop")
                    return Drop_ROLE_ID;
                else 
                
                if (child is IDialectScopeElement && ((XmlTag)child).TagName == "dialect-scope")
                    return DialectScope_ROLE_ID;
                else 
                
        return base.GetChildRole(child);
        }
    }
}
