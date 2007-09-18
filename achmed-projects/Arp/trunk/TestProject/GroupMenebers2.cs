using System;

namespace TestProject
{
    public class GroupMembers2
    {
        private delegate void D();
        
        class GroupMembers2Nested
        {
            
        }
        
        private string a;
        private string b;
        private int c;


        public GroupMembers2()
        {
        }


        public GroupMembers2(string a, string b, int c)
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