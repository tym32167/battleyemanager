using System;

namespace BattlEyeManager.Services
{
    public interface IFactory<T> 
    {
        T GetService();
    }
}