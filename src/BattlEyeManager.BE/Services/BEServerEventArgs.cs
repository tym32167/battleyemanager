using BattleNET;
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

    public class CommandArgs : EventArgs
    {
        public int ServerId { get; }
        public BattlEyeCommand Command { get; }
        public string Param { get; }

        public CommandArgs(int serverId, BattlEyeCommand command, string param)
        {
            ServerId = serverId;
            Command = command;
            Param = param;
        }
    }
}