using System;
using System.Collections.Generic;

namespace Arp.Generator.Names
{
    public class CamelNameConverter : INameConverter
    {
        #region INameConverter Members

        public string ConvertElementName(string name)
        {
            return RemoveMinus(name);
        }

        public string ConvertTypeName(string name)
        {
            return RemoveMinus(name) + "Element";
        }

        public string ConvertAttributeName(string name)
        {
            return RemoveMinus(name) + "Attribute";
        }

        public string ConvertSimpleTypeName(string name)
        {
            return RemoveMinus(name);
        }

        public string ConvertEnumerationName(string name)
        {
            return RemoveMinus(name);
        }


        public string ConvertSimpleTypeRestrictionName(string name)
        {
            return RemoveMinus(name);
        }

        public string CreateComplexName(string[] names)
        {
            List<string> list = new List<string>(names);
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = RemoveMinus(list[i]);
            }

            return JionParts(list.ToArray());
        }


        public string ConvertFacetName(string value)
        {
            return RemoveMinus(value);
        }

        #endregion

        protected string UpFerstLetter(string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            if(name.Length == 0)
                throw new ArgumentException("empty name");
            if(Char.IsLetter(name[0]))
            {
                name = Char.ToUpper(name[0]) + name.Substring(1);
            }

            return name;
        }

        protected string RemoveMinus(string name)
        {
            string [] splitted = name.Split('-');
            return JionParts(splitted);
        }

        private string JionParts(string[] parts)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (string subName in parts)
            {
                sb.Append(UpFerstLetter(subName));
            }

            return sb.ToString();
        }
    }
}