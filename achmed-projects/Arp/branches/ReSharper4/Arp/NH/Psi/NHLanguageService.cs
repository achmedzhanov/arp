using System.Drawing;
using Arp.NH.Psi.Parsing;
using Arp.NH.Psi.Search;
using JetBrains.ComponentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CodeStyle;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Caches2;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Application;
using JetBrains.Util;

namespace Arp.NH.Psi
{
    [ShellComponentImplementation, LanguageService, ShellComponentInterface(ProgramConfigurations.ALL)]
    public class NHLanguageService : XmlLanguageServiceBase, IShellComponent
    {
        public const string NH_STRING = "NH";
        
        public static readonly PsiLanguageType NH = new PsiLanguageType(NH_STRING);


        public NHLanguageService()
            : base(NH)
        {

        }


        public void Init()
        {
            // do nothing
        }

        ///<summary>
        ///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ///</summary>
        ///<filterpriority>2</filterpriority>
        public void Dispose()
        {
            // do nothing
        }



        public override ILanguageCacheProvider CacheProvider
        {
            get { return null; }
        }

        public override ITypePresenter TypePresenter
        {
            get { return null; }
        }


        ///<summary>
        ///
        ///            Check if the PSI caches should be invalidated on this change
        ///            
        ///</summary>
        ///
        public override bool ShouldInvalidatePsiCache(IElement element, PsiChangedElementType elementType)
        {
//            return base.ShouldInvalidatePsiCache(element, elementType);
            return true;
        }

        public override IParser CreateParser(ILexer lexer, ISolution solution, IProject project,
                                             CheckForInterrupt checkForInterrupt)
        {
          
            return new NHParser(lexer, checkForInterrupt);
        }

        class NHParser : XmlLanguageServiceBase.XmlParser
        {
            public NHParser(ILexer lexer, CheckForInterrupt checkForInterrupt)
                : base(lexer, checkForInterrupt)
            {
            }

            public override IFile ParseFile()
            {
                var file = new NHTreeBuilder(NHElementFactory.Instance, base.myCheckForInterrupt).BuildXml(base.myLexer);
                return file;
            }
        }


        #region test overrides

        // TODO

        public override ILanguageSpecificSearcherFactory LanguageSpecificSearcherFactory
        {
            get
            {
                return NHSearcherFactory.Instance;
            }
        }

        #endregion

    }
}