using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Arp.Assertions;
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
using JetBrains.UI.Interop;

namespace Arp.log4net.Services.CodeCompletion
{
    [SolutionComponentInterface(ProgramConfigurations.ALL), SolutionComponentImplementation]
    public class TypingHandler : ITypingHandler, ISolutionComponent
    {
        private readonly ISolution solution;

        public TypingHandler(ISolution solution)
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


                                                        LookupWindowManager.Instance.AfterLookupWindowShown += Instance_OnAfterLookupWindowShown;
                                                        
                                                        try
                                                        {
                                                            IUpdatableAction updatableAction =
                                                                ActionManager.Instance.GetAction("CompleteCodeBasic");
                                                            ActionManager.Instance.ExecuteActionIfAvailable((IExecutableAction)updatableAction);                                                            
                                                        }
                                                        finally
                                                        {
                                                            LookupWindowManager.Instance.AfterLookupWindowShown -= Instance_OnAfterLookupWindowShown;
                                                        }


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


        private void Instance_OnAfterLookupWindowShown(object sender, EventArgs e)
        {
            IntPtr completorHandle = Win32Declarations.FindWindow("VsCompletorPane", null);
            IntPtr focusHandle = Win32Declarations.GetFocus(); // VsEditPane handler

            const int VK_ESCAPE = 0x1B;

            if (completorHandle == IntPtr.Zero)
                return;

            #region user messages

            int sres = Win32Declarations.SendMessage(focusHandle, Win32Declarations.WM_USER + 42, IntPtr.Zero, IntPtr.Zero);

            bool pres = Win32Declarations.PostMessage(focusHandle, Win32Declarations.WM_USER + 46, IntPtr.Zero, IntPtr.Zero);

            #endregion

        }

        private void CloseVSCompletor()
        {
            IntPtr hWnd = Win32Declarations.FindWindow("VsCompletorPane", null);
            if (hWnd != IntPtr.Zero && LookupWindowManager.Instance.CurrentLookup != null)
            {
                const int VK_ESCAPE = 0x1B;


                IntPtr focusHandle = Win32Declarations.GetFocus();
                IntPtr activeHandle = Win32Declarations.GetActiveWindow();
                string focus = GetClassName(focusHandle);
                string active = GetClassName(activeHandle);

                Debug.Print(string.Format("focus {0}", focus));
                Debug.Print(string.Format("active {0}", active));

//                IntPtr currentActive = Win32Declarations.GetActiveWindow();

                Win32Declarations.SendMessage(hWnd, Win32Declarations.WM_KEYDOWN, (IntPtr)VK_ESCAPE, IntPtr.Zero);
                Win32Declarations.SendMessage(hWnd, Win32Declarations.WM_KEYUP, (IntPtr)VK_ESCAPE, (IntPtr)1);
                Win32Declarations.SendMessage(hWnd, Win32Declarations.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);


                    Lookup lookup = LookupWindowManager.Instance.CurrentLookup as Lookup;
                    if(lookup != null)
                    {
                        Win32Declarations.SetFocus(lookup.Window.Handle);
                        IntPtr lastFocus = Win32Declarations.GetFocus();
//                        IntPtr ret = Win32Declarations.SetActiveWindow(lookup.Window.Handle);
//                        currentActive = Win32Declarations.GetActiveWindow();
                    }
            }
        }

        private string GetClassName(IntPtr currentFocus)
        {
            StringBuilder stringBuilder = new StringBuilder(500);
            Win32Declarations.GetClassName(currentFocus, stringBuilder, 300);
            return stringBuilder.ToString();
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