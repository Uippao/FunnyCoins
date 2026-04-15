using System;
using System.Collections.Generic;
using System.Globalization;
using FunnyCoins.Effects;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Handlers;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Plugins.Enums;
using MEC;
using UnityEngine;
using Logger = LabApi.Features.Console.Logger;
using Random = System.Random;
using RueI.API;
using RueI.API.Elements;

namespace FunnyCoins
{
    public class FunnyCoins: Plugin<Config>
    {
        public override string Name { get; } = "FunnyCoins";
        public override string Description { get; } = "A LabAPI plugin that brings random effects to flipping coins.";
        public override string Author { get; } = "Uippao";
        public override Version Version { get; } = new Version(1, 1, 0, 2);
        public override Version RequiredApiVersion { get; } = new Version(LabApiProperties.CompiledVersion);
        public override LoadPriority Priority { get; } = LoadPriority.High;

        public static FunnyCoins Instance { get; private set; }
        
        public static readonly Random Rng = new Random();
        private readonly Dictionary<string, float> _cooldowns = new Dictionary<string, float>();
        private static readonly Tag CooldownTag = new Tag("funnycoins_cooldown");
        private static readonly Tag EffectTag = new Tag("funnycoins_effect");
        
        private static readonly Tag CustomItemsTag = new Tag("customitems_hint");
    
        public override void Enable()
        {  
            Instance = this;
            LoadConfigs();
            if (Config.CustomText == null)
                Config.CustomText = new Config.CustomStrings();
            
            Config.GoodEffectChance = Math.Max(0.0, Math.Min(1.0, Config.GoodEffectChance));
            Config.CoinCooldown = Math.Max(0f, Config.CoinCooldown);
            
            EffectRegistry.Load();

            Config.PopulateMissingWeights(EffectRegistry.GoodEffects);
            Config.PopulateMissingWeights(EffectRegistry.BadEffects);
            Config.PopulateMissingEffectMessages(EffectRegistry.GoodEffects);
            Config.PopulateMissingEffectMessages(EffectRegistry.BadEffects);
            
            SaveConfig();
            
            PlayerEvents.FlippingCoin += OnFlippingCoin;
            PlayerEvents.FlippedCoin += OnFlippedCoin;
            ServerEvents.RoundRestarted += OnRoundRestarted;
        }  

        public override void Disable()
        {
            PlayerEvents.FlippingCoin -= OnFlippingCoin;
            PlayerEvents.FlippedCoin -= OnFlippedCoin;
            ServerEvents.RoundRestarted -= OnRoundRestarted;
            
            _cooldowns.Clear();
            
            foreach (var player in Player.List)
            {
                RueDisplay.Get(player).Remove(CooldownTag);
            }
            
            Instance = null;
        }
        
        private void OnFlippingCoin(PlayerFlippingCoinEventArgs ev)
        {
            var player = ev.Player;

            if (player == null || !player.IsAlive)
                return;

            if (_cooldowns.TryGetValue(player.UserId, out float endTime))
            {
                float remaining = endTime - Time.time;

                if (remaining > 0f)
                {
                    ev.IsAllowed = false;
                    
                    string text = string.Format(Config.CustomText.CooldownText, $"{remaining:F1}");

                    var display = RueDisplay.Get(player);
                    display.Remove(CustomItemsTag);
                    display.Remove(EffectTag);
                    display.Show(
                        CooldownTag,
                        new BasicElement(
                            250,
                            $"<align=left>{text}</align>"
                        ),
                        2f
                    );

                    LogDebug($"{player.Nickname} tried flipping a coin but is on cooldown ({remaining:F1}s left).");
                    return;
                }

                _cooldowns.Remove(player.UserId);
            }

            double roll = Rng.NextDouble();
            bool good = roll < Config.GoodEffectChance;

            Item coin = player.CurrentItem;
            if (coin == null)
                return;

            ushort serial = coin.Serial;

            var pool = good
                ? EffectRegistry.GoodEffects
                : EffectRegistry.BadEffects;

            var effect = EffectRegistry.PickRandom(pool);

            effect.Execute(player);
            if (!effect.HandlesOwnMessage)
                Instance.ShowEffectMessage(player, effect);

            LogDebug($"{player.Nickname} flipped a coin. Roll: {(good ? "GOOD" : "BAD")}");

            Timing.CallDelayed(2f, () =>
            {
                if (Item.TryGet(serial, out Item item))
                {
                    item.CurrentOwner?.RemoveItem(item);
                }
                else if (Pickup.Get(serial) is Pickup pickup)
                {
                    pickup.Destroy();
                }
            });
        }

        private void OnFlippedCoin(PlayerFlippedCoinEventArgs ev)
        {
            var player = ev.Player;

            if (player == null)
                return;

            _cooldowns[player.UserId] = Time.time + Config.CoinCooldown;

            LogDebug($"{player.Nickname} entered coin cooldown ({Config.CoinCooldown}s).");
        }
        
        private void OnRoundRestarted()
        {
            _cooldowns.Clear();
            
            foreach (var player in Player.List)
            {
                RueDisplay.Get(player).Remove(CooldownTag);
            }
        }
        
        internal void LogDebug(string text)
        {
           if (Config.Debug)
               Logger.Debug(text);
        }
        
        public (string text, float duration)? GetEffectMessageTemplate(string effectId, string key = "default")
        {
            if (Config.CustomText.EffectMessages.TryGetValue(effectId, out var dict))
            {
                if (dict.TryGetValue(key, out var msg))
                    return (msg.Text, msg.Duration);
            }
            return null;
        }

        public void ShowEffectMessage(Player player, ICoinEffect effect, string key = "default", params object[] args)
        {
            var tpl = GetEffectMessageTemplate(effect.Id, key);
            if (tpl == null)
                return;

            string text = tpl.Value.text;
            float duration = tpl.Value.duration;

            if (args != null && args.Length > 0)
            {
                try
                {
                    text = string.Format(CultureInfo.InvariantCulture, text, args);
                }
                catch (FormatException)
                {
                }
            }

            var display = RueDisplay.Get(player);
            display.Remove(CustomItemsTag);
            display.Remove(CooldownTag);
            display.Show(
                EffectTag,
                new BasicElement(250, $"<align=left>{text}</align>"),
                duration
            );
        }
    }
}
