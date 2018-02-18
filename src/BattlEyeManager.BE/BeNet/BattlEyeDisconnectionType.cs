﻿/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * BattleNET v1.3.3 - BattlEye Library and Client            *
 *                                                         *
 *  Copyright (C) 2015 by it's authors.                    *
 *  Some rights reserved. See license.txt, authors.txt.    *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using System.ComponentModel;

namespace BattlEyeManager.BE.BeNet
{
    public enum BattlEyeDisconnectionType
    {
        [Description("Disconnected!")]
        Manual,

        [Description("Disconnected! (Connection timeout)")]
        ConnectionLost,

        [Description("Disconnected! (Socket Exception)")]
        SocketException,
    }
}
