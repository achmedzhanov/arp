using System.Collections.Generic;
using Arp.NH.Psi;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Naming;
using JetBrains.ReSharper.Psi.Naming.DefaultNamingStyle;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace Arp.NH.Services.Refactorings
{
    [NamingStyle(LanguageId = NHLanguageService.NH_STRING, Name = "Default")]
    public class NHNamingStyle : DefaultNamingStyle
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