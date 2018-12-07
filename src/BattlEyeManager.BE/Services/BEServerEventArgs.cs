using System;

namespace BattlEyeManager.BE.Services
{
    public class BEServerEventArgs<T> : EventArgs
    {
        public BEServerEventArgs(ServerInfo server, T data)
        {
            Server = server;
            Data = data;
        }

        public ServerInfo Server { get; }
        public T Data { get; }
    }
}