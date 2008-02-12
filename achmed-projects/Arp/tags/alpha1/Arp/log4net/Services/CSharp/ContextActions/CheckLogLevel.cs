using System;
using System.Collections.Generic;
using System.Text;
using Arp.Assertions;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.ContextActions;
using JetBrains.ReSharper.Daemon.CSharp.ContextActions.Util;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.TextControl;
using JetBrains.Util;

namespace Arp.log4net.Services.CSharp.ContextActions
{
    [ContextAction(Group = "log4net", Name = "Check log level", Description = "Puts the current log statement under an 'if' statement that performs log level check.")]
    public class CheckLogLevel : OneItemContextActionBase
    {
        public CheckLogLevel(ISolution solution, ITextControl textControl) : base(solution, textControl)
        {

        }

        protected override void ExecuteInternal()
        {
            LogCallAnalayResult result = GetLogCallAnalayResult();
            
            Assert.Check(result.IsLogCall, "result.IsLogCall");
            Assert.CheckNotNull(result.InvocationExpression);
            IReferenceExpression referenceExpression = result.InvocationExpression.InvokedExpression as IReferenceExpression;
            Assert.CheckNotNull(referenceExpression, "referenceExpression");
            IStatement statement = referenceExpression.GetContainingElement<IStatement>(true);
            Assert.CheckNotNull(statement);
            IStatement copy = (IStatement)statement.Copy(statement.ToTreeNode().Parent);

            IIfStatement ifStatement = (IIfStatement)Provider.ElementFactory.CreateStatement("if($0.Is$1Enabled) $2", result.LoggerExpression, result.Level, result.InvocationExpression);
            Assert.CheckNotNull(ifStatement, "ifStatement");
            IIfStatement createdStatement = (IIfStatement)statement.ReplaceBy(ifStatement);
            FormatWithDefaultProfile(createdStatement);
            createdStatement.Then.ReplaceBy(copy);
        }

        ///<summary>
        ///
        ///            Called to check if ContextAction is available.
        ///            ReadLock is taken
        ///            Will not be called if PsiManager, ProjectFile of Solution == null
        ///            
        ///</summary>
        ///
        ///<returns>
        ///
        ///</returns>
        ///
        protected override bool IsAvailableInternal()
        {
            // TODO check already exists if statement
            
            if (GetLogCallAnalayResult().IsLogCall)
                return true;

            return false;
        }

        LogCallAnalayResult GetLogCallAnalayResult()
        {
            IInvocationExpression invocationExpression = GetSelectedElement<IInvocationExpression>(true);
            LogCallAnalayResult result = Analyze(invocationExpression);
            return result;
        }

        private LogCallAnalayResult Analyze(IInvocationExpression invocationExpression)
        { // NOTE it works ! :)
            if (invocationExpression == null)
            {
                return LogCallAnalayResult.NotLogCall;
            }
            
            IReferenceExpression referenceExpression = invocationExpression.InvokedExpression as IReferenceExpression;
            if (referenceExpression == null)
            {
                return LogCallAnalayResult.NotLogCall;
            }

            IMethod method = referenceExpression.Reference.Resolve().DeclaredElement as IMethod;
            
            HashSet<string> logMethodNames = new HashSet<string>( new string [] 
                {"Debug", "Error", "Fatal", "Info",
                "DebugFormat", "ErrorFormat", "FatalFormat", "InfoFormat"});

            if ((method == null) || (!logMethodNames.Contains(method.ShortName)))
            {
                return LogCallAnalayResult.NotLogCall;
            }
            
            ICSharpExpression qualifierExpression = referenceExpression.QualifierExpression;
            if (qualifierExpression == null)
                return LogCallAnalayResult.NotLogCall;

            IDeclaredType expressionDeclaredType = qualifierExpression.Type() as IDeclaredType;
            if (expressionDeclaredType == null)
                return LogCallAnalayResult.NotLogCall;

            if(expressionDeclaredType.GetCLRName() != "log4net.ILog")
                return LogCallAnalayResult.NotLogCall;

            return new LogCallAnalayResult(method.ShortName, qualifierExpression.GetText(), invocationExpression);

//            return ((qualifierExpression != null) && qualifierExpression.Type().Equals(expression.GetManager().PredefinedType.String));            

        }

        public override string Text
        {
            get
            {
                LogCallAnalayResult result = GetLogCallAnalayResult();
                string text = string.Format("if({0}.Is{1}Enabled)", result.LoggerExpression, result.Level);
                return "Check log level " + text;
            }
        }
    }

    internal class LogCallAnalayResult
    {
        private const string FORMAT_SUFFIX = "Format";
        private readonly bool isLogCall;
        private readonly string methodName;
        private readonly string loggerExpression;
        private readonly IInvocationExpression invocationExpression;

        public static readonly LogCallAnalayResult NotLogCall = new LogCallAnalayResult();

        protected LogCallAnalayResult()
        {
            isLogCall = false;
        }

        public LogCallAnalayResult(string methodName, string loggerExpression, IInvocationExpression invocationExpression)
        {
            if (methodName == null) throw new ArgumentNullException("methodName");
            if (loggerExpression == null) throw new ArgumentNullException("loggerExpression");
            if (invocationExpression == null) throw new ArgumentNullException("invocationExpression");
            
            isLogCall = true;
            this.methodName = methodName;
            this.loggerExpression = loggerExpression;
            this.invocationExpression = invocationExpression;
        }

        public bool IsLogCall
        {
            get
            {
                return isLogCall;
            }
        }

        public string MethodName
        {
            get { return methodName; }
        }

        public string Level
        {
            get
            {
                if (methodName.EndsWith(FORMAT_SUFFIX))
                {
                    Assert.Check((methodName.Length - FORMAT_SUFFIX.Length) > 0);
                    return methodName.Substring(0, methodName.Length - FORMAT_SUFFIX.Length);
                }
                
                return methodName;
            }
        }

        public string LoggerExpression
        {
            get { return loggerExpression; }
        }


        public IInvocationExpression InvocationExpression
        {
            get { return invocationExpression; }
        }
    }
}
