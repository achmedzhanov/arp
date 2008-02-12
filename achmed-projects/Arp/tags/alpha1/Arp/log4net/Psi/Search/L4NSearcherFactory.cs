using System.Collections.Generic;
using JetBrains.ComponentModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.Shell;

namespace Arp.log4net.Psi.Search
{
    [ShellComponentImplementation, ShellComponentInterface(ProgramConfigurations.ALL)]
    public class L4NSearcherFactory : ILanguageSpecificSearcherFactory, IShellComponent
    {
        private static readonly L4NSearcherFactory instance = new L4NSearcherFactory();

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
            return new string[] {element.ShortName};
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

        public static L4NSearcherFactory Instance
        {
            get { return instance; }
        }
    }
}