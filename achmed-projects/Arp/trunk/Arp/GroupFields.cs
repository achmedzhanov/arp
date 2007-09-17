using System;
using System.Collections.Generic;
using System.Text;
using Arp.Assertions;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.CodeInsight.Services.CSharp.Generate.Util;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp.ContextActions.Util;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.TextControl;
using JetBrains.Util;

namespace Arp
{
    [ContextAction(Name = "Group Fileds")]
    public class GroupFields : OneItemContextActionBase
    {
        #region Fields
        #endregion


        #region Constructor

        public GroupFields(ISolution solution, ITextControl textControl) : base(solution, textControl)
        {
        }

        #endregion

        protected override void ExecuteInternal()
        {
            ITypeDeclaration declaration = GetTypeDeclaration();
            Assert.CheckNotNull(declaration);
            
            List<IGroupingOption> options = new List<IGroupingOption>();
            options.Add(new ConstructorsGrouping(declaration));
            
            RegionDecorator regionDecorator = new RegionDecorator(declaration.ToTreeNode());

            IList<ITypeMemberDeclaration> declarations = declaration.MemberDeclarations;
            foreach (ITypeMemberDeclaration memberDeclaration in declarations)
            {
                foreach (IGroupingOption option in options)
                {
                    if(option.IsAccept(memberDeclaration))
                        break;
                }
            }

            foreach (IGroupingOption option in options)
            {
                option.Execute();
            }

        }

        protected override bool IsAvailableInternal()
        {
            return GetTypeDeclaration() != null;
        }


        public override string Text
        {
            get { return "Group fileds"; }
        }

        #region Methods

        private ITypeDeclaration GetTypeDeclaration()
        {
            ITypeDeclaration typeDeclaration = base.GetSelectedElement<ITypeDeclaration>(true);
            if (!(typeDeclaration is IClassDeclaration || typeDeclaration is IInterfaceDeclaration))
                return null;

            return typeDeclaration;
        }

        #endregion

    }
}
