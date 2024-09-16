namespace DotNetLibSystem
{
    namespace Sys.TrdSafe
    {
        public class SafeDouble : Locked<double>
        {
            public SafeDouble(double value) : base(value)
            {
            }

            public static SafeDouble operator *(SafeDouble safeInt, double value)
            {
                safeInt._mtx.Wait();
                safeInt._value *= value;
                safeInt._mtx.Set();
                return safeInt;
            }


            public static SafeDouble operator +(SafeDouble safeInt, double value)
            {
                safeInt._mtx.Wait();
                safeInt._value += value;
                safeInt._mtx.Set();
                return safeInt;
            }

            public static SafeDouble operator -(SafeDouble safeInt, double value)
            {
                safeInt._mtx.Wait();
                safeInt._value -= value;
                safeInt._mtx.Set();
                return safeInt;
            }

            public static SafeDouble operator ++(SafeDouble safeInt)
            {
                safeInt._mtx.Wait();
                safeInt._value++;
                safeInt._mtx.Set();
                return safeInt;
            }

            public static SafeDouble operator --(SafeDouble safeInt)
            {
                safeInt._mtx.Wait();
                safeInt._value--;
                safeInt._mtx.Set();
                return safeInt;
            }
        }
    }
}
