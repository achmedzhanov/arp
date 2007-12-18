using System.Collections.Generic;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class BaseL4NTag : XmlTag
    {
        public BaseL4NTag(CompositeNodeType _type) : base(_type)
        {
        }

        protected string GetAttributeValue(string attributeName, string notExistsName)
        {
            IXmlAttribute attribute = this.GetAttribute(attributeName);
            return attribute == null ? string.Empty : attribute.UnquotedValue;
        }

        protected IXmlAttributeValue GetAttributeValueElement(string attributeName)
        {
            IXmlAttribute attribute = this.GetAttribute(attributeName);
            if (attribute == null)
                return null;
            return attribute.Value;
        }



        protected ICollection<T> GetTagsByType<T>()
            where T :class
        {
            IList<IXmlTag> tags = this.GetTags(delegate(IXmlTag obj)
                                                   {
                                                       return true;
                                                   });
            List<T> ret = new List<T>();
            foreach (IXmlTag tag in tags)
            {
                T casted = tag as T;
                if(casted != null)
                    ret.Add(casted);
            }
            
            return ret;
        }



    }
}