using LabApi.Features.Wrappers;
using UnityEngine;

namespace FunnyCoins.Effects
{
    public class HealEffect : SimpleCoinEffect
    {
        public override string Id => "Heal";
        public override bool IsGood => true;
        public override int DefaultWeight => 10;

        public override string DefaultMessage => "You've been magically healed!";

        public override void Execute(Player player)
        {
            player.Health = player.MaxHealth;
        }
    }

    public class AidEffect : SimpleCoinEffect
    {
        public override string Id => "Aid";
        public override bool IsGood => true;
        public override int DefaultWeight => 4;

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