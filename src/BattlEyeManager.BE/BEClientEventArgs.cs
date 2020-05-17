using System;

namespace BattlEyeManager.BE
{
    public class BEClientEventArgs<T> : EventArgs
    {
        public BEClientEventArgs(T data)
        {
            Data = data;
        }

        public T Data { get; private set; }
    }
}