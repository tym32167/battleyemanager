﻿/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * BattleNET v1.3.3 - BattlEye Library and Client            *
 *                                                         *
 *  Copyright (C) 2015 by it's authors.                    *
 *  Some rights reserved. See license.txt, authors.txt.    *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using System.Net;

namespace BattlEyeManager.BE.BeNet
{
    public struct BattlEyeLoginCredentials
    {
        public BattlEyeLoginCredentials(IPAddress host, int port, string password)
            : this()
        {
            Host = host;
            Port = port;
            Password = password;
        }

        public IPAddress Host { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
    }
}