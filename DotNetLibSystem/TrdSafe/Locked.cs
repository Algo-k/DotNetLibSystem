using DotNetLibSystem.Sys.Syncron;
using System.Threading;

namespace DotNetLibSystem
{
    namespace Sys.TrdSafe
    {
        public class Locked<T> : ILocked<T>
        {
            protected T _value;
            protected Win32Event _mtx = new Win32Event(true, false);

            public Locked(T value)
            {
                _value = value;
            }

            public Locked()
            {
            }

            public T Value
            {
                get
                {
                    return Get();
                }
                set
                {
                    Set(value);
                }
            }

            public T ValueCore
            {
                get
                {
                    return _value;
                }
                set
                {
                    _value = value;
                }
            }

            public T Get()
            {
                _mtx.Wait();
                var value = _value;
                _mtx.Set();
                return value;
            }

            public void WaitForStatus(T t, int resolution)
            {
                while(!Value.Equals(_value))
                {
                    Thread.Sleep(resolution);
                }
            }

            public T GetAndLock()
            {
                _mtx.Wait();
                var value = _value;
                return value;
            }

            public T GetAndLock(T eval)
            {
                _mtx.Wait();
                var value = _value;
                if (value.Equals(eval))
                {
                    _mtx.Set();
                    return value;
                }
                else
                {
                    return value;
                }
            }

            public void Wait()
            {
                _mtx.Wait();
            }

            public void Release()
            {
                _mtx.Set();
            }

            public T Set(T value)
            {
                _mtx.Wait();
                _value = value;
                _mtx.Set();
                return value;
            }

            public bool Set(T value, int miliseconds)
            {

                if (_mtx.Wait(miliseconds))
                {
                    _value = value;
                    _mtx.Set();
                    return true;
                }
                _mtx.Set();
                return false;
            }

            public void SetPure(T value)
            {
                Set(value);
            }

            public Locked<T> Clone()
            {
                return new Locked<T>(Value);
            }
        }
    }
}
