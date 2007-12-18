using System.Collections.Generic;
using System.Drawing;
using Arp.log4net.Psi;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Caches2;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Xml;
using JetBrains.Util;

namespace Arp.log4net.Services
{

    [XmlLanguage, ProjectFileLanguageService(new string[] {".l4n", ".config", ".log4net", ".log4net.xml" }, IsDefault = true)]
    public class L4NProjectFileLanguageService : IProjectFileLanguageService
    {
        public static ProjectFileType L4N = new ProjectFileType("L4N");

        private static readonly PsiLanguageType[] POSSIBLE_PSI_LANGUAGE_TYPES = new PsiLanguageType[] { L4NLanguageService.L4N };


        public PsiLanguageType GetPsiLanguageType(IProjectFile file)
        {
            if (file == null)
            {
                return PsiLanguageType.UNKNOWN;
            }
            
            
            return GetPsiLanguageType(file.LanguageType);
        }

        public PsiLanguageType GetPsiLanguageType(ProjectFileType languageType)
        {
            if (languageType != L4N)
            {
                return PsiLanguageType.UNKNOWN;
            }
            return L4NLanguageService.L4N;
        }

        ///<summary>
        ///
        ///            Creates lexer, that corresponds to source file text (NOT generated!) 
        ///            
        ///</summary>
        ///
        public ILexerFactory CreateLexer(ProjectFileType languageType, IBuffer buffer)
        {
            if (languageType != L4N)
                return null;

            return new XmlLexerFactory(L4NLanguageService.L4N);
        }

        private XmlProjectFileLanguageService GetXmlProjectFileLanguageService()
        {
            return (XmlProjectFileLanguageService)ProjectFileLanguageServiceManager.Instance.GetService(XmlProjectFileLanguageService.XML);
        }

        public ProjectFileType LanguageType
        {
            get { return L4N; }
        }

        public Image Icon
        {
            get { return null; }
        }

        public ICollection<PsiLanguageType> PossiblePsiLanguageTypes
        {
            get { return POSSIBLE_PSI_LANGUAGE_TYPES; }
        }

        public IWordIndexLanguageProvider WordIndexLanguageProvider
        {
            get
            {
                XmlProjectFileLanguageService xmlProjectFileLanguageService = GetXmlProjectFileLanguageService();
                return xmlProjectFileLanguageService.WordIndexLanguageProvider;
            }
        }



    }
}