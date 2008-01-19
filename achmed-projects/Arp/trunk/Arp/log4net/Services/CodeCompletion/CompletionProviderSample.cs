using System.Collections.Generic;
using JetBrains.ReSharper.CodeInsight.Services.CodeCompletion;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.CodeInsight.Services.Xml.CodeCompletion;
using JetBrains.UI.Interop;
using JetBrains.Util;

namespace Arp.log4net.Services.CodeCompletion
{
    [XmlCodeCompletionProvider]
    public class CompletionProviderSample : XmlCodeCompletionProvider
    {
        public override bool IsAvailable(XmlCodeCompletionContext context, CodeCompletionType codeCompletionType)
        {
            return codeCompletionType == CodeCompletionType.BasicCompletion;
        }

        public override ILookup CreateLookup(XmlCodeCompletionContext context, CodeCompletionType codeCompletionType)
        {

//            Win32Declarations.MessageBeep(MessageBeepType.Error);

            IList<ILookupItem> items = EvaluteItems(context, codeCompletionType);

            LookupWindowOptions options = new LookupWindowOptions();
            options.PreferencePolicy = null;
            options.CaseSensitive = false;
            options.ShowParameterInfo = LookupOptions.Instance.ShowSummary.Value;
            options.InitialPrefixIsShortest = true;
            options.AutocompleteCommonPrefix = CodeCompletionSettings.Instance.AutocompleteCommonPrefix.Value;

            TextRange prefixRange = new TextRange(context.TextControl.CaretModel.Offset, context.TextControl.CaretModel.Offset);
            return LookupWindowManager.Instance.CreateLookup(context.TextControl, prefixRange, items, context.Solution, options);
        }

        private IList<ILookupItem> EvaluteItems(XmlCodeCompletionContext context, CodeCompletionType codeCompletionType)
        {
            List<ILookupItem> ret = new List<ILookupItem>();
            ret.Add(new TextLookupItem("file", "If the path is relative it is taken as relative from the application base directory."));
            ret.Add(new TextLookupItem("appendToFile", "If the value is set to false then the file will be overwritten, if it is set to true then the file will be appended to."));
            return ret;
        }
    }
}