using System;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve.Filters;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl
{
    public interface IFiltersProvider
    {
        ISymbolFilter[] GetSymbolFilters(out int mustRun, FilteredReferenceName filteredReferenceName);
    }

    public class FiltersArray : IFiltersProvider
    {
        private readonly ISymbolFilter[] filters = null;

        public FiltersArray(ISymbolFilter[] filters)
        {
            this.filters = filters;
        }

        #region IFiltersProvider Members

        public ISymbolFilter[] GetSymbolFilters(out int mustRun, FilteredReferenceName filteredReferenceName)
        {
            if (filters == null || filters.Length == 0)
            {
                mustRun = 0;
                return EmptyArray<ISymbolFilter>.Instance;
            }
            else
            {
                mustRun = filters.Length;
                return filters;
            }
        }

        #endregion
    }

    public class FilteredReferenceName : ReferenceName
    {
        private readonly IFiltersProvider filtersProvider;

        public FilteredReferenceName(TreeElement identifier, IQualifier qualifier, IFiltersProvider filtersProvider) : base(identifier, qualifier)
        {
            if (filtersProvider == null) throw new ArgumentNullException("filtersProvider");
            this.filtersProvider = filtersProvider;
        }

        public FilteredReferenceName(IQualifier qualifier, IFiltersProvider filtersProvider) : base(qualifier)
        {
            if (filtersProvider == null) throw new ArgumentNullException("filtersProvider");
            this.filtersProvider = filtersProvider;
        }

        public FilteredReferenceName(IFiltersProvider filtersProvider)
        {
            if (filtersProvider == null) throw new ArgumentNullException("filtersProvider");
            this.filtersProvider = filtersProvider;
        }

        public FilteredReferenceName(TreeElement identifier, IFiltersProvider filtersProvider) : base(identifier)
        {
            this.filtersProvider = filtersProvider;
        }

        public override ISymbolFilter[] GetSymbolFilters(out int mustRun)
        {
//            string name = this.GetName();
//            TypeParameterNumberFilter filter = new TypeParameterNumberFilter(this.TypeArgumentsNumber);
//            ExactNameFilter nameFilter = new ExactNameFilter(name);
//            return new ISymbolFilter[] { nameFilter, TypeOrNamespaceFilter.INSTANCE, filter, PreImportedElementsFilter.INSTANCE, new AccessRightsFilter(this), ImportedElementsFilter.INSTANCE, OverriddenFilter.INSTANCE, new ExplicitSubstitutionProvider(this.TypeArguments), new LocalNamesFilter(this.GetProject(), true) };

            return filtersProvider.GetSymbolFilters(out mustRun, this);
        }        
    }
}