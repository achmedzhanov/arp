using System;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace Arp.Utils
{
    public static class ElementUtils
    {
        public static T Find<T>(IElement element, Predicate<T> predicate)
            where T : class, IElement
        {
            RecursiveElementProcessor<T> processor = new RecursiveElementProcessor<T>(predicate);
            element.ProcessDescendants(processor);
            return processor.Result;
        }

        class RecursiveElementProcessor <T> : IRecursiveElementProcessor
            where T : class, IElement
        {
            private T result;
            private readonly Predicate<T> predicate;
            private bool processingIsFinished = false;


            public RecursiveElementProcessor(Predicate<T> predicate)
            {
                this.predicate = predicate;
            }

            public T Result
            {
                get { return result; }
            }

            public bool InteriorShouldBeProcessed(IElement element)
            {
                Check(element);

                return !processingIsFinished;
            }

            private void Check(IElement element)
            {
                T asT = element  as T;

                if (asT != null && predicate(asT))
                {
                    result = asT;
                    processingIsFinished = true;
                }
            }

            public void ProcessBeforeInterior(IElement element)
            {
                // do nothing
            }

            public void ProcessAfterInterior(IElement element)
            {
                Check(element);
            }


            public bool ProcessingIsFinished
            {
                get { return processingIsFinished; }
            }
        }

    }
}