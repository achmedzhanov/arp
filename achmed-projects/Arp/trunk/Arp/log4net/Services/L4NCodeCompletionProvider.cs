using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.CodeInsight.Services.CodeCompletion;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.CodeInsight.Services.Xml.CodeCompletion;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.ReSharper.TextControl;
using JetBrains.Util;

namespace Arp.log4net.Services
{
    [LanguageSpecificImplementation("L4N", typeof(ICodeCompletionServiceProvider))]
    public class L4NCodeCompletionProvider : ICodeCompletionServiceProvider
    {
        ///<summary>
        ///
        ///            Checks if the given code completion is available at this point
        ///            
        ///</summary>
        ///
        public bool IsAvailable(ISolution solution, ITextControl textControl, CodeCompletionType codeCompletionType)
        {
            L4NCodeCompletionContext context = CreateContext(solution, textControl, codeCompletionType);
            if (context == null)
                return false;
            return context.IsAvailable();
        }

        protected L4NCodeCompletionContext CreateContext(ISolution solution, ITextControl textControl, CodeCompletionType codeCompletionType)
        {
            if (codeCompletionType == CodeCompletionType.BasicCompletion)
                return new L4NCodeCompletionContext(solution, textControl);
            return null;
        }

        ///<summary>
        ///
        ///            Execute given code completion
        ///            
        ///</summary>
        ///
        public void Execute(ISolution solution, ITextControl textControl, CodeCompletionType codeCompletionType,
                            CompletionHandler itemCompleted)
        {
            L4NCodeCompletionContext context = CreateContext(solution, textControl, codeCompletionType);
            IList<ILookupItem> items = context.EvaluateLookupItems();

            LookupWindowOptions options = new LookupWindowOptions();
            options.PreferencePolicy = null;
            options.CaseSensitive = false;
            options.ShowParameterInfo = LookupOptions.Instance.ShowSummary.Value;
            options.InitialPrefixIsShortest = true;
            options.AutocompleteCommonPrefix = CodeCompletionSettings.Instance.AutocompleteCommonPrefix.Value;

            TextRange prefixRange = context.PrefixRange;
            ILookup lookup = LookupWindowManager.Instance.CreateLookup(context.TextControl, prefixRange, items, context.Solution, options);

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