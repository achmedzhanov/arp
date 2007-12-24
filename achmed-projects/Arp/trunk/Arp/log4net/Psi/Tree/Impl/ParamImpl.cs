using System.Collections.Generic;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class ParamImpl : BaseL4NTag, IParam
    {
        public ParamImpl()
            : base(L4NElementType.APPENDER_PARAM_ELEMENT)
        {
        }

        public string Name
        {
            get { return this.TagName; }
        }

        public DocumentRange NameDocumentRange
        {
            get
            {
                return this.Header.GetDocumentRange();
            }
        }

        public string StringValue
        {
            get
            {
                return base.GetAttributeValue("value", null);
            }
        }


        public IElementParametersOwner Owner
        {
            get { return (IElementParametersOwner)Parent; }
        }

        public IElementParameterInfoProvider ElementParameterInfoProvider
        {
            get { return (IElementParameterInfoProvider)Parent; }
        }

        public ICollection<IParam> GetParams()
        {
            return GetTagsByType<IParam>();
        }

    }
}