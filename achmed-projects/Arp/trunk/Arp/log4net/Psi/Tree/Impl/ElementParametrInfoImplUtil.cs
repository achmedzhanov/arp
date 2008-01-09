using System;
using System.Collections.Generic;
using JetBrains.ReSharper.Psi;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl
{
    public static class ElementParametrInfoImplUtil
    {
        public static ICollection<IParameterDescriptor> GetParameters(ITypeElement type)
        {
            if (type == null) throw new ArgumentNullException("type");

//            if(is prmiive type)
//                return EmptyArray<IParameterDescriptor>.Instance;

            List<IParameterDescriptor> ret = new List<IParameterDescriptor>();

            List<ITypeElement> hierarchy = new List<ITypeElement>();
            hierarchy.Add(type);

            bool typeIsInterface = (type as IInterface) != null;

            /// collect hierarchy
            CollectionUtil.ProcessRecursively<ITypeElement>(type, 
                delegate(ITypeElement input)
                {
                    foreach (IDeclaredType superType in input.GetSuperTypes())
                    {
                        ITypeElement element = superType.GetTypeElement();
                        if (!typeIsInterface && (element as IInterface) != null)
                            continue;
                        if(superType.GetCLRName() == "System.Object")
                            return EmptyArray<ITypeElement>.Instance;

                        List<ITypeElement> oneElementList = new List<ITypeElement>();
                        oneElementList.Add(element);
                        return oneElementList;
                    }
                    
                    return EmptyArray<ITypeElement>.Instance;

                }, 
                delegate(ITypeElement obj)
                    {
                        hierarchy.Add(obj);
                    }
                );

            foreach (ITypeElement nextElement in hierarchy)
            {
                foreach (IProperty property in nextElement.Properties)
                {
                    if (property.IsWritable && !property.IsStatic /* TODO check public accesibility and not index condition*/ )
                        ret.Add(new ParameterDescriptorImpl(property));
                }                
            }

            return ret;
        }
    }
}