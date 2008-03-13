
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
    public partial class CompositeIndexElementImpl : XmlTag,  ICompositeIndexElement
    {

        #region Role ids

        private const int KeyPropertys_ROLE_ID = 1;

        private const int KeyManyToOnes_ROLE_ID = 2;

        #endregion

        #region Constructor 

        public CompositeIndexElementImpl() : base(ElementType.CompositeIndexElementImpl_ELEMENT) {}

        #endregion

        #region Attributes


        public IXmlAttribute ClassAttribute 
        {
            get
            {
                return base.GetAttribute("class");
            }
        }

        #endregion

        #region Elements


        public ICollection<IKeyPropertyElement> KeyPropertys
        { 
            get
            { 
                return base.FindListOfChildrenByRole<IKeyPropertyElement>(KeyPropertys_ROLE_ID);
            } 
        }

        public ICollection<IKeyManyToOneElement> KeyManyToOnes
        { 
            get
            { 
                return base.FindListOfChildrenByRole<IKeyManyToOneElement>(KeyManyToOnes_ROLE_ID);
            } 
        }


        #endregion

 

        public override short GetChildRole(TreeElement child)
        {

                if (child is IKeyPropertyElement && ((XmlTag)child).TagName == "key-property")
                    return KeyPropertys_ROLE_ID;
                else 
                
                if (child is IKeyManyToOneElement && ((XmlTag)child).TagName == "key-many-to-one")
                    return KeyManyToOnes_ROLE_ID;
                else 
                
        return base.GetChildRole(child);
        }
    }
}
