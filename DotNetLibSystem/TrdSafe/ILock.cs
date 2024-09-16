namespace DotNetLibSystem
{
    namespace Sys.TrdSafe
    {
        public interface ILock
        {
            void Release();
        }

        public interface ILocked<T> : ILock
        {
            T Value { get; set; }
            T Get();
            T GetAndLock();
            T Set(T value);
            bool Set(T value, int miliseconds);
            void SetPure(T value);
        }
    }
}
