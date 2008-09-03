using System.Collections.Generic;
using System.Drawing;
using Arp.Common.Assertions;
using Arp.NH.Psi;
using JetBrains.Application;
using JetBrains.ComponentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Caches2;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Xml;
using JetBrains.Util;

namespace Arp.NH.Services
{
    [XmlLanguage, ShellComponentInterface(ProgramConfigurations.ALL), ShellComponentImplementation, ProjectFileLanguageService(new [] { HBM_SUFFIX, ".hbmxml" }, IsDefault = true)]
    public class NHProjectFileLanguageService : /*IProjectFileLanguageService*/  IDerivedProjectFileLanguageService , IShellComponent
    {
        public const string HBM_SUFFIX = ".hbm.xml";
        public static ProjectFileType NH = new ProjectFileType("NH");

        private static readonly PsiLanguageType[] POSSIBLE_PSI_LANGUAGE_TYPES = new PsiLanguageType[] { NHLanguageService.NH };

        public void Dispose()
        {
            
        }

        public void Init()
        {
        }

        public ProjectFileType GetProjectFileType(IProjectFile file)
        {
            if (file == null)
            {
                return ProjectFileType.UNKNOWN;
            }

            if (file.Name.EndsWith(HBM_SUFFIX) || file.Name.EndsWith(".hbmxml"))
                return NH;

            return null;
        }

        public PsiLanguageType GetPsiLanguageType(IProjectFile file)
        {
            if (file == null)
            {
                return PsiLanguageType.UNKNOWN;
            }

            Assert.CheckNotNull(file.Name);
            if (file.Name.EndsWith(HBM_SUFFIX))
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