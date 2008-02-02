using System;
using System.Collections;
using Arp.Assertions;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp.QuickFixes;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.ReSharper.TextControl;
using JetBrains.Shell;
using JetBrains.Util;

namespace Arp.log4net.Services.QuickFixes
{
    public class CreateMissedParameterFix : IQuickFix, IBulbItem
    {
        private readonly MissedParameterError missedParameterError;
        private DocumentRange headerNameRange;

        public CreateMissedParameterFix(MissedParameterError missedParameterError)
        {
            if (missedParameterError == null)
                throw new ArgumentNullException("missedParameterError");

            this.missedParameterError = missedParameterError;
            headerNameRange = ((IXmlTag) missedParameterError.Element).ToTreeNode().Header.Name.GetDocumentRange();
        }

        public string Text
        {
            get
            {
                return
                    string.Format("Create {0} {1}",
                                  (missedParameterError.Descriptor.IsAttribute ? "attribute" : "element"),
                                  missedParameterError.Descriptor.Name);
            }
        }

        #region IBulbAction Members

        ///<summary>
        ///
        ///            Check if this action is available at the constructed context.
        ///            Actions could store precalculated info in <paramref name="cache" /> to share it between different actions
        ///            
        ///</summary>
        ///
        public bool IsAvailable(IUserDataHolder cache)
        {
            return true;
        }

        public IBulbItem[] Items
        {
            get { return new IBulbItem[] {this}; }
        }

        #endregion

        public void Execute(ISolution solution, ITextControl textControl)
        {
            // TODO extract add attribute to another class
            // method name  IDeclaredParameter AddDeclaredParameter()
            
            if (missedParameterError.Descriptor.IsAttribute)
            {
                IDocument document = textControl.Document;
                using (ModificationCookie cookie = document.EnsureWritable())
                {
                    int navigationOffset = -1;

                    try
                    {
                        CommandProcessor.Instance.BeginCommand("TextControl:AddAttribute");
                        string toInsert = " " + this.missedParameterError.Descriptor.Name + "=\"\"";
                        document.InsertText(this.headerNameRange.TextRange.EndOffset, toInsert);
                        navigationOffset = (this.headerNameRange.TextRange.EndOffset + toInsert.Length) - 1;
                    }
                    finally
                    {
                        CommandProcessor.Instance.EndCommand();
                    }
                    if (navigationOffset != -1)
                    {
                        textControl.CaretModel.MoveTo(navigationOffset, TextControlScrollType.MAKE_VISIBLE);
                        // todo run Completion Action
                    }
                }
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}