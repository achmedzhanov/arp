using Arp.Common.Psi.Services.CodeCompletion;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.TextControl;

namespace Arp.NH.Services.CodeCompletion
{
    public class NHCodeCompletionContext : BaseCodeCompletionContext
    {
        public NHCodeCompletionContext(ISolution solution, ITextControl textControl) : base(solution, textControl)
        {
        }

        protected override void InitilizeRules()
        {
            // there is no rules
        }
    }
}