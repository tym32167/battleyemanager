using BattleNET;

namespace BattlEyeManager.BE.Abstract
{
    public interface IBattlEyeServerFactory
    {
        IBattlEyeServer Create(BattlEyeLoginCredentials credentials);
    }
}