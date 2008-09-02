using System;
using System.Collections.Generic;
using Arp.Common.Assertions;
using Arp.Common.Psi.Daemon;
using Arp.Common.Utils;
using Arp.NH.Psi.Tree;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace Arp.NH.Stages.DuplicateMapping
{
    public class DiplaceMappingsProcessor : BaseProcessor, IHighlightingProcessor
    {
        #region IRecursiveElementProcessor Members

        public bool InteriorShouldBeProcessed(IElement element)
        {
            return true;
        }

        public void ProcessBeforeInterior(IElement element)
        {
            // do nothing
        }

        public void ProcessAfterInterior(IElement element)
        {
            IClassElement classElement = element as IClassElement;
            if(classElement == null)
                return;

            DuplicatesSearcher propertiesSearcher = new DuplicatesSearcher();
            DuplicatesSearcher columnsSearcher = new DuplicatesSearcher();

            // TODO create visitors
            // TODO visit other elements
            foreach (IPropertyElement property in classElement.Properties)
            {
                #region property name

                AddAttributeValue(propertiesSearcher, property.NameAttribute);

                #endregion

                #region Column

                if(property.ColumnAttribute == null)
                {
                    if(property.Column != null && property.Column.NameAttribute != null)
                        AddAttributeValue(columnsSearcher, property.Column.NameAttribute);
                    else
                        AddAttributeValue(columnsSearcher, property.NameAttribute);
                }
                else
                {
                    AddAttributeValue(columnsSearcher, property.ColumnAttribute);
                }

                #endregion
            }


            HighlightDuplicates(propertiesSearcher,   delegate(IComparable source)
                                                        {
                                                            return string.Format("Member {0} already mapped", source);
                                                        });
            HighlightDuplicates(columnsSearcher, delegate(IComparable source)
                                                     {
                                                         return string.Format("Column {0} already mapped", source);
                                                     });

        }

        private void HighlightDuplicates(DuplicatesSearcher duplicatesSearcher, Transformer<IComparable,string> keyTransformer)
        {
            List<List<DuplicatesSearcher.DuplicatesSearcherItem>> duplicates = duplicatesSearcher.GetDuplicates();
            foreach (List<DuplicatesSearcher.DuplicatesSearcherItem> list in duplicates)
            {
                Assert.Check(list.Count > 1);

                bool skippedFirst = false;
                foreach (DuplicatesSearcher.DuplicatesSearcherItem item in list)
                {
                    if(!skippedFirst)
                    {
                        skippedFirst = true;
                        continue;
                    }

                    highlightings.Add(
                        new HighlightingInfo(item.Range, new DuplicateMappingHighlighting(keyTransformer(item.Key))));

                }

            }
        }

        private void AddAttributeValue(DuplicatesSearcher duplicatesSearcher, IXmlAttribute attribute)
        {
            if(attribute == null)
                return;
            if(attribute.Value == null)
                return;
            if (string.IsNullOrEmpty(attribute.Value.UnquotedValue) || attribute.Value.UnquotedValue.Trim() == string.Empty)
                return;

            TextRange valueRange = attribute.Value.GetDocumentRange().TextRange;
            attribute.Value.GetText().StartsWith("\"");
            valueRange = valueRange.TrimLeft(1);
            attribute.Value.GetText().EndsWith("\"");
            valueRange = valueRange.TrimRight(1);
            duplicatesSearcher.Add(attribute.Value.UnquotedValue, attribute.Value.GetContainingFile().GetDocumentRange(valueRange));
        }

        public bool ProcessingIsFinished
        {
            get { return false; }
        }

        #endregion

        class DuplicatesSearcher
        {
            public class DuplicatesSearcherItem
            {
                private readonly IComparable key;
                private readonly DocumentRange range;


                public DuplicatesSearcherItem(IComparable key, DocumentRange textRange)
                {
                    this.key = key;
                    this.range = textRange;
                }


                public IComparable Key
                {
                    get { return key; }
                }

                public DocumentRange Range
                {
                    get { return range; }
                }
            }

            private readonly List<DuplicatesSearcherItem> items = new List<DuplicatesSearcherItem>();
            
            public void Add(IComparable key, DocumentRange textRange)
            {
                items.Add(new DuplicatesSearcherItem(key, textRange));
            }

            public  List<List<DuplicatesSearcherItem>> GetDuplicates()
            {
                List<List<DuplicatesSearcherItem>> result = new List<List<DuplicatesSearcherItem>>();
                foreach (DuplicatesSearcherItem item in items)
                {
                    IList<DuplicatesSearcherItem> found = CollectionUtil.FindAll(items, delegate(DuplicatesSearcherItem obj)
                                                                                            {
                                                                                                return obj.Key.CompareTo(item.Key) == 0 && obj != item;
                                                                                            });
                    if(found.Count > 0)
                    {
                        List<DuplicatesSearcherItem> itemDuplicates = new List<DuplicatesSearcherItem>();
                        itemDuplicates.Add(item);
                        itemDuplicates.AddRange(found);
                        result.Add(itemDuplicates);
                    }
                }

                return result;
            }
        }

    }
}