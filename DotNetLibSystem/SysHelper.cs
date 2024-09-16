using DotNetLibSystem.Sys;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace DotNetLibSystem
{
    public static class SysHelper
    {
        public static void SoftLockMutex(Mutex mtx, int wait_millisecond)
        {
            bool res;
            while (true)
            {
                res = mtx.WaitOne(1);
                if (res)
                    return;
                Thread.Sleep(wait_millisecond);
            }
        }
    }
}
