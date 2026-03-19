using FunnyCoins.Effects;

namespace FunnyCoins.API
{
    public static class FunnyCoinsAPI
    {
        public static void RegisterEffect(ICoinEffect effect)
        {
            EffectRegistry.Register(effect);
        }
    }
}