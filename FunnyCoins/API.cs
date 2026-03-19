using FunnyCoins.Effects;
using LabApi.Features.Wrappers;

namespace FunnyCoins.API
{
    public static class FunnyCoinsAPI
    {
        public static void RegisterEffect(ICoinEffect effect)
        {
            EffectRegistry.Register(effect);
        }

        public static void ShowEffectMessage(Player player, ICoinEffect effect, string key = "default", params object[] args)
        {
            FunnyCoins.Instance.ShowEffectMessage(player, effect, key, args);
        }
    }
}