using Arp.log4net.Psi.Tree.Impl;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.log4net.Psi.Tree
{
    public abstract class L4NConpositeElementType : XmlElementTypes.XmlCompositeNodeType
    {
        
        protected L4NConpositeElementType(string s) : base(s, L4NLanguageService.L4N)
        {
        }

        public override PsiLanguageType LanguageType
        {
            get { return base.LanguageType; }
        }
    }

    public class L4NSECTION_ELEMENT_TYPE : L4NConpositeElementType
    {
        public L4NSECTION_ELEMENT_TYPE(string s)
            : base(s)
        {
        }

        public override CompositeElement Create()
        {
            return new L4NSectionImpl();
        }
    }


    public class APPENDER_ELEMENT_TYPE : L4NConpositeElementType
    {
        public APPENDER_ELEMENT_TYPE(string s)
            : base(s)
        {
        }

        public override CompositeElement Create()
        {
            return new AppenderImpl();
        }
    }

    public class APPENDER_PARAM_ELEMENT_TYPE : L4NConpositeElementType
    {
        public APPENDER_PARAM_ELEMENT_TYPE(string s)
            : base(s)
        {
        }

        public override CompositeElement Create()
        {
            return new PropertyParamImpl();
        }
    }

    public class APPENDER_ELEMENT_REF_TYPE : L4NConpositeElementType
    {
        public APPENDER_ELEMENT_REF_TYPE(string s)
            : base(s)
        {
        }

        public override CompositeElement Create()
        {
            return new AppenderRefImpl();
        }
    }

    public class LOGGER_ELEMENT_TYPE : L4NConpositeElementType
    {
        public LOGGER_ELEMENT_TYPE(string s)
            : base(s)
        {
        }

        public override CompositeElement Create()
        {
            return new LoggerImpl();
        }
    }

    public class REFERENCE_NAME : L4NConpositeElementType
    {
        public REFERENCE_NAME(string s)
            : base(s)
        {
        }

        public override CompositeElement Create()
        {
            return new ReferenceName();
        }
    }

    public class REFERENCE_NAME_ATTRIBUTE_VALUE : L4NConpositeElementType
    {
        public REFERENCE_NAME_ATTRIBUTE_VALUE(string s)
            : base(s)
        {
        }

        public override CompositeElement Create()
        {
            return new ReferenceNameAttributeValue();
        }
    }



}