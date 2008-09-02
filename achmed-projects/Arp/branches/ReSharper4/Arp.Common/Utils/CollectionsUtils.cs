using System;
using System.Collections;
using System.Collections.Generic;

namespace Arp.Common.Utils
{
    public delegate T Transformer<F, T>(F source);

    public class CollectionsUtils
    {

        public static readonly IList EmptyList = ArrayList.ReadOnly(new ArrayList());

        protected CollectionsUtils()
        {
        }

        public static int GetLength(IEnumerable collection)
        {
            int ret = 0;
            if (collection != null)
                foreach (object o in collection)
                {
                    ret += 1;
                }
            return ret;
        }

        public static IList SubList(IList list, int n)
        {
            ArrayList ret = new ArrayList(n);
            for (int i = 0; i < n && i < list.Count; i++)
            {
                ret.Add(list[i]);
            }
            return ret;
        }

        public static ArrayList SubList(IEnumerable list, int start, int n)
        {
            ArrayList ret = new ArrayList(n);
            int i = 0;
            foreach (object o in list)
            {
                if (i >= start + n)
                    break;

                if (i >= start)
                {
                    ret.Add(o);
                }

                i++;
            }

            return ret;
        }

        public static IList<T> SubList<T>(ICollection<T> list, int start, int n)
        {
            List<T> ret = new List<T>(n);
            for (int i = start; i < start + n && i < list.Count; i++)
            {
                ret.Add(GetElement(list, i));
            }
            return ret;
        }

        public static List<T> CreateOneElementList<T>(T element)
        {
            List<T> ret = new List<T>(1);
            ret.Add(element);
            return ret;
        }

        public static bool IsEmpty(ICollection c)
        {
            return c == null || c.Count == 0;
        }

        public static bool IsEmpty<T>(ICollection<T> c)
        {
            return c == null || c.Count == 0;
        }

        public static object GetElementOrNull(IList list, int idx)
        {
            return GetElement(list, idx, null);
        }

        public static object GetElement(IList list, int idx, object notExistValue)
        {
            if (idx < 0 || idx > list.Count - 1)
                return notExistValue;
            return list[idx];
        }

        public static T GetElement<T>(IEnumerable<T> collection, int at)
        {
            int idx = 0;
            foreach (T obj in collection)
            {
                if (idx == at)
                    return obj;
                idx++;
            }

            throw new ArgumentOutOfRangeException("at", "expected value less then " + idx);

        }

        public static ICollection<T> ConvertCollections<T, V>(ICollection<V> values)
            where T : class
        {
            List<T> result = null;
            result = new List<T>();
            foreach (V val in values)
            {
                if (val is T)
                {
                    result.Add(val as T);
                }
                else
                {
                    throw new InvalidCastException(String.Format("Cannot convert from {0} to {1}", typeof(V).FullName, typeof(T).FullName));
                }
            }
            return (ICollection<T>)result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static T Find<T>(IEnumerable<T> collection, Predicate<T> p)
        {
            foreach (T obj in collection)
            {
                if (p(obj))
                    return obj;
            }

            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static List<T> FindAll<T>(IEnumerable<T> collection, Predicate<T> p)
        {
            List<T> ret = new List<T>();
            foreach (T obj in collection)
            {
                if (p(obj))
                    ret.Add(obj);
            }

            return ret;
        }

        public static T Find<T>(IEnumerable collection, Predicate<T> p)
        {
            foreach (T obj in collection)
            {
                if (p(obj))
                    return obj;
            }

            return default(T);
        }


        public static List<T> ImplicitCast<F, T>(ICollection<F> source)
            where F : T
        {
            return Transform<F, T>(source, delegate(F s)
                                               {
                                                   T t = s;
                                                   return t;
                                               });
        }

        public static List<T> Transform<F, T>(ICollection<F> source, Transformer<F, T> transformer)
        {
            if (transformer == null)
                throw new ArgumentException("transformer");

            if (source == null || source.Count == 0)
                return new List<T>();

            List<T> ret = new List<T>(source.Count);
            foreach (F f in source)
            {
                ret.Add(transformer(f));
            }

            return ret;
        }

        public static List<T> Transform<F, T>(ICollection<F> source)
            where F : T
        {

            if (source == null || source.Count == 0)
                return new List<T>();

            List<T> ret = new List<T>(source.Count);
            foreach (F f in source)
            {
                ret.Add(f);
            }

            return ret;
        }

        #region Window functions

        public static int GetWindowStart(int pageSize, int pageIdx)
        {
            return pageSize * pageIdx;
        }

        public static int GetWindowEnd(int pageSize, int pageIdx)
        {
            return pageSize * (pageIdx + 1);
        }

        #endregion

        #region Generic utils

        public static List<T> CreateList<T>(IEnumerable collection)
        {
            List<T> ret = new List<T>();
            if (collection != null)
            {
                foreach (object o in collection)
                {
                    ret.Add((T)o);
                }
            }
            return ret;
        }

        public static IList CreateList(IEnumerable collection)
        {
            ArrayList ret = new ArrayList();
            if (collection != null)
            {
                foreach (object o in collection)
                {
                    ret.Add(o);
                }
            }
            return ret;
        }

        #endregion

        public static List<T> Reverse<T>(IEnumerable<T> coll)
        {
            List<T> ret = new List<T>(1);
            foreach (T t in coll)
            {
                ret.Insert(0, t);
            }
            return ret;
        }

        public static T[] ToArray<T>(ICollection<T> collection)
        {
            if (collection == null)
                return null;

            T[] ret = new T[collection.Count];

            int idx = 0;
            foreach (T element in collection)
            {
                ret[idx++] = element;
            }

            return ret;
        }
    }
}