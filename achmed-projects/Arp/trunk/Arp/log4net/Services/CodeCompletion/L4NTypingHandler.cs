using System;
using Arp.log4net.Psi;
using JetBrains.ActionManagement;
using JetBrains.ComponentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.CodeInsight.Services.CodeCompletion;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.TextControl;
using JetBrains.Shell;

namespace Arp.log4net.Services.CodeCompletion
{
//    [SolutionComponentInterface(ProgramConfigurations.ALL), SolutionComponentImplementation]
    public class L4NTypingHandler : ITypingHandler, ISolutionComponent
    {
        private readonly ISolution solution;

        public L4NTypingHandler(ISolution solution)
        {
            if (solution == null) throw new ArgumentNullException("solution");
            this.solution = solution;
        }

        #region ITypingHandler

        public void Type(ITextControl textControl, char c, NextHandler nextHandler)
        {
            nextHandler();

            if (!IsSupported(textControl))
                return;

            if (LookupWindowManager.Instance.CurrentLookup == null && c == '<'
                /*&& CodeCompletionSettings.Instance.AutoPopupOnIdentifier.GetValue()*/)
            {
                Action action = null;
                action = delegate
                                    {
                                        CommandProcessor.Instance.BeforeCommandFinished -= action;

                                        // it is really simple
                                        // just execute basic completion
                                        // TODO see SessionStarter and  CodeCompletionSession.ExecuteAsync 
                                        // as example for raise completion (smart or basic depend from context)
                                        // and using resharper options (enable completion, pause etc )

                                        bool interrupted = false;

                                        PsiManager.GetInstance(solution).AsyncCommitAllDocuments(
                                                delegate
                                                    {
                                                        return interrupted;
                                                    }, 
                                                delegate
                                                    {
                                                        if (interrupted)
                                                            return;

                                                        IUpdatableAction updatableAction =
                                                            ActionManager.Instance.GetAction("CompleteCodeBasic");
                                                        ActionManager.Instance.ExecuteActionIfAvailable((IExecutableAction)updatableAction);

                                                    }, 
                                                delegate
                                                    {
                                                        interrupted = true;
                                                    }
                                            );

                                    };
                CommandProcessor.Instance.BeforeCommandFinished  += action;
            }
        }

        #endregion

        #region ISolutionComponent

        public void AfterSolutionOpened()
        {
            TextControlManager.Instance.AddTypingHandler(this);
        }

        public void BeforeSolutionClosed()
        {
            TextControlManager.Instance.RemoveTypingHandler(this);
        }


        public void Init()
        {

        }

        public void Dispose()
        {

        }
        #endregion

        private bool IsSupported(ITextControl control)
        {
            IProjectFile projectFile = DocumentManager.GetInstance(this.solution).GetProjectFile(control.Document);
            return ((projectFile != null) && (projectFile.LanguageType == L4NProjectFileLanguageService.L4N));
        }



    }
}