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


        public ICollection<IModule> Candidates
        {
            get
            {
                return new List<IModule>(candidates);
            }
        }

        public UnusedModulesProcessor(ICollection<IModule> candidates)
        {
            if (candidates == null) 
                throw new ArgumentNullException("candidates");

            this.candidates = new HashSet<IModule>(candidates);
        }

        class ElementProcessor
        {
            private readonly HashSet<IModule> scopeModules;
//            private readonly HashSet<ITypeElement> processedElements = new HashSet<ITypeElement>();

            public ElementProcessor(HashSet<IModule> scopeModules)
            {
                this.scopeModules = scopeModules;
            }

            public void ProcessElement(IDeclaredElement declaredElement)
            {
                // TODO check for mscorlib

                ITypeElement typeElement = declaredElement as ITypeElement;

                if (typeElement != null)
                {
//                    IDeclaredType declaredType = TypeFactory.CreateType(typeElement);

                    IModule module = declaredElement.Module;
                    if (scopeModules.Contains(module))
                        DropModule(module);                    
                }

            }

            protected void DropModule(IModule module)
            {
                // TODO support for multimodule assemblies
                scopeModules.Remove(module);
            }
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
                ElementProcessor elementProcessor = new ElementProcessor(candidates);
                elementProcessor.ProcessElement(resolved);
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