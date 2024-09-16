using System;
using System.Runtime.InteropServices;

namespace DotNetLibSystem
{
    namespace Sys.Syncron
    {
        public class Win32Event
        {
            [DllImport("kernel32.dll")]
            static extern IntPtr CreateEvent(IntPtr lpEventAttributes, bool bManualReset, bool bInitialState, string lpName);

            [DllImport("kernel32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool CloseHandle(IntPtr hObject);

            [DllImport("kernel32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetEvent(IntPtr hEvent);

            [DllImport("kernel32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool ResetEvent(IntPtr hEvent);

            [DllImport("kernel32.dll")]
            public static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

            [DllImport("kernel32.dll")]
            public static extern int WaitForMultipleObjects(uint nCount, IntPtr[] lpHandles, ushort bWaitAll, uint dwMilliseconds);




            public IntPtr Handle;

            public Win32Event(bool state, bool manualReset = false)
            {
                Handle = CreateEvent(IntPtr.Zero, manualReset, state, "");
            }

            public bool Set()
            {
                var res = SetEvent(Handle);
                return res;
            }

            [DllImport("kernel32.dll")]
            public static extern int GetLastError();


            public bool Wait(uint time = 0xffffffff)
            {
                var res = WaitForSingleObject(Handle, time);
                return res == 0;
            }
            public bool Wait(int time)
            {
                var res = WaitForSingleObject(Handle, (uint)time);
                return res == 0;
            }

            public bool Close()
            {
                return CloseHandle(Handle);
            }

            public bool Reset()
            {
                return ResetEvent(Handle);
            }


            public static int WaitEvents(Win32Event[] events, uint time = 0xffffffff, bool waitAll = false)
            {
                var handles = new IntPtr[events.Length];
                for (int i = 0; i < events.Length; i++)
                {
                    handles[i] = events[i].Handle;
                }

                int size = Marshal.SizeOf(typeof(IntPtr));
                IntPtr pHandles = Marshal.AllocHGlobal(size * handles.Length);
                Marshal.Copy(handles, 0, pHandles, handles.Length);
                var result = WaitForMultipleObjects((uint)handles.Length, handles, waitAll ? (ushort)1 : (ushort)0, time);
                Marshal.FreeHGlobal(pHandles);
                return result;
            }
            public static int WaitEvents(Win32Event event1, Win32Event event2, uint time = 0xffffffff, bool waitAll = false)
            {
                var handles = new IntPtr[2] { event1.Handle, event2.Handle };

                int size = Marshal.SizeOf(typeof(IntPtr));
                IntPtr pHandles = Marshal.AllocHGlobal(size * 2);
                Marshal.Copy(handles, 0, pHandles, 2);
                var result = WaitForMultipleObjects(2, handles, waitAll ? (ushort)1 : (ushort)0, time);
                Marshal.FreeHGlobal(pHandles);
                return result;
            }
        }
    }
}