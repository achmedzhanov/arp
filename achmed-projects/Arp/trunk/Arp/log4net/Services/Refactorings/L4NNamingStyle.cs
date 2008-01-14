using System.Collections.Generic;
using Arp.log4net.Psi;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Naming;
using JetBrains.ReSharper.Psi.Naming.DefaultNamingStyle;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace Arp.log4net.Services.Refactorings
{
    [NamingStyle(LanguageId = L4NLanguageService.L4N_STRING, Name = "Default")]
    public class L4NNamingStyle : DefaultNamingStyle
    {
        // Methods
        public override string[] GetBaseNames(IType type)
        {
            return new string[0];
        }

        public override string GetRoot(IExpression expression)
        {
            return string.Empty;
        }

        protected override DefaultNamingStyleSettings GetSettings(ISolution solution)
        {
            return new DefaultNamingStyleSettings();
        }

        // Properties
        public override PsiLanguageType Language
        {
            get
            {
                return L4NLanguageService.L4N;
            }
        }

        protected override IDictionary<string, string> OurNamesMap
        {
            get
            {
                return new Dictionary<string, string>();
            }
        }

        protected override ICollection<string> OurReservedNames
        {
            get
            {
                return EmptyArray<string>.Instance;
            }
        }
    }
}