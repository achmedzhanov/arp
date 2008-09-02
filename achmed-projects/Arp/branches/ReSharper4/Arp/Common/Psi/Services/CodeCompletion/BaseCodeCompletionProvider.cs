using System.Collections.Generic;
using Arp.Common.Assertions;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.CodeInsight.Services.CodeCompletion;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.Psi;
using JetBrains.TextControl;
using JetBrains.Util;

namespace Arp.Common.Psi.Services.CodeCompletion
{
    public abstract class BaseCodeCompletionProvider : ICodeCompletionServiceProvider
    {
        ///<summary>
        ///
        ///            Checks if the given code completion is available at this point
        ///            
        ///</summary>
        ///
        public bool IsAvailable(ISolution solution, ITextControl textControl, CodeCompletionType codeCompletionType)
        {
            // TODO introduce rule completion abstraction
            ICodeCompletionContext  context = CreateContexts(solution, textControl, codeCompletionType);

            if (context == null)
                return false;
   
            if (context.IsAvailable())
                return true;

            return false;
        }

        protected abstract ICodeCompletionContext CreateContexts(ISolution solution, ITextControl textControl, CodeCompletionType codeCompletionType);

        ///<summary>
        ///
        ///            Execute given code completion
        ///            
        ///</summary>
        ///
        public void Execute(ISolution solution, ITextControl textControl, CodeCompletionType codeCompletionType,
                            CompletionHandler itemCompleted)
        {
            ICodeCompletionContext context = CreateContexts(solution, textControl, codeCompletionType);
            
            Assert.CheckNotNull(context, "context");

            IList<ILookupItem> items = context.EvaluateLookupItems();

            items = LookupUtil.GetFilteredItems(items, context.GetPrefix(), CodeCompletionSettings.Instance.CaseSensitiveCompletion.Value);

            if(items.Count == 0)
                return;

            LookupWindowOptions options = new LookupWindowOptions();
            options.PreferencePolicy = null;
            options.CaseSensitive = false;
            options.ShowParameterInfo = LookupOptions.Instance.ShowSummary.Value;
            options.InitialPrefixIsShortest = true;
            options.AutocompleteCommonPrefix = CodeCompletionSettings.Instance.AutoCompleteCommonPrefix.Value;
            

            TextRange prefixRange = context.PrefixRange;
            ILookup lookup = LookupWindowManager.Instance.CreateLookup(textControl, prefixRange, items, solution, options);

            lookup.ShowLookup();
        }

        public void BindToType(ITextControl textControl, TextRange textRange, ITypePointer typePointer)
        {
            // do nothing
        }

        public void BindToDeclaredElement<T>(IElementInstancePointer<T> pointer, ITextControl textControl,
                                             TextRange textRange) where T : IDeclaredElement
        {
            // do nothing
        }
    }
}