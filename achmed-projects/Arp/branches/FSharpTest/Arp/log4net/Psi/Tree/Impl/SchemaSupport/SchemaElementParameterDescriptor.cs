using System.Xml.Schema;
using JetBrains.ReSharper.Psi;

namespace Arp.log4net.Psi.Tree.Impl.SchemaSupport
{
    public class SchemaElementParameterDescriptor : IParameterDescriptor
    {
        private XmlSchemaElement element;

        public SchemaElementParameterDescriptor(XmlSchemaElement element)
        {
            this.element = element;
        }

        #region IParameterDescriptor Members

        public string Name
        {
            get
            {
                return element.Name;
            }
        }

        public IType Type
        {
            get
            {
                return null;
            }
        }

        public IDeclaredElement DecraedElement
        {
            get
            {
                return null;
            }
        }

        public bool IsRequired
        {
            get
            {
                return element.MinOccurs > 0;
            }
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
            get { throw new System.NotImplementedException(); }
        }

        public string[] PossibleValues
        {
            get { throw new System.NotImplementedException(); }
        }

        public bool RequredType
        {
            get { return false; }
        }

        #endregion


    }
}