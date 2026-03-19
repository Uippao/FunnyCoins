using System;
using System.Collections.Generic;
using CustomPlayerEffects;
using FunnyCoins.Effects;
using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Modules;
using InventorySystem.Items.Usables.Scp244.Hypothermia;
using LabApi.Features.Wrappers;
using PlayerRoles;
using UnityEngine;
using FirearmPickup = LabApi.Features.Wrappers.FirearmPickup;

namespace FunnyCoins
{
    public class Utils
    {
        public static readonly IReadOnlyList<Action<Player>> BadStatusEffects = new Action<Player>[]
        {
            p => p.EnableEffect<Flashed>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 10f)
            ),
            p => p.EnableEffect<Deafened>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 30f)
            ),
            p => p.EnableEffect<Burned>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 30f)
            ),
            p => p.EnableEffect<Hemorrhage>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 30f)
            ),
            p => p.EnableEffect<Slowness>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 30f)
            ),
            p => p.EnableEffect<CardiacArrest>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 30f)
            ),
            p => p.EnableEffect<Poisoned>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 30f)
            ),
            p => p.EnableEffect<Sinkhole>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 30f)
            ),
            p => p.EnableEffect<Hypothermia>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 30f)
            ),
            p => p.EnableEffect<Disabled>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 30f)
            ),
            p => p.EnableEffect<Blurred>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 30f)
            ),
            p => p.EnableEffect<Asphyxiated>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 30f)
            )
        };
        
        public static readonly IReadOnlyList<Action<Player>> GoodStatusEffects = new Action<Player>[]
        {
            p => p.EnableEffect<Invigorated>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 20f)
            ),
            p => p.EnableEffect<Invisible>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 10f)
            ),
            p => p.EnableEffect<MovementBoost>(
                (byte)UnityEngine.Random.Range(10, 30),
                UnityEngine.Random.Range(10f, 40f)
            ),
            p => p.EnableEffect<DamageReduction>(
                (byte)UnityEngine.Random.Range(50, 100),
                UnityEngine.Random.Range(5f, 30f)
            ),
            p => p.EnableEffect<Vitality>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(20f, 120f)
            ),
            p => p.EnableEffect<SilentWalk>(
                (byte)UnityEngine.Random.Range(5, 100),
                UnityEngine.Random.Range(5f, 30f)
            )
        };
        
        public static readonly IReadOnlyDictionary<RoleTypeId, RoleTypeId> OppositeRoles = new Dictionary<RoleTypeId, RoleTypeId>
        { 
            { RoleTypeId.ClassD, RoleTypeId.Scientist },
            { RoleTypeId.Scientist, RoleTypeId.ClassD },

            { RoleTypeId.FacilityGuard, RoleTypeId.ChaosConscript },
            { RoleTypeId.NtfPrivate, RoleTypeId.ChaosRifleman },
            { RoleTypeId.NtfSergeant, RoleTypeId.ChaosMarauder },
            { RoleTypeId.NtfSpecialist, RoleTypeId.ChaosConscript },
            { RoleTypeId.NtfCaptain, RoleTypeId.ChaosRepressor },
            
            { RoleTypeId.ChaosConscript, RoleTypeId.NtfSpecialist }, 
            { RoleTypeId.ChaosRifleman, RoleTypeId.NtfPrivate }, 
            { RoleTypeId.ChaosMarauder, RoleTypeId.NtfSergeant }, 
            { RoleTypeId.ChaosRepressor, RoleTypeId.NtfCaptain },
            
            { RoleTypeId.Tutorial, RoleTypeId.Scp0492 }
        };

        public static readonly List<ItemType> SpecialWeapons = new List<ItemType>
        {
            ItemType.MicroHID,
            ItemType.Jailbird,
            ItemType.ParticleDisruptor,
            ItemType.GunSCP127,
            ItemType.SCP018,
            ItemType.SCP1509,
            ItemType.GunCom45,
            ItemType.GunAK
        };
        
        public static readonly List<ItemType> BadKeycards = new List<ItemType>
        {
            ItemType.KeycardJanitor,
            ItemType.KeycardZoneManager,
            ItemType.KeycardScientist,
            ItemType.KeycardGuard,
            ItemType.KeycardResearchCoordinator
        };
        
        public static readonly List<ItemType> GoodKeycards = new List<ItemType>
        {
            ItemType.KeycardMTFOperative,
            ItemType.KeycardMTFCaptain,
            ItemType.KeycardChaosInsurgency,
            ItemType.KeycardFacilityManager
        };
        
        public static readonly List<ItemType> WeirdKeycards = new List<ItemType>
        {
            ItemType.KeycardMTFPrivate,
            ItemType.KeycardContainmentEngineer
        };
        
        public static readonly List<ItemType> Headgear = new List<ItemType>
        {
            ItemType.SCP268,
            ItemType.SCP1344
        };
        
        public static readonly List<ItemType> Armor = new List<ItemType>
        {
            ItemType.ArmorLight,
            ItemType.ArmorCombat,
            ItemType.ArmorHeavy
        };
        
        public static readonly List<ItemType> LMGs = new List<ItemType>
        {
            ItemType.GunFRMG0,
            ItemType.GunLogicer
        };
        
        public static readonly List<ItemType> Rifles = new List<ItemType>
        {
            ItemType.GunE11SR,
            ItemType.GunAK,
            ItemType.GunA7
        };
        
        public static void SpawnGunWithAmmo(ItemType gunType, Vector3 position)  
        {  
            Pickup pickup = Pickup.Create(gunType, position);  
            ItemType ammoType = GetFirearmAmmoType(gunType);  
      
            int maxAmmo = GetFirearmMaxAmmo(gunType);  
  
            if (ammoType != ItemType.None && maxAmmo > 0)
            {
                int ammoPerPickup = GetAmmoPerPickup(ammoType);
                int ammoToSpawn = maxAmmo;
                int pickupsNeeded = Mathf.CeilToInt((float)ammoToSpawn / ammoPerPickup);
          
                for (int i = 0; i < pickupsNeeded; i++)
                {
                    int amountInThisPickup = Mathf.Min(ammoPerPickup, ammoToSpawn);
                    Vector3 pickupPos = position + new Vector3(i * 0.2f, 0, 0);
              
                    Pickup ammoPickup = Pickup.Create(ammoType, pickupPos);
                    ammoPickup?.Spawn();
              
                    ammoToSpawn -= amountInThisPickup;
                }
            }
            
            pickup?.Spawn();
        }

        public static ItemType GetFirearmAmmoType(ItemType itemType)
        {
            if (InventoryItemLoader.AvailableItems.TryGetValue(itemType, out ItemBase itemBase) &&
                itemBase is Firearm firearm)
            {
                FirearmItem firearmItem = FirearmItem.Get(firearm);
                return firearmItem.AmmoType;
            }
            return ItemType.None;
        }
        
        public static int GetFirearmMaxAmmo(ItemType itemType)
        {
            if (InventoryItemLoader.AvailableItems.TryGetValue(itemType, out ItemBase itemBase) &&
                itemBase is Firearm firearm)
            {
                FirearmItem firearmItem = FirearmItem.Get(firearm);
                return firearmItem.MaxAmmo;
            }
            return 20;
        }
        
        private static readonly Dictionary<ItemType, int> AmmoPerPickup = new Dictionary<ItemType, int>() 
        {  
            { ItemType.Ammo9x19, 15 },
            { ItemType.Ammo556x45, 40 },
            { ItemType.Ammo762x39, 30 },
            { ItemType.Ammo44cal, 6 },
            { ItemType.Ammo12gauge, 14 }
        };
        
        private static int GetAmmoPerPickup(ItemType ammoType)
        {
            if (AmmoPerPickup.TryGetValue(ammoType, out int value))
                return value;
            return 20;
        }
        
        internal static bool IsExternal(ICoinEffect effect)
        {
            return effect.GetType().Assembly != typeof(FunnyCoins).Assembly;
        }
    }
}