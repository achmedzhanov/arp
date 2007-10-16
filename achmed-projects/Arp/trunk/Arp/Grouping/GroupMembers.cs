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
using JetBrains.ReSharper.Psi.CSharp.Impl;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.TextControl;
using JetBrains.Shell;
using JetBrains.Util;

namespace Arp.Grouping
{
//    [ContextAction(Name = "Group Fileds")]
    [Obsolete]
    public class GroupMembers : OneItemContextActionBase
    {
        private const string DELEGATES = "Delegates";
        private const string TYPES = "Types";
        private const string FIELDS = "Fields";
        private const string EVENTS = "Events";
        private const string CONSTRUCTORS = "Constructors";
        private const string PROPERTIES = "Properties";
        private const string METHODS = "Methods";
        private const string HANDLERS = "Handlers";

        #region Fields
        #endregion


        #region Constructor

        public GroupMembers(ISolution solution, ITextControl textControl) : base(solution, textControl)
        {
        }

        #endregion

        protected override void ExecuteInternal()
        {
            ITypeDeclaration declaration = GetTypeDeclaration();
            Assert.CheckNotNull(declaration);
            
            List<IGroupingOption> options = new List<IGroupingOption>();

            options.Add(new PredicateGrouping(declaration, DELEGATES, delegate(ITypeMemberDeclaration obj)
                                                                          {
                                                                              return obj is IDelegateDeclaration;
                                                                          }));


            options.Add(new PredicateGrouping(declaration, TYPES, delegate(ITypeMemberDeclaration obj)
                                                                      {
                                                                          return obj is ITypeDeclaration;
                                                                      }));

            
            options.Add(new PredicateGrouping(declaration, FIELDS, delegate(ITypeMemberDeclaration obj)
                                                                       {
                                                                           return obj is IFieldDeclaration;
                                                                       }));

            options.Add(new PredicateGrouping(declaration, EVENTS, delegate(ITypeMemberDeclaration obj)
                                                                       {
                                                                           return obj is IEventDeclaration;
                                                                       }));            

            options.Add(new PredicateGrouping(declaration, CONSTRUCTORS, delegate(ITypeMemberDeclaration obj)
                                                                             {
                                                                                 return obj is IConstructorDeclaration;
                                                                             }));

            options.Add(new PredicateGrouping(declaration, PROPERTIES, delegate(ITypeMemberDeclaration obj)
                                                                           {
                                                                               return obj is IPropertyDeclaration;
                                                                           }));
//
//            options.Add(new PredicateGrouping(declaration, HANDLERS, delegate(ITypeMemberDeclaration obj)
//                                                                         {
//                                                                             IMethodDeclaration methodDeclaration = obj as IMethodDeclaration;
//                                                                             if (methodDeclaration == null)
//                                                                                 return false;
//
//                                                                             IList<IRegularParameterDeclaration> parameters = methodDeclaration.ParameterDeclarations;
//                                                                             if (parameters == null)
//                                                                                 return false;
//
//                                                                             if (parameters.Count != 2)
//                                                                                 return false;
//
//
//
//                                                                        }));
            
            options.Add(new PredicateGrouping(declaration, METHODS, delegate(ITypeMemberDeclaration obj)
                                                                        {
                                                                            return obj is IMethodDeclaration;
                                                                        }));


            
            RegionDecorator regionDecorator = new RegionDecorator(declaration.ToTreeNode());

            using (WriteLockCookie cookie = WriteLockCookie.Create())
            {
                // grouping

                IList<ITypeMemberDeclaration> declarations = declaration.MemberDeclarations;
                foreach (ITypeMemberDeclaration memberDeclaration in declarations)
                {
                    foreach (IGroupingOption option in options)
                    {
                        if (option.IsAccept(memberDeclaration))
                            break;
                    }
                }

                foreach (IGroupingOption option in options)
                {
                    option.Execute();
                }

                
                // reordering
                List<string> orderedRegionTypes = new List<string>();
                orderedRegionTypes.Add(DELEGATES);
                orderedRegionTypes.Add(TYPES);
                orderedRegionTypes.Add(FIELDS);
                orderedRegionTypes.Add(EVENTS);
                orderedRegionTypes.Add(CONSTRUCTORS);
                orderedRegionTypes.Add(PROPERTIES);
                orderedRegionTypes.Add(METHODS);
                orderedRegionTypes.Add(HANDLERS);

                Assert.Check(orderedRegionTypes.Count == options.Count);

                ReorderRegions reorderRegions = new ReorderRegions(declaration, orderedRegionTypes);
                
                reorderRegions.Reorder();

            }

        }

        protected override bool IsAvailableInternal()
        {
            return GetTypeDeclaration() != null;
        }


        public override string Text
        {
            get { return "Group members"; }
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