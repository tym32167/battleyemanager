﻿namespace BattlEyeManager.Core
{
    public interface IIpService
    {
        string GetIpAddress(string host);
        string GetCountry(string Ip);
    }
}