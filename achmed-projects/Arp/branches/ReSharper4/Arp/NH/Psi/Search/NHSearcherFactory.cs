using System.Collections.Generic;
using Arp.Common.Psi.Search;
using JetBrains.ComponentModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.Shell;

namespace Arp.NH.Psi.Search
{
    [ShellComponentImplementation, ShellComponentInterface(ProgramConfigurations.ALL)]
    public class NHSearcherFactory : ILanguageSpecificSearcherFactory, IShellComponent
    {
        private static readonly NHSearcherFactory instance = new NHSearcherFactory();

        #region IShellComponent

        public void Init()
        {

        }

        ///<summary>
        ///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ///</summary>
        ///<filterpriority>2</filterpriority>
        public void Dispose()
        {

        }

        #endregion

        #region ILanguageSpecificSearcherFactory

        public ICollection<string> GetAllPossibleAliases(IDeclaredElement element)
        {
            return new string[] { element.ShortName };
        }

        public bool IsUnnamed(IDeclaredElement element)
        {
            return false;
        }

        public ILanguageSpecificSearcher CreateConstructorSpecialReferenceSearcher(
            ICollection<IConstructor> constructors, FindResultConsumer consumer)
        {
            return null;
        }

        public ILanguageSpecificSearcher CreateMethodsReferencedByDelegateSearcher(IDelegate @delegate,
                                                                                   FindResultConsumer consumer)
        {
            return null;
        }

        public ILanguageSpecificSearcher CreateReferenceSearcher(ICollection<IDeclaredElement> elements,
                                                                 FindResultConsumer consumer)
        {
            return new ReferencesSearcher(elements, consumer);
        }

        public ILanguageSpecificSearcher CreateTextOccurenceSeacrher(ICollection<IDeclaredElement> elements,
                                                                     FindResultConsumer consumer)
        {
            return null;
        }

        public ILanguageSpecificSearcher CreateTextOccurenceSeacrher(string subject, FindResultConsumer consumer)
        {
            return null;
        }

        public ILanguageSpecificSearcher CreateLateBoundReferenceSearcher(ICollection<IDeclaredElement> elements,
                                                                          FindResultConsumer consumer)
        {
            return null;
        }

        #endregion

        public static NHSearcherFactory Instance
        {
            get { return instance; }
        }
    }
}