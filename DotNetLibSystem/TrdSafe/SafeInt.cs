namespace DotNetLibSystem
{
    namespace Sys.TrdSafe
    {
        public class SafeInt : Locked<int>
        {
            public SafeInt(int value) : base(value)
            {
            }

            public static SafeInt operator +(SafeInt safeInt, int value)
            {
                safeInt._mtx.Wait();
                safeInt._value += value;
                safeInt._mtx.Set();
                return safeInt;
            }

            public static SafeInt operator -(SafeInt safeInt, int value)
            {
                safeInt._mtx.Wait();
                safeInt._value -= value;
                safeInt._mtx.Set();
                return safeInt;
            }

            public static SafeInt operator ++(SafeInt safeInt)
            {
                safeInt._mtx.Wait();
                safeInt._value ++;
                safeInt._mtx.Set();
                return safeInt;
            }

            public static SafeInt operator --(SafeInt safeInt)
            {
                safeInt._mtx.Wait();
                safeInt._value --;
                safeInt._mtx.Set();
                return safeInt;
            }
        }
    }
}
