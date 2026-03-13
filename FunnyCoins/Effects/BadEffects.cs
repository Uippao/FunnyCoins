using LabApi.Features.Wrappers;
using InventorySystem.Items;
using InventorySystem.Items.ThrowableProjectiles;
using UnityEngine;

namespace FunnyCoins.Effects
{
    public class BoomEffect : SimpleCoinEffect
    {
        public override string Id => "Boom";
        public override bool IsGood => false;
        public override int DefaultWeight => 7;

        public override string DefaultMessage => "Boom.";

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position + Vector3.up * 0.1f;

            TimedGrenadeProjectile.SpawnActive(
                pos,
                ItemType.GrenadeHE,
                player,
                2.5
            );
        }
    }
}