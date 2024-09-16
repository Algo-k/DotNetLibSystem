namespace DotNetLibSystem
{
    namespace Sys.TrdSafe
    {
        public class LockedTransfere<T> : Locked<T>
        {
            bool _fill = false;

            new public T Get()
            {
                T value = default(T);
                _mtx.Wait();
                if (_fill)
                    value = _value;
                _fill = false;
                _mtx.Set();
                return value;
            }

            new public T Set(T value)
            {
                _mtx.Wait();
                _value = value;
                _fill = true;
                _mtx.Set();
                return value;
            }
        }
    }
}
