﻿/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * BattleNET v1.3.3 - BattlEye Library and Client            *
 *                                                         *
 *  Copyright (C) 2015 by it's authors.                    *
 *  Some rights reserved. See license.txt, authors.txt.    *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using System;

namespace BattlEyeManager.BE.BeNet
{
    public delegate void BattlEyeMessageEventHandler(BattlEyeMessageEventArgs args);

    public class BattlEyeMessageEventArgs : EventArgs
    {
        public BattlEyeMessageEventArgs(string message, int id)
        {
            Message = message;
            Id = id;
        }

        public string Message { get; private set; }
        public int Id { get; private set; }
    }
}
