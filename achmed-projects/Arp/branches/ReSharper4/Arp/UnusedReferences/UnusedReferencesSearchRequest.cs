using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.CodeView.Occurences;
using JetBrains.ReSharper.CodeView.Search;
using JetBrains.Shell;
using JetBrains.Shell.Progress;
using JetBrains.Util;

namespace Arp.UnusedReferences
{
    public class UnusedReferencesSearchRequest : SearchRequest
    {
        private readonly ISolution solution;
        private bool emptyResult = false;
        private UnusedReferencesSearchResult results = null;


        public UnusedReferencesSearchRequest(ISolution solution)
        {
            if (solution == null) throw new ArgumentNullException("solution");
            this.solution = solution;
        }


        public bool EmptyResult
        {
            get { return emptyResult; }
        }

        public UnusedReferencesSearchResult Results
        {
            get { return results; }
        }

        public override ICollection<IOccurence> Search(IProgressIndicator progressIndicator)
        {
            UnusedModulesProjectVisitor visitor = new UnusedModulesProjectVisitor(progressIndicator);
            using (ReadLockCookie cookie = ReadLockCookie.Create())
            {
                solution.Accept(visitor);
                emptyResult = visitor.GetTotalUnusedModules() == 0;
                results = visitor.GetSearchResults();
            }
            return EmptyArray<IOccurence>.Instance;
        }

        public override string Title
        {
            get { return "unused modules"; }
        }


        public override ISolution Solution
        {
            get { return solution; }
        }

        public override ICollection SearchTargets
        {
            get
            {
                return EmptyArray<object>.Instance;
            }
        }
    }
}