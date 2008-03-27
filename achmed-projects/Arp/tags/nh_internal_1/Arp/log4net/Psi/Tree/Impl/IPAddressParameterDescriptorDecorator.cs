using JetBrains.ReSharper.Psi;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class IPAddressParameterDescriptorDecorator : IParameterDescriptor
    {
        private IParameterDescriptor original;

        public IPAddressParameterDescriptorDecorator(IParameterDescriptor original)
        {
            this.original = original;
        }


        public string Name
        {
            get { return original.Name; }
        }

        public IType Type
        {
            get { return original.Type; }
        }

        public IDeclaredElement DecraedElement
        {
            get { return original.DecraedElement; }
        }

        public bool IsRequired
        {
            get { return original.IsRequired; }
        }

        public string RequredBefore
        {
            get { return original.RequredBefore; }
        }

        public string RequredAfter
        {
            get { return original.RequredAfter; }
        }

        public bool IsAttribute
        {
            get { return original.IsAttribute; }
        }

        public string[] Conflicts
        {
            get { return original.Conflicts; }
        }

        public bool IsEnumerable
        {
            get { return original.IsEnumerable; }
        }

        public string[] PossibleValues
        {
            get { return original.PossibleValues; }
        }

        public bool RequredType
        {
            get { return false; }
        }
    }
}