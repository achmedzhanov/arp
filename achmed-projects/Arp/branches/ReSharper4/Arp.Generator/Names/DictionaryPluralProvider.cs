using System;
using System.Collections.Specialized;

namespace Arp.Generator.Names
{
    public class DictionaryPluralProvider : IPluralProvider
    {
        private StringDictionary dictionary;


        public DictionaryPluralProvider()
        {
            dictionary = new StringDictionary();

            dictionary["property"] = "properties";

        }

        #region IPluralProvider Members

        public string Plural(string word)
        {
            string lower = word.ToLower();
            if (dictionary.ContainsKey(lower))
            {
                return FixRegister(word, dictionary[lower]);
            }
            else
                return word + "s";

        }

        #endregion

        public StringDictionary Dictionary
        {
            get { return dictionary; }
        }

        protected string FixRegister(string first, string second)
        {
            if (first == null) throw new ArgumentNullException("first");
            if (second == null) throw new ArgumentNullException("second");

            char[] result = second.ToCharArray();

            for (int i = 0; i < first.Length && i < second.Length; i++)
            {
                if(Char.ToLower(first[i]) != Char.ToLower(second[i]))
                    break;

                if (Char.IsUpper(first[i]) != Char.IsUpper(second[i]))
                    result[i] = first[i];
            }

            return new string(result);
        }
    }
}
