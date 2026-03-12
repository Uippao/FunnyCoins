using LabApi.Features.Wrappers;
using UnityEngine;
using LabApi.Features.Enums;

namespace FunnyCoins.Effects
{
    public class HealEffect : ICoinEffect
    {
        public string Id => "Heal";
        public bool IsGood => true;
        public int DefaultWeight => 10;

        public string DefaultMessage => "You've been magically healed!";

        public void Execute(Player player)
        {
            player.Health = player.MaxHealth;
        }
    }

    public class AidEffect : ICoinEffect
    {
        public string Id => "Aid";
        public bool IsGood => true;
        public int DefaultWeight => 4;

        public string DefaultMessage => "You've received some humanitarian aid!";

        public void Execute(Player player)
        {
            Vector3 pos = player.Position;

            Pickup medkit = Pickup.Create(ItemType.Medkit, pos);
            Pickup painkillers1 = Pickup.Create(ItemType.Painkillers, pos);
            Pickup painkillers2 = Pickup.Create(ItemType.Painkillers, pos);
            
            medkit.Spawn();
            painkillers1.Spawn();
            painkillers2.Spawn();
        }
    }
}