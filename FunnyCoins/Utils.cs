using System;
using System.Collections.Generic;
using CustomPlayerEffects;
using InventorySystem.Items.Usables.Scp244.Hypothermia;
using LabApi.Features.Wrappers;
using PlayerRoles;

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
            ItemType.SCP018,
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
    }
}