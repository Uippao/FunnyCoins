using LabApi.Features.Wrappers;
using InventorySystem.Items;
using InventorySystem.Items.ThrowableProjectiles;
using UnityEngine;

namespace FunnyCoins.Effects
{
    public class BoomEffect : ICoinEffect
    {
        public string Id => "Boom";
        public bool IsGood => false;
        public int DefaultWeight => 7;

        public string DefaultMessage => "Boom.";

        public void Execute(Player player)
        {
            Vector3 pos = player.Position + Vector3.up * 0.1f;

            TimedGrenadeProjectile grenade = TimedGrenadeProjectile.SpawnActive(
                pos,
                ItemType.GrenadeHE,
                player,
                2.5
            );
        }
    }
}