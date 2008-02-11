using System.Collections.Generic;
using Arp.Assertions;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Impl;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl
{

    // TODO We need to implement GetReferense to have QuickDoc action (it is work with AppenderRef (!))
// tip in Lookup TODO see  IdentifierTooltipProvider.GetTooltip,  CSharpIdentifierTooltipProvider, CSharpIdentifierHighlighting
    // quick doc TODO see ShowQuickDocAction
    public class PropertyParamImpl : TypedParametersOwner, IDeclaredParameter, IParameterDescriptorProvider, IDeclaredParametersOwner  //, ITagNameBasedDeclaredParameter
    {
        private ICollection<IParameterDescriptor> elementParametrInfos;

        public PropertyParamImpl()
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
                return Header.Name.GetDocumentRange();
            }
        }

        public string StringValue
        {
            get
            {
                return base.GetAttributeValue("value", null);
            }
        }


        public IDeclaredParametersOwner Owner
        {
            get { return (IDeclaredParametersOwner)Parent; }
        }

        public IParameterDescriptorProvider ParameterDescriptorProvider
        {
            get { return (IParameterDescriptorProvider)Parent; }
        }


        protected override ITreeReference[] CreateReferences()
        {
            return new ITreeReference[] { new ParamPropertyReference(this) };
        }

        public ICollection<IDeclaredParameter> GetParams()
        {
            return GetTagsByType<IDeclaredParameter>();
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



        public IParameterDescriptor ParameterDescriptor
        {
            get
            {
                Assert.Check(ParameterDescriptorProvider.IsAvailable, "ParameterDescriptorProvider.IsAvailable");
                return ParameterDescriptorProvider.GetParameterDescriptor(this.Name);
            }
        }

    }
}