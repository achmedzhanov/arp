using System;
using System.Collections.Generic;
using JetBrains.ReSharper.Psi;

namespace Arp.log4net.Psi.Tree.Impl
{
    public static class ElementParametrInfoImplUtil
    {
        public static ICollection<IElementParametrInfo> GetParameters(ITypeElement type)
        {
            if (type == null) throw new ArgumentNullException("type");

            List<IElementParametrInfo> ret = new List<IElementParametrInfo>();

            foreach (IProperty property in type.Properties)
            {
                if(property.IsWritable /* TODO check public accesibility and not index condition*/ )
                    ret.Add(new ElementParametrInfoImpl(property));
            }

            return ret;
        }
    }
}