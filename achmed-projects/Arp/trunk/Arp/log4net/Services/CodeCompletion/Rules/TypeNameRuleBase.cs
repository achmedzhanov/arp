using System.Collections.Generic;
using Arp.Assertions;
using Arp.log4net.Psi;
using Arp.log4net.Psi.Tree;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.CodeView.Occurences;
using JetBrains.ReSharper.CodeView.Search;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Caches;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace Arp.log4net.Services.CodeCompletion.Rules
{
    public abstract class TypeNameRuleBase : ICodeCompletionRule
    {
        public void Apply(CodeCompletionContext context, IList<ILookupItem> result)
        {
//            ITypeElement[] possibleAppenders = PsiManager.GetInstance(context.Solution).GetDeclarationsCache(
//                DeclarationsCacheScope.SolutionScope(context.Solution, true), true).GetPossibleInheritors("log4net.Appender.IAppender");

            ITypeElement appenderDeclaredElementType = GetBaseType(context);

            if(appenderDeclaredElementType == null)
                return;

            IList<ITypeElement> inheritTypes = GetInheritTypes(appenderDeclaredElementType, context);



            foreach (ITypeElement typeElement in inheritTypes)
            {
                IClass @class = typeElement as IClass;
                if (@class == null)
                    continue;
                
                IModifiersOwner modifiersOwner = typeElement as IModifiersOwner;
                if (modifiersOwner != null)
                {
                    if(modifiersOwner.GetAccessRights() != AccessRights.PUBLIC || modifiersOwner.IsAbstract)
                        continue;
                }

                DeclaredElementFullNameLookupItem item = new DeclaredElementFullNameLookupItem(new DeclaredElementInstance(typeElement), new DeclaredElementLookupItemCreationContext(context.ProjectFile), L4NLanguageService.L4N);
                item.InsertRange = new TextRange(0);
                item.ReplaceRange = new TextRange(0, context.GetPrefix().Length);
                result.Add(item);
            }
        }

        protected abstract ITypeElement GetBaseType(CodeCompletionContext context);

        protected ITypeElement GetTypeElement(CodeCompletionContext context, string typeName)
        {
            IDeclarationsCache cache = PsiManager.GetInstance(context.Solution).GetDeclarationsCache(DeclarationsCacheScope.SolutionScope(context.Solution, true), true);
            return cache.GetTypeElementByCLRName(typeName);
        }

        private IList<ITypeElement> GetInheritTypes(IDeclaredElement declaredElement, CodeCompletionContext context)
        {
            SearchInheritorsRequest request = new SearchInheritorsRequest(declaredElement, SearchScope.SOLUTION_AND_LIBRARIES, context.ProjectFile);
            ICollection<IOccurence> occurences = request.Search();
            List<ITypeElement> elements = new List<ITypeElement>();
            foreach (IOccurence occurence in occurences)
            {
                if(!occurence.IsValid)
                    continue;

                DeclaredElementEnvoy<IDeclaredElement> envoy = occurence.MergeKey as DeclaredElementEnvoy<IDeclaredElement>;
                if (envoy != null)
                {
                    if (!envoy.IsValid)
                        continue;
                    ITypeElement validDeclaredElement = envoy.GetValidDeclaredElement() as ITypeElement;
                    if (validDeclaredElement != null)
                        elements.Add(validDeclaredElement);
                }
                else if (occurence.TypeElement != null)
                {
                    ITypeElement typeElement = occurence.TypeElement.GetValidDeclaredElement();
                    if (typeElement != null)
                        elements.Add(typeElement);                
                }

            }
            return elements;
        }

        public abstract bool IsApplicable(CodeCompletionContext context);

        protected bool IsAttributeOf<T>(string name, CodeCompletionContext context)
        {
            IXmlAttribute attribute = context.Token.GetContainingElement<IXmlAttribute>(false);
            Assert.CheckNotNull(attribute);
            if (attribute.AttributeName != name)
                return false;
            return attribute.GetContainingElement<IXmlTag>(false) is T;
        }

        protected T GetAttributeTag<T>(CodeCompletionContext context) 
            where T : class
        {
            IXmlAttribute attribute = context.Token.GetContainingElement<IXmlAttribute>(false);
            Assert.CheckNotNull(attribute);
            return attribute.GetContainingElement<IXmlTag>(false) as T;
        }

    }
}