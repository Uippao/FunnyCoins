using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cassie;
using CustomPlayerEffects;
using LabApi.Features.Wrappers;
using PlayerRoles;
using UnityEngine;

namespace FunnyCoins.Effects
{
    public class GrenadeEffect : SimpleCoinEffect
    {
        public override string Id => "Grenade";
        public override bool IsGood => false;
        public override int DefaultWeight => 15;

        public override string DefaultMessage => "You like grenades?";

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
    
    public class BoomEffect : SimpleCoinEffect
    {
        public override string Id => "Boom";
        public override bool IsGood => false;
        public override int DefaultWeight => 9;

        public override string DefaultMessage => "Boom.";

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;

            TimedGrenadeProjectile.SpawnActive(
                pos,
                ItemType.GrenadeHE,
                player,
                0.0
            );
        }
    }
    
    public class HeartAttackEffect : SimpleCoinEffect
    {
        public override string Id => "HeartAttack";
        public override bool IsGood => false;
        public override int DefaultWeight => 11;

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
        public override int DefaultWeight => 20;

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
        public override int DefaultWeight => 18;

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
        public override int DefaultWeight => 17;

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
        public override int DefaultWeight => 13;

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
        public override int DefaultWeight => 16;

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
        public override int DefaultWeight => 10;

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
        public override int DefaultWeight => 15;

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
        public override int DefaultWeight => 5;

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
            player.EnableEffect<Blindness>(100);
        }
    }
    
    public class DeadManEffect : ICoinEffect
    {
        public string Id => "DeadMan";
        public bool IsGood => false;
        public int DefaultWeight => 1;

        public bool HandlesOwnMessage => true;
        public string DefaultMessage => null;

        public IEnumerable<EffectMessageDefinition> DefaultMessages => new[]
        {
            new EffectMessageDefinition("detonated", "Hope you like balls", 5f),
            new EffectMessageDefinition("started", "I guess the O5s didn't approve of this...", 5f),
            new EffectMessageDefinition("stopped", "You might not die today after all", 5f)
        };

        public void Execute(Player player)
        {
            if (Warhead.IsDetonated)
            {
                FunnyCoins.Instance.ShowEffectMessage(player, this, "detonated");
                
                Vector3 pos = player.Position + Vector3.up * 1.7f;

                TimedGrenadeProjectile.SpawnActive(
                    pos,
                    ItemType.SCP018,
                    player
                );

                TimedGrenadeProjectile.SpawnActive(
                    pos,
                    ItemType.SCP018,
                    player
                );

                TimedGrenadeProjectile.SpawnActive(
                    pos,
                    ItemType.SCP018,
                    player
                );
                return;
            }

            if (DeadmanSwitch.IsSequenceActive)
            {
                DeadmanSwitch.Reset();
                Warhead.IsLocked = false;
                Warhead.Stop();
                FunnyCoins.Instance.ShowEffectMessage(player, this, "stopped");
            }
            else
            {
                DeadmanSwitch.InitiateProtocol();
                FunnyCoins.Instance.ShowEffectMessage(player, this, "started");
            }
        }
    }
    
    public class NukeEffect : ICoinEffect
    {
        public string Id => "Nuke";
        public bool IsGood => false;
        public int DefaultWeight => 2;

        public bool HandlesOwnMessage => true;
        public string DefaultMessage => null;

        public IEnumerable<EffectMessageDefinition> DefaultMessages => new[]
        {
            new EffectMessageDefinition("detonated", "Hope you like balls", 4f),
            new EffectMessageDefinition("started", "Time to go", 4f),
            new EffectMessageDefinition("stopped", "The mighty coin has saved the facility", 6f)
        };

        public void Execute(Player player)
        {
            if (Warhead.IsDetonated)
            {
                FunnyCoins.Instance.ShowEffectMessage(player, this, "detonated");
                
                Vector3 pos = player.Position + Vector3.up * 1.7f;

                TimedGrenadeProjectile.SpawnActive(
                    pos,
                    ItemType.SCP018,
                    player
                );

                TimedGrenadeProjectile.SpawnActive(
                    pos,
                    ItemType.SCP018,
                    player
                );

                TimedGrenadeProjectile.SpawnActive(
                    pos,
                    ItemType.SCP018,
                    player
                );
                return;
            }

            if (Warhead.IsDetonationInProgress)
            {
                Warhead.Stop();
                FunnyCoins.Instance.ShowEffectMessage(player, this, "stopped");
            }
            else
            {
                Warhead.Start();
                FunnyCoins.Instance.ShowEffectMessage(player, this, "started");
            }
        }
    }
    
    public class RoleSwapEffect : SimpleCoinEffect
    {
        public override string Id => "RoleSwap";
        public override bool IsGood => false;
        public override int DefaultWeight => 22;

        public override string DefaultMessage => "Switcheroo!";

        public override void Execute(Player player)
        {
            if (Utils.OppositeRoles.TryGetValue(player.Role, out var opposite))
            {
                player.SetRole(opposite, flags: RoleSpawnFlags.None);
            }
            else
            {
                player.SetRole(RoleTypeId.Scp3114);
                player.Health = 10f;
                player.MaxHealth = 10f;
                player.MaxHumeShield = 20f;
            }
        }
    }

    public class ZombifiedEffect : SimpleCoinEffect
    {
        public override string Id => "Zombified";
        public override bool IsGood => false;
        public override int DefaultWeight => 11;
        
        public override string DefaultMessage => "Get zombified loser";

        public override void Execute(Player player)
        {
            player.SetRole(RoleTypeId.Scp0492);
        }
    }
    
    public class TeleportToScpEffect : ICoinEffect
    {
        public string Id => "TeleportToScp";
        public bool IsGood => false;
        public int DefaultWeight => 16;

        public bool HandlesOwnMessage => true;

        public string DefaultMessage => null;

        public IEnumerable<EffectMessageDefinition> DefaultMessages => new[]
        {
            new EffectMessageDefinition("teleported", "Good luck!", 4f),
            new EffectMessageDefinition("noscp", "Oh you lucky bastard...", 4f)
        };

        public void Execute(Player player)
        {
            var scps = Player.List
                .Where(p => p.IsSCP && p.IsAlive)
                .ToArray();

            if (scps.Length == 0)
            {
                Pickup.Create(ItemType.Painkillers, player.Position).Spawn();

                FunnyCoins.Instance.ShowEffectMessage(player, this, "noscp");
                return;
            }

            var nearestScp = scps
                .OrderBy(scp => Vector3.Distance(player.Position, scp.Position))
                .First();

            player.Position = nearestScp.Position;

            FunnyCoins.Instance.ShowEffectMessage(player, this, "teleported");
        }
    }
    
    public class PocketEffect : SimpleCoinEffect
    {
        public override string Id => "Pocket";
        public override bool IsGood => false;
        public override int DefaultWeight => 14;

        public override string DefaultMessage => "Welcome to the shadow realm";
        public override float DefaultMessageDuration => 5f;

        public override void Execute(Player player)
        {
            PocketDimension.ForceInside(player);
        }
    }
    
    public class SwapPositionEffect : ICoinEffect
    {
        public string Id => "SwapPosition";
        public bool IsGood => false;
        public int DefaultWeight => 19;

        public bool HandlesOwnMessage => true;

        public string DefaultMessage => null;

        public IEnumerable<EffectMessageDefinition> DefaultMessages => new[]
        {
            new EffectMessageDefinition("swapped", "You swapped places with someone!", 5f),
            new EffectMessageDefinition("notarget", "Nobody to swap with... unlucky.", 5f)
        };

        public void Execute(Player player)
        {
            var targets = Player.List
                .Where(p => p != player && p.IsAlive)
                .ToList();

            if (targets.Count == 0)
            {
                FunnyCoins.Instance.ShowEffectMessage(player, this, "notarget");
                return;
            }

            var target = targets[FunnyCoins.Rng.Next(targets.Count)];

            Vector3 pos1 = player.Position;
            Vector3 pos2 = target.Position;

            player.Position = pos2;
            target.Position = pos1;

            FunnyCoins.Instance.ShowEffectMessage(player, this, "swapped");
            FunnyCoins.Instance.ShowEffectMessage(target, this, "swapped");
        }
    }
}