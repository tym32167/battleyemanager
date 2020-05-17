﻿using System;
using System.Collections.Generic;

namespace BattlEyeManager.Steam
{
    [Serializable]
    public class ServerRulesResult : Dictionary<string, string>
    {
        public static ServerRulesResult Parse(byte[] bytes)
        {
            var result = new ServerRulesResult();
            var parser = new ResponseParser(bytes);
            parser.CurrentPosition += 7;
            while (parser.BytesLeft)
            {
                result.Add(parser.GetStringToTermination(), parser.GetStringToTermination());
            }
            return result;
        }
    }
}