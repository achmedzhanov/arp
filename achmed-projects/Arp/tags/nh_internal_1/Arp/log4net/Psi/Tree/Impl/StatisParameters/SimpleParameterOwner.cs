using System;
using System.Collections.Generic;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.log4net.Psi.Tree.Impl.StatisParameters
{
    public class SimpleParameterOwner : IDeclaredParametersOwner
    {
        private IXmlTag tag;
        private IParameterDescriptorProvider provider;

        public SimpleParameterOwner(IXmlTag tag, IParameterDescriptorProvider provider)
        {
            if (tag == null) throw new ArgumentNullException("tag");
            if (provider == null) throw new ArgumentNullException("provider");
            this.tag = tag;
            this.provider = provider;
        }

        #region IDeclaredParametersOwner Members

        public ICollection<IDeclaredParameter> GetParams()
        {
            List<IDeclaredParameter> ret = new List<IDeclaredParameter>();
            
            foreach (IXmlAttribute attribute in tag.Attributes)
            {
                ret.Add(new AttributeParamImpl(attribute, provider));
            }

            return ret;
        }

        #endregion
    }
}