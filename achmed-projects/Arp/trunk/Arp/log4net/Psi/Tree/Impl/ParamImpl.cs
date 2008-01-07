using System.Collections.Generic;
using Arp.Assertions;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Impl;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class ParamImpl : TypedParametersOwner, IParam
    {
        private ICollection<IParameterDescriptor> elementParametrInfos;

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

        public IParameterDescriptorProvider ParameterDescriptorProvider
        {
            get { return (IParameterDescriptorProvider)Parent; }
        }

        public ICollection<IParam> GetParams()
        {
            return GetTagsByType<IParam>();
        }


        public ICollection<IParameterDescriptor> GetParameterDescriptors()
        {

            if (elementParametrInfos == null)
            {
                if (!IsAvailable)
                    elementParametrInfos = EmptyArray<IParameterDescriptor>.Instance;
                else
                {

                        ITypeElement type = GetParametersOwnerType();
                        if (type == null)
                            elementParametrInfos = EmptyArray<IParameterDescriptor>.Instance;
                        else
                            elementParametrInfos = ElementParametrInfoImplUtil.GetParameters(type);                                    
                    
                }
            }

            return elementParametrInfos;
        }

        protected ITypeElement GetParametersOwnerType()
        {
            IDeclaredType declaredType = ParameterDescriptor.Type as IDeclaredType;
            if (declaredType == null)
                return null;

                if (TypeFromAttribute != null)
                    // todo check inheritance
                    return TypeFromAttribute;
                else
                    return declaredType.GetTypeElement();
                                                       
        }

        public IParameterDescriptor GetParameterDescriptor(string name)
        {
            IList<IParameterDescriptor> found = CollectionUtil.FindAll(GetParameterDescriptors(), delegate(IParameterDescriptor obj)
                                                                                {
                                                                                    return obj.Name == name;
                                                                                });

            Assert.Check(found.Count < 2);

            return found.Count == 0 ? null : found[0];
        }

        public bool IsAvailable
        {
            get
            {
                return ParameterDescriptorProvider.IsAvailable 
                    && ParameterDescriptor != null;
            }
        }



        protected IParameterDescriptor ParameterDescriptor
        {
            get
            {
                return ParameterDescriptorProvider.GetParameterDescriptor(this.Name);
            }
        }

    }
}