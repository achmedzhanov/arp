using System;
using System.Collections.Generic;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace Arp.log4net.Services
{
    public class L4NTagProcessor
    {
        private IXmlTag rootTag;
        private Dictionary<string, string> appendersInfo = new Dictionary<string, string>();

        public void Process(IXmlTag tag)
        {
            this.rootTag = tag;

            ICollection<IXmlTag> appenderTags = GetTagsByName(rootTag, "appender");
            
            foreach (IXmlTag appenderTag in appenderTags)
            {
                ProcessAppender(appenderTag);
            }

//            TODO build list of ordered rules for parsing, run rules step by step by result of preveus rules



        }

        private void ProcessAppender(IXmlTag tag)
        {
            IXmlAttribute nameAttr = tag.GetAttribute("name");
            string name = nameAttr.Value.UnquotedValue;
            IXmlAttribute typeAttr = tag.GetAttribute("type");
            string type = typeAttr.Value.UnquotedValue;
//            if(!String.IsNullOrEmpty(type))
//                TODO add error
//            if(!String.IsNullOrEmpty(name))
//                TODO add error
            
        }

        protected static ICollection<IXmlTag> GetTagsByName(IXmlTag tag, string name)
        {
            return tag.GetTags(delegate(IXmlTag obj)
                                   {
                                       return obj.TagName == name;
                                   });



        }



    }
}