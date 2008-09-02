using System.Collections.Generic;
using Arp.log4net.Psi.Tree;


namespace Arp.log4net.Services
{
    public static class ParametersUtil
    {
        public static IParameterDescriptor GetByName(IEnumerable<IParameterDescriptor> descriptors, string name)
        {
            foreach (IParameterDescriptor descriptor in descriptors)
            {
                if(descriptor.Name == name)
                    return descriptor;
            }

            return null;
        }

        public static IDeclaredParameter GetByName(IEnumerable<IDeclaredParameter> parameters, string name)
        {
            foreach (IDeclaredParameter parameter in parameters)
            {
                if (parameter.Name == name)
                    return parameter;
            }

            return null;
        }

    }
}