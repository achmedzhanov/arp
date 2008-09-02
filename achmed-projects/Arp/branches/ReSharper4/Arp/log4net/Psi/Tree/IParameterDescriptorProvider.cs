using System.Collections.Generic;

namespace Arp.log4net.Psi.Tree
{
    public interface IParameterDescriptorProvider
    {
        /// <summary>
        /// return list of all available parameters
        /// </summary>
        /// <returns></returns>
        ICollection<IParameterDescriptor> GetParameterDescriptors();

        IParameterDescriptor GetParameterDescriptor(string name);

        bool IsAvailable { get;}

    }
}