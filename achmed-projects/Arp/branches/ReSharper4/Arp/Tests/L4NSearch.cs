using System;
using System.Collections.Generic;
using Arp.log4net.Psi.Tree;
using Arp.Utils;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.CodeView.Occurences;
using JetBrains.ReSharper.CodeView.Search;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Shell.Progress;
using JetBrains.Util;
using NUnit.Framework;

namespace Arp.Tests
{
    [TestFixture]
    public class L4NSearch : BaseTest
    {
        [Test]
        public void SimpleSearch()
        {
            Pair<ISolution, IProjectFile> solutionHolder =
                CreateSingleFileSolution("log4net", "simpleConfig.config");
            try
            {
                IProjectFile projectFile = solutionHolder.Second;

                IFile file = PsiManager.GetInstance(projectFile.GetSolution()).GetPsiFile(projectFile);
                Assert.IsNotNull(file);
                IAppender appender = ElementUtils.Find<IAppender>(file, delegate(IAppender obj) { return true; });
                Assert.IsNotNull(appender);

                DeclaredElementUsageSearchRequest request =
                    new DeclaredElementUsageSearchRequest((IDeclaredElement)appender, SearchPattern.FIND_USAGES, SearchScope.SOLUTION, solutionHolder.First);

                ICollection<IDeclaredElement> searchElements = request.SearchElements;
                Assert.AreEqual(1, searchElements.Count);

                ICollection<IOccurence> result = request.Search(NullProgressIndicator.INSTANCE);

                Assert.AreEqual(1, result.Count);
            }
            finally
            {
                SolutionManager.Instance.CloseSolution(solutionHolder.First);
            }
        }
    }
}