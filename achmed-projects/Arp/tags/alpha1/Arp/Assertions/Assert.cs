using System;

namespace Arp.Assertions
{
    public class Assert
    {
        protected Assert()
        {
        }

        public static void Fail()
        {
            Assert.Check(false);
        }

        public static void Fail(string message)
        {
            Assert.Check(false, message);
        }

        public static void Check(bool expr, string msg)
        {
            if (!expr)
                throw new AssertException(msg);
        }

        public static void Check(bool expr)
        {
            if (!expr)
                throw new AssertException();
        }

        public static void CheckFalse(bool expr, string msg)
        {
            if (expr)
                throw new AssertException(msg);
        }

        public static void CheckFalse(bool expr)
        {
            if (expr)
                throw new AssertException();
        }


        public static void CheckNotNull(object v, string msg)
        {
            if (v == null)
                throw new AssertException(msg);
        }

        public static void CheckNotNull(object v)
        {
            if (v == null)
                throw new AssertException();
        }

        public static void CheckNotNullAndEmpty(string v)
        {
            //            if (StringUtils.IsEmptyOrNull(v))
            if (String.IsNullOrEmpty(v))
                throw new AssertException();
        }

        public static void CheckNoNegative(int v)
        {
            if (v < 0)
                throw new AssertException();
        }

        public static void CheckNoNegative(int v, string message)
        {
            if (v < 0)
                throw new AssertException(message);
        }

        public static void CheckEqual(int expected, int value)
        {
            Assert.Check(expected == value, "expected " + expected + " but value is " + value);
        }

        public static void CheckLessThen(int maxValue, int value)
        {
            Assert.Check(maxValue >= value, "expected less then " + maxValue + ", but value is " + value);
        }

    }
}