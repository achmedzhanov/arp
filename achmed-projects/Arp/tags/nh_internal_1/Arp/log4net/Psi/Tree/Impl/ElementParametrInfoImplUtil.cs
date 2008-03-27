using System;
using System.Collections.Generic;
using JetBrains.ReSharper.Psi;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl
{
    public static class ElementParametrInfoImplUtil
    {
        public static ICollection<IParameterDescriptor> GetParameters(ITypeElement type, params string [] skipNames)
        {
            if (type == null) throw new ArgumentNullException("type");

            // TODO see .GetManager().PredefinedType.String
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
                    if (skipNames != null && Array.IndexOf(skipNames, property.ShortName) != -1)
                        continue;

                    IModifiersOwner modifiersOwner = (IModifiersOwner)property;
                    if (modifiersOwner.IsAbstract)
                        continue;
                    if (modifiersOwner.GetAccessRights() != AccessRights.PUBLIC)
                        continue;
                    
                    if (!property.IsWritable)
                        continue;

                    if (property.IsStatic)
                        continue;

                    ret.Add(Create(property));
                }                
            }

            return ret;
        }

        private static IParameterDescriptor Create(IProperty property)
        {
            ParameterDescriptorImpl impl = new ParameterDescriptorImpl(property);

            IDeclaredType declaredType = property.Type as IDeclaredType;
            if (declaredType != null && declaredType.GetCLRName() == "log4net.Core.Level")
                return new LevelParameterDescriptorDecorator(impl);
            else if (declaredType != null && declaredType.GetCLRName() == "System.Text.Encoding")
                return new EncodingParameterDescriptorDecorator(impl);
            else if (declaredType != null && declaredType.GetCLRName() == "System.Net.IPAddress")
                return new IPAddressParameterDescriptorDecorator(impl);

            return impl;
        }
    }
}