using System.Threading;
using System.Collections.Generic;

namespace DotNetLibSystem
{
    namespace Sys.TrdSafe
    {
        public class HLock<T> : ILocked<T>
        {
            private List<T> _past;
            private readonly int _hsize;
            private T _value;
            private ManualResetEvent _event = new ManualResetEvent(true);

            public HLock(T value, int hsize)
            {
                _value = value;
                _past =new List<T>();
                _hsize = hsize;
            }

            public HLock(int hsize)
            {
                _past = new List<T>();
                _hsize = hsize;
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

            public T Get()
            {
                _event.WaitOne();
                _event.Reset();
                var value = _value;
                _event.Set();
                return value;
            }
            public T Past(int index)
            {
                _event.WaitOne();
                _event.Reset();
                var value = _past[index];
                _event.Set();
                return value;
            }

            public T GetAndLock()
            {
                _event.WaitOne();
                _event.Reset();
                var value = _value;
                return value;
            }

            public T GetAndLock(T eval)
            {
                _event.WaitOne();
                _event.Reset();
                var value = _value;
                if (value.Equals(eval))
                {
                    _event.Set();
                    return value;
                }
                else
                {
                    return value;
                }
            }

            public void Release()
            {
                _event.Set();
            }

            public T Set(T value)
            {
                _event.WaitOne();
                _event.Reset();
                _past.Insert(0, _value);
                if (_past.Count > _hsize)
                    _past.RemoveAt(_hsize);
                _value = value;
                _event.Set();
                return value;
            }

            public bool Set(T value, int miliseconds)
            {

                if (_event.WaitOne(miliseconds))
                {
                    _event.Reset();
                    _past.Insert(0, _value);
                    if (_past.Count > _hsize)
                        _past.RemoveAt(_hsize);
                    _value = value;
                    _event.Set();
                    return true;
                }
                _event.Set();
                return false;
            }

            public void SetPure(T value)
            {
                Set(value);
            }
        }
    }
}
