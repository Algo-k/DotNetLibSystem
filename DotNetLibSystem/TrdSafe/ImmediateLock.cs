using System.Threading;

namespace DotNetLibSystem
{
    namespace Sys.TrdSafe
    {
        public class ImmediateLock
        {
            protected bool _value = false;
            protected Mutex _mtx = new Mutex();
            
            public bool TryLock()
            {
                _mtx.WaitOne();
                var res = _value;
                if (!res)
                    _value = true;
                _mtx.ReleaseMutex();
                return !res;
            }

            public void LockWaitBySleep(int resolution)
            {
                while (TryLock() == false)
                {
                    Thread.Sleep(resolution);
                }
            }

            public void Unlock()
            {
                _mtx.WaitOne();
                _value = false;
                _mtx.ReleaseMutex();
            }
        }
    }
}
