using System;
using System.Collections.Generic;
using Arp.Assertions;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace Arp
{
    public class UnusedModulesProcessor : IRecursiveElementProcessor
    {
        private readonly HashSet<IModule> candidates;

        public UnusedModulesProcessor(HashSet<IModule> candidates)
        {
            if (candidates == null) 
                throw new ArgumentNullException("candidates");

            this.candidates = candidates;
        }

        
        public bool InteriorShouldBeProcessed(IElement element)
        {
            if (candidates.Count == 0)
                return false;
            return true;
        }

        public void ProcessBeforeInterior(IElement element)
        {
            
        }

        public void ProcessAfterInterior(IElement element)
        {
            Assert.CheckNotNull(element);
            
            foreach (IReference reference in element.GetReferences())
            {
                IDeclaredElement resolved = reference.Resolve().DeclaredElement;
                if(resolved is IConstructor)
                {
                    resolved = resolved.GetContainingType();
                }

                {
                    ITypeElement typeElement = resolved as ITypeElement;

                    if (typeElement != null)
                    {
                        //                    IDeclaredType declaredType = TypeFactory.CreateType(typeElement);

                        IModule module = resolved.Module;
                        if (candidates.Contains(module))
                            candidates.Remove(module);
                    }                    
                }

            }
        }

        public bool ProcessingIsFinished
        {
            get
            {
                return false ;
            }
        }
    }
}