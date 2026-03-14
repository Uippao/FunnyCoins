using System;
using System.Collections.Generic;
using System.Reflection;
using CustomPlayerEffects;
using LabApi.Features.Wrappers;
using UnityEngine;

namespace FunnyCoins.Effects
{
    public class BoomEffect : SimpleCoinEffect
    {
        public override string Id => "Boom";
        public override bool IsGood => false;
        public override int DefaultWeight => 9;

        public override string DefaultMessage => "Boom.";

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position + Vector3.up * 0.1f;

            TimedGrenadeProjectile.SpawnActive(
                pos,
                ItemType.GrenadeHE,
                player,
                2.0
            );
        }
    }
    
    public class HeartAttackEffect : SimpleCoinEffect
    {
        public override string Id => "HeartAttack";
        public override bool IsGood => false;
        public override int DefaultWeight => 6;

        public override string DefaultMessage => "You're having a heart attack!";

        public override void Execute(Player player)
        {
            player.EnableEffect<CardiacArrest>(1, 20);
        }
    }
    
    public class ToeStub : SimpleCoinEffect
    {
        public override string Id => "ToeStub";
        public override bool IsGood => false;
        public override int DefaultWeight => 13;

        public override string DefaultMessage => "You stubbed your toe";

        public override void Execute(Player player)
        {
            player.Health = player.Health * 0.3f;
        }
    }
    
    public class ConcussedEffect : SimpleCoinEffect
    {
        public override string Id => "Concussed";
        public override bool IsGood => false;
        public override int DefaultWeight => 16;

        public override string DefaultMessage => "Your head is spinning";

        public override void Execute(Player player)
        {
            player.EnableEffect<Concussed>(30, 10f);
        }
    }

    public class ExhaustedEffect : SimpleCoinEffect
    {
        public override string Id => "Exhausted";
        public override bool IsGood => false;
        public override int DefaultWeight => 14;

        public override string DefaultMessage => "You suddenly feel extremely tired";

        public override void Execute(Player player)
        {
            player.EnableEffect<Exhausted>(20, 35f);
            player.EnableEffect<Disabled>(1, 35f);
        }
    }
    
    public class CrippledEffect : SimpleCoinEffect
    {
        public override string Id => "Crippled";
        public override bool IsGood => false;
        public override int DefaultWeight => 11;

        public override string DefaultMessage => "Your legs give out from under you";

        public override void Execute(Player player)
        {
            player.EnableEffect<Ensnared>(1, 10f);
        }
    }
    
    public class DropItemsEffect : SimpleCoinEffect
    {
        public override string Id => "DropItems";
        public override bool IsGood => false;
        public override int DefaultWeight => 9;

        public override string DefaultMessage => "You fumble and drop your items";

        public override void Execute(Player player)
        {
            player.DropEverything();
        }
    }
    
    public class AllCoinsEffect : SimpleCoinEffect
    {
        public override string Id => "AllCoins";
        public override bool IsGood => false;
        public override int DefaultWeight => 6;

        public override string DefaultMessage => "All your items have turned into coins";
        public override float DefaultMessageDuration => 5f;

        public override void Execute(Player player)
        {
            var itemsCopy = new List<Item>(player.Items);

            foreach (var item in itemsCopy)
            {
                player.RemoveItem(item);
                player.AddItem(ItemType.Coin);
            }
        }

    }

    public class ClearInventoryEffect : SimpleCoinEffect
    {
        public override string Id => "ClearInventory";
        public override bool IsGood => false;
        public override int DefaultWeight => 8;

        public override string DefaultMessage => "All your items have disappeared";

        public override void Execute(Player player)
        {
            player.ClearInventory();
        }
    }
    
    public class RandomBadEffect : SimpleCoinEffect
    {
        public override string Id => "RandomBadEffect";
        public override bool IsGood => false;
        public override int DefaultWeight => 13;

        public override string DefaultMessage => "Something terrible has happened to you";

        public override void Execute(Player player)
        {
            var effectAction = Utils.BadStatusEffects[
                FunnyCoins.Rng.Next(Utils.BadStatusEffects.Count)
            ];

            effectAction(player);
        }
    }
    
    public class SeveredHandsEffect : SimpleCoinEffect
    {
        public override string Id => "SeveredHands";
        public override bool IsGood => false;
        public override int DefaultWeight => 4;

        public override string DefaultMessage => "Eh, you never needed those anyway";
        public override float DefaultMessageDuration => 5f;

        public override void Execute(Player player)
        {
            player.EnableEffect<SeveredHands>();
        }
    }
    
    public class SeveredEyesEffect : SimpleCoinEffect
    {
        public override string Id => "SeveredEyes";
        public override bool IsGood => false;
        public override int DefaultWeight => 4;

        public override string DefaultMessage => "Eyes are overrated";

        public override void Execute(Player player)
        {
            player.EnableEffect<SeveredEyes>();
        }
    }
}