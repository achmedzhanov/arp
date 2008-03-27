using System.Collections.Generic;
using JetBrains.ReSharper.Psi.Tree;

namespace Arp.log4net.Psi.Tree
{
    public interface IParameterDescriptorManager
    {
        ICollection<IParameterDescriptor> GetParameterDescriptors(IElement element);

        IParameterDescriptor GetParameterDescriptor(IElement element, string name);

        bool IsAvailable(IElement element);

    }
}