using System;
using System.Collections.Generic;
using System.IO;
using FunnyCoins.Effects;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Handlers;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using MEC;
using PlayerRoles;
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
        public override Version Version { get; } = new Version(1, 0, 0, 0);
        public override Version RequiredApiVersion { get; } = new Version(LabApiProperties.CompiledVersion);
        
        public static FunnyCoins Instance { get; private set; }
        
        public static readonly Random Rng = new Random();
        private readonly Dictionary<string, float> _cooldowns = new Dictionary<string, float>();
        private static readonly Tag CooldownTag = new Tag("funnycoins_cooldown");
        private static readonly Tag EffectTag = new Tag("funnycoins_effect");
    
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
                    
                    string text = string.Format(Config.CustomText.CooldownText, $"{remaining:F1}s");

                    var display = RueDisplay.Get(player);
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
        
        public void ShowEffectMessage(Player player, ICoinEffect effect)
        {
            if (!Config.CustomText.EffectMessages.TryGetValue(effect.Id, out var msg))
                return;

            var display = RueDisplay.Get(player);
            display.Remove(CooldownTag);
            display.Show(
                EffectTag,
                new BasicElement(
                    250,
                    $"<align=left>{msg.Text}</align>"
                ),
                msg.Duration
            );
        }
        
        internal void LogDebug(string text)
        {
           if (Config.Debug)
               Logger.Debug(text);
        }
    }
}
