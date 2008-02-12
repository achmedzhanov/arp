using Arp.log4net.Psi.Tree.Impl.StatisParameters;
using JetBrains.ReSharper.Psi;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class LevelParameterDescriptorDecorator : IParameterDescriptor
    {
        private IParameterDescriptor original;


        public LevelParameterDescriptorDecorator(IParameterDescriptor original)
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

        #region IParameterDescriptor Members

        public bool IsEnumerable
        {
            get { return true; }
        }

        public string[] PossibleValues
        {
            get { return StatisParameterDescriptor.THRESHOLD; }
        }

        #endregion

        public bool RequredType
        {
            get { return false; }
        }
    }
}