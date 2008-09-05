using System;
using System.Collections.Generic;
using System.Globalization;
using Arp.Common.Assertions;
using Arp.Common.Utils;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Caches;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace Arp.NH.Psi
{
    public class NHGlobalQualifier : IQualifier
    {
        private readonly IElement element;
        private GlobalQualifier globalQualifier;
        private readonly Dictionary<string, string> aliasesTypes = 
            new Dictionary<string, string>
                {
                    {"Int32", typeof(int).FullName},
                    {"int", typeof(int).FullName},
                    {"binary", typeof(Byte[]).FullName},
                    {"boolean", typeof(bool).FullName},
                    {"byte", typeof(byte).FullName},
                    {"character", typeof(char).FullName},
                    {"locale", typeof(CultureInfo).FullName},
                    {"datetime", typeof(DateTime).FullName},
                    {"big_decimal", typeof(decimal).FullName},
                    {"double", typeof(double).FullName},
                    {"guid", typeof(Guid).FullName},
                    {"short", typeof(short).FullName},
                    {"integer", typeof(int).FullName},
                    {"long", typeof(long).FullName},
                    {"float", typeof(Single).FullName},
                    {"string", typeof(string).FullName},
                    {"class", typeof(Type).FullName},
                    {"date", "NHibernateUtil.Date"},
                    {"time", "NHibernateUtil.Time"},
                    {"timestamp", "NHibernateUtil.Timestamp"},
                    {"serializable", "NHibernateUtil.Serializable"},
                    {"true_false", "NHibernateUtil.TrueFalse"},
                    {"yes_no", "NHibernateUtil.YesNo"},
                };

        private PsiManager manager;
        private IProject project;

        public GlobalQualifier GlobalQualifier
        {
            get
            {
                if(globalQualifier == null)
                    globalQualifier = new GlobalQualifier(Manager, Project);
                return globalQualifier;
            }
        }

        public PsiManager Manager
        {
            get
            {
                if (manager == null)
                    manager = element.GetManager();
                Assert.CheckNotNull(manager);
                return manager;
            }
        }

        public IProject Project
        {
            get
            {
                if (project == null)
                    project = element.GetProject();
                Assert.CheckNotNull(project);
                return project;
            }
        }

        public NHGlobalQualifier(IElement element)
        {
            this.element = element;
        }

        public ISymbolTable GetSymbolTable(params string[] referenceNames)
        {
            ISymbolTable tableFromGlobal = GlobalQualifier.GetSymbolTable(referenceNames);
            SymbolTable aliasesTable = new SymbolTable(false);

            foreach (KeyValuePair<string, IDeclaredElement> pair in GetAliases())
            {
                aliasesTable.AddSymbol(pair.Key, pair.Value, EmptySubstitution.INSTANCE, null, 0);
            }

            var table = new MergedTable(tableFromGlobal, aliasesTable);
            return table;
        }

        protected Dictionary<string, IDeclaredElement> GetAliases()
        {
            var ret = new Dictionary<string, IDeclaredElement>();
            foreach (var pair in aliasesTypes)
            {
//                var elements = manager.GetDeclarationsCache(DeclarationsCacheScope.ProjectScope(project, true), true).GetElementsAtQualifiedName(pair.Value);
                var element = manager.GetDeclarationsCache(DeclarationsCacheScope.SolutionScope(project.GetSolution(), true), true).GetTypeElementByCLRName(pair.Value);
                if (element != null)
                    ret[pair.Key] = element;
            }
            return ret;
        }

        public QualifierKind GetKind()
        {
            return GlobalQualifier.GetKind();
        }

        public bool Resolved
        {
            get
            {
                return true;
            }
        }
    }
}