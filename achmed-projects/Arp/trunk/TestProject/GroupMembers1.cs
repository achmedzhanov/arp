using System;

namespace TestProject
{
    public class GroupMembers1
    {
        private string a;
        private string b;
        private int c;


        public GroupMembers1()
        {
        }


        public GroupMembers1(string a, string b, int c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        private event EventHandler h1;


        public string A
        {
            get { return a; }
        }

        public string B
        {
            get { return b; }
        }

        public void M1()
        {
            
        }


        public int C
        {
            get { return c; }
            set { c = value; }
        }

        private event EventHandler h2;

    }
}