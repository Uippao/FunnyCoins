using System;
using System.Collections.Generic;
using FunnyCoins.Effects;
using LabApi.Loader.Features.Plugins;

namespace FunnyCoins

{
    public class Config
    {
        public bool Debug { get; set; } = false;
        public double GoodEffectChance { get; set; } = 0.5;
        public float CoinCooldown { get; set; } = 5f;
        public Dictionary<string, int> EffectWeights { get; set; } = new  Dictionary<string, int>();
        public CustomStrings CustomText { get; set; } = new CustomStrings();

        public class CustomStrings
        {
            public string CooldownText { get; set; } = "Time before next coinflip: {0}";

            public Dictionary<string, Dictionary<string, EffectMessage>> EffectMessages { get; set; }
                = new Dictionary<string, Dictionary<string, EffectMessage>>();
        }
        
        public class EffectMessage
        {
            public string Text { get; set; }
            public float Duration { get; set; } = 4f;
        }
        
        public int GetWeight(ICoinEffect effect)
        {
            if (FunnyCoins.Instance.Config.EffectWeights.TryGetValue(effect.Id, out int weight))
                return Math.Max(weight, 0);

            return effect.DefaultWeight;
        }
        
        public void PopulateMissingWeights(IEnumerable<ICoinEffect> effects)
        {
            foreach (var effect in effects)
            {
                if (!EffectWeights.ContainsKey(effect.Id))
                {
                    EffectWeights[effect.Id] = effect.DefaultWeight;
                }
            }
        }
        
        public void PopulateMissingEffectMessages(IEnumerable<ICoinEffect> effects)
        {
            foreach (var effect in effects)
            {
                if (!CustomText.EffectMessages.TryGetValue(effect.Id, out var dict))
                {
                    dict = new Dictionary<string, EffectMessage>();
                    CustomText.EffectMessages[effect.Id] = dict;
                }

                foreach (var def in effect.DefaultMessages)
                {
                    if (!dict.ContainsKey(def.Key))
                    {
                        dict[def.Key] = new EffectMessage
                        {
                            Text = def.DefaultTemplate,
                            Duration = def.DefaultDuration
                        };
                    }
                }
            }
        }

    }
}