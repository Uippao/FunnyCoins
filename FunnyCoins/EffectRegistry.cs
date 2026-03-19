using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FunnyCoins.Effects
{
    public static class EffectRegistry
    {
        public static readonly List<ICoinEffect> GoodEffects = new  List<ICoinEffect>();
        public static readonly List<ICoinEffect> BadEffects = new  List<ICoinEffect>();

        public static void Load()
        {
            var effectTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t =>
                    typeof(ICoinEffect).IsAssignableFrom(t) &&
                    !t.IsInterface &&
                    !t.IsAbstract);

            foreach (var type in effectTypes)
            {
                ICoinEffect effect = (ICoinEffect)Activator.CreateInstance(type);

                Register(effect);
            }
        }
        
        public static void Register(ICoinEffect effect)
        {
            if (effect == null)
                return;

            if (GetEffectById(effect.Id) != null)
                return;

            if (effect.IsGood)
                GoodEffects.Add(effect);
            else
                BadEffects.Add(effect);
        }
        
        public static ICoinEffect PickRandom(List<ICoinEffect> effects)
        {
            int totalWeight = effects.Sum(e => FunnyCoins.Instance.Config.GetWeight(e));

            if (totalWeight <= 0)
                return effects[0];

            int roll = FunnyCoins.Rng.Next(totalWeight);

            int cumulative = 0;

            foreach (var effect in effects)
            {
                cumulative += FunnyCoins.Instance.Config.GetWeight(effect);

                if (roll < cumulative)
                    return effect;
            }

            return effects[0];
        }
        
        public static ICoinEffect GetEffectById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            ICoinEffect effect = GoodEffects.FirstOrDefault(e => e.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
            if (effect != null)
                return effect;

            return BadEffects.FirstOrDefault(e => e.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        }
    }
}