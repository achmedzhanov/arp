using System.Collections.Generic;
using System.Drawing;
using Arp.Common.Assertions;
using Arp.NH.Psi;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Caches2;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Xml;
using JetBrains.Util;

namespace Arp.NH.Services
{
    [XmlLanguage, ProjectFileLanguageService(new string[] { ".hbm.xml" }, IsDefault = true)]
    public class NHProjectFileLanguageService : IProjectFileLanguageService
    {
        public static ProjectFileType NH = new ProjectFileType("NH");

        private static readonly PsiLanguageType[] POSSIBLE_PSI_LANGUAGE_TYPES = new PsiLanguageType[] { NHLanguageService.NH };


        public PsiLanguageType GetPsiLanguageType(IProjectFile file)
        {
            if (file == null)
            {
                return PsiLanguageType.UNKNOWN;
            }

            Assert.CheckNotNull(file.Name);
            if (file.Name.EndsWith(".hbm.xml"))
                return NHLanguageService.NH;
        
            return GetPsiLanguageType(file.LanguageType);
        }

        public PsiLanguageType GetPsiLanguageType(ProjectFileType languageType)
        {
            if (languageType != NH)
            {
                return PsiLanguageType.UNKNOWN;
            }
            return NHLanguageService.NH;
        }

        ///<summary>
        ///
        ///            Creates lexer, that corresponds to source file text (NOT generated!) 
        ///            
        ///</summary>
        ///
        public ILexerFactory CreateLexer(ProjectFileType languageType, IBuffer buffer)
        {
            if (languageType != NH)
                return null;

            return new XmlLexerFactory(NHLanguageService.NH);
        }

        private XmlProjectFileLanguageService GetXmlProjectFileLanguageService()
        {
            return (XmlProjectFileLanguageService)ProjectFileLanguageServiceManager.Instance.GetService(XmlProjectFileLanguageService.XML);
        }

        public ProjectFileType LanguageType
        {
            get { return NH; }
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