using System.Drawing;
using Arp.log4net.Psi.Parsing;
using Arp.log4net.Psi.Search;
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
using JetBrains.Shell;
using JetBrains.Util;

namespace Arp.log4net.Psi
{
    [ShellComponentImplementation, LanguageService, ShellComponentInterface(ProgramConfigurations.ALL)]
    public class L4NLanguageService : XmlLanguageServiceBase, IShellComponent
    {
        public const string L4N_STRING = "L4N";
        
        public static readonly PsiLanguageType L4N = new PsiLanguageType(L4N_STRING);
        
        
        public L4NLanguageService()
            : base(L4N)
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
            return new L4NParser(lexer, checkForInterrupt);
        }

        class L4NParser : XmlLanguageServiceBase.XmlParser
        {
            public L4NParser(ILexer lexer, CheckForInterrupt checkForInterrupt) : base(lexer, checkForInterrupt)
            {
            }

            public override IFileNode ParseFile()
            {
                IXmlFile file = new L4NTreeBuilder(L4NElementFactory.Instance, base.myCheckForInterrupt).BuildXml(base.myLexer);
                if (file == null)
                {
                    return null;
                }
                return file.ToTreeNode();

            }
        }


        #region test overrides

        public override IRawLexer CreateLexer(IBuffer buffer)
        {
            return base.CreateLexer(buffer);
        }

        public override ILexer CreateFilteringLexer(ILexer lexer)
        {
            return base.CreateFilteringLexer(lexer);
        }

        public override string GetTokenReprByTokenType(TokenNodeType token)
        {
            return base.GetTokenReprByTokenType(token);
        }

        public override string GetCommentText(string commentTokenText)
        {
            return base.GetCommentText(commentTokenText);
        }

        public override bool IsFilteredNode(ITreeNode node)
        {
            return base.IsFilteredNode(node);
        }

        public override PsiLanguageType LanguageType
        {
            get { return base.LanguageType; }
        }

        public override bool IsValidName(DeclaredElementType elementType, string name)
        {
            return base.IsValidName(elementType, name);
        }

        public override bool IsTokenSkipped(ITokenNode node)
        {
            return base.IsTokenSkipped(node);
        }

        public override string[] EnumerateParserCapabilities()
        {
            return base.EnumerateParserCapabilities();
        }

        public override ITreeNode ParseUsingCapability(string text, string capability, ISolution solution,
                                                       IProject project)
        {
            return base.ParseUsingCapability(text, capability, solution, project);
        }

        public override PreProcessingDirective[] GetDefines(IProject project)
        {
            return base.GetDefines(project);
        }

        public override IReferenceContextCodec CreateReferenceContextCodec()
        {
            return base.CreateReferenceContextCodec();
        }

        ///<summary>
        ///
        ///            Get the language-specific image for the given declared element.
        ///            Returned image should be without extensions
        ///            
        ///</summary>
        ///
        ///<param name="declaredElement"></param>
        ///<returns>
        ///Image, or 
        ///<c>null</c> if to use default image
        ///</returns>
        ///
        public override Image GetDeclaredElementImage(IDeclaredElement declaredElement)
        {
            return base.GetDeclaredElementImage(declaredElement);
        }

        ///<summary>
        ///
        ///            Check if the given type member is visible in this language
        ///            
        ///</summary>
        ///
        ///<returns>
        ///
        ///</returns>
        ///
        public override bool IsTypeMemberVisible(ITypeMember member)
        {
            return base.IsTypeMemberVisible(member);
        }

        public override ReferenceAccessType GetReferenceAccessType(IReference reference)
        {
            return base.GetReferenceAccessType(reference);
        }

        public override ReferenceAccessType GetReferenceAccessType(IDeclaredElement target, IReference reference)
        {
            return base.GetReferenceAccessType(target, reference);
        }

        ///<summary>
        ///
        ///            Get the code formatter
        ///            
        ///</summary>
        ///
        public override CodeFormatter CodeFormatter
        {
            get { return base.CodeFormatter; }
        }

        public override IConstantValueService ConstantValueService
        {
            get { return base.ConstantValueService; }
        }

        public override ITypeConversionRule TypeConversionRule
        {
            get { return base.TypeConversionRule; }
        }

        public override ILanguageSpecificSearcherFactory LanguageSpecificSearcherFactory
        {
            get
            {
//                return base.LanguageSpecificSearcherFactory;

                return L4NSearcherFactory.Instance;
                }
        }

        ///<summary>
        ///
        ///            Provide language-specific presentation of the declared elements.
        ///            Returns 
        ///<c>null</c> if default presenter should be used
        ///            
        ///</summary>
        ///
        ///            You should never call this method directly. Use <cref>DeclaredElementPresenter.Format</cref>&gt; instead.
        ///        
        public override IDeclaredElementPresenter DeclaredElementPresenter
        {
            get { return base.DeclaredElementPresenter; }
        }

        #endregion


    }
}