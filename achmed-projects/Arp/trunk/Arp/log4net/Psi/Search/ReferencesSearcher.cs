using System;
using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Finder;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace Arp.log4net.Psi.Search
{
    public class ReferencesSearcher : ILanguageSpecificSearcher, IReferenceSearchContext
    {
        private readonly HashSet<IDeclaredElement> elements;
        private readonly FindResultConsumer consumer;
        private readonly List<string> elementNames = new List<string>();


        public ReferencesSearcher(ICollection<IDeclaredElement> elements, FindResultConsumer consumer)
        {
            this.elements = new HashSet<IDeclaredElement>(elements);
            this.consumer = consumer;

            foreach (IDeclaredElement element in elements)
            {
                elementNames.Add(element.ShortName);
            }
        }

        #region ILanguageSpecificSearcher

        ///<returns>
        ///True if further search is not needed
        ///</returns>
        ///
        public bool ProcessProjectItem(IProjectFile projectItem)
        {
            IFile psiFile = PsiManager.GetInstance(projectItem.GetSolution()).GetPsiFile(projectItem);
            if (psiFile != null)
            {
                return this.ProcessElement(psiFile);
            }
            return false;

        }

        ///<returns>
        ///True if further search is not needed
        ///</returns>
        ///
        public bool ProcessElement(IElement element)
        {
            Logger.Assert(element != null, "The condition (element != null) is false.");
            return (new ReferenceSearchSourceFileProcessor(this, element.ToTreeNode()).Run() == FindExecution.Stop);

        }

        #endregion

        #region IReferenceSearchContext

        public IList<string> GetNamesToSearchForInProjectFile(IProjectFile item)
        {
            return elementNames;
        }

        public HashSet<IDeclaredElement> Elements
        {
            get
            {
                return elements;
            }
        }

        public bool HasUnnamedElement
        {
            get { return false; }
        }

        public FindResultConsumer ResultConsumer
        {
            get { return consumer;   }
        }

        #endregion

    }
}