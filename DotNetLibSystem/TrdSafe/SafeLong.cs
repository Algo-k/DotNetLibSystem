namespace DotNetLibSystem
{
    namespace Sys.TrdSafe
    {
        public class SafeLong : Locked<long>
        {
            public SafeLong(long value) :base(value)
            {
            }

            public static SafeLong operator +(SafeLong safeInt, long value)
            {
                safeInt._mtx.Wait();
                safeInt._value += value;
                safeInt._mtx.Set();
                return safeInt;
            }

            public static SafeLong operator -(SafeLong safeInt, long value)
            {
                safeInt._mtx.Wait();
                safeInt._value -= value;
                safeInt._mtx.Set();
                return safeInt;
            }

            public static SafeLong operator ++(SafeLong safeInt)
            {
                safeInt._mtx.Wait();
                safeInt._value++;
                safeInt._mtx.Set();
                return safeInt;
            }

            public static SafeLong operator --(SafeLong safeInt)
            {
                safeInt._mtx.Wait();
                safeInt._value--;
                safeInt._mtx.Set();
                return safeInt;
            }
        }
    }
}
