using System.Collections.Generic;
using LabApi.Features.Wrappers;
using UnityEngine;

namespace FunnyCoins.Effects
{
    public class StrengthenedEffect : ICoinEffect
    {
        public string Id => "Strengthened";
        public bool IsGood => true;
        public int DefaultWeight => 7;

        public bool HandlesOwnMessage => true;

        public IEnumerable<EffectMessageDefinition> DefaultMessages => new[]
        {
            new EffectMessageDefinition("damaged", "You've been magically healed!", 4f),
            new EffectMessageDefinition("fullhealth", "You feel stronger!", 4f)
        };

        public string DefaultMessage => null;

        public void Execute(Player player)
        {
            if (player.Health < player.MaxHealth)
            {
                player.Health = player.MaxHealth;

                FunnyCoins.Instance.ShowEffectMessage(player, this, "damaged");
            }
            else
            {
                player.MaxHealth += 100;
                player.Health = player.MaxHealth;

                FunnyCoins.Instance.ShowEffectMessage(player, this, "fullhealth");
            }
        }
    }

    public class AidEffect : SimpleCoinEffect
    {
        public override string Id => "Aid";
        public override bool IsGood => true;
        public override int DefaultWeight => 15;

        public override string DefaultMessage => "You've received some humanitarian aid!";

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;

            Pickup.Create(ItemType.Medkit, pos).Spawn();
            Pickup.Create(ItemType.Painkillers, pos).Spawn();
            Pickup.Create(ItemType.Painkillers, pos).Spawn();
        }
    }
}