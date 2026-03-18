using System.Collections.Generic;
using System.Linq;
using CustomPlayerEffects;
using LabApi.Features.Wrappers;
using UnityEngine;

namespace FunnyCoins.Effects
{
    public class StrengthenedEffect : ICoinEffect
    {
        public string Id => "Strengthened";
        public bool IsGood => true;
        public int DefaultWeight => 12;

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
        public override int DefaultWeight => 18;

        public override string DefaultMessage => "You've received some humanitarian aid!";

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;

            Pickup.Create(ItemType.Medkit, pos).Spawn();
            Pickup.Create(ItemType.Painkillers, pos).Spawn();
            Pickup.Create(ItemType.Painkillers, pos).Spawn();
        }
    }
    
    public class RandomGoodEffect : SimpleCoinEffect
    {
        public override string Id => "RandomGoodEffect";
        public override bool IsGood => true;
        public override int DefaultWeight => 15;

        public override string DefaultMessage => "You feel better in some way";
        public override float DefaultMessageDuration => 5f;

        public override void Execute(Player player)
        {
            var effectAction = Utils.GoodStatusEffects[
                FunnyCoins.Rng.Next(Utils.GoodStatusEffects.Count)
            ];

            effectAction(player);
        }
    }
    
    public class GhostEffect : SimpleCoinEffect
    {
        public override string Id => "Ghost";
        public override bool IsGood => true;
        public override int DefaultWeight => 5;

        public override string DefaultMessage => "You became a ghost (temporarily)";
        public override float DefaultMessageDuration => 5f;

        public override void Execute(Player player)
        {
            player.EnableEffect<Fade>(80, 15);
            player.EnableEffect<Ghostly>(1, 15);
        }
    }
    
    public class StealthEffect : SimpleCoinEffect
    {
        public override string Id => "Stealth";
        public override bool IsGood => true;
        public override int DefaultWeight => 7;

        public override string DefaultMessage => "You became stealthier!";

        public override void Execute(Player player)
        {
            player.EnableEffect<SilentWalk>(10);
        }
    }
    
    public class InvisibilityEffect : SimpleCoinEffect
    {
        public override string Id => "Invisibility";
        public override bool IsGood => true;
        public override int DefaultWeight => 6;

        public override string DefaultMessage => "You became temporarily invisible";
        public override float DefaultMessageDuration => 5f;

        public override void Execute(Player player)
        {
            player.EnableEffect<Invisible>(1, 7);
        }
    }
    
    public class BetterVisionEffect : SimpleCoinEffect
    {
        public override string Id => "BetterVision";
        public override bool IsGood => true;
        public override int DefaultWeight => 4;

        public override string DefaultMessage => "Your vision improved";

        public override void Execute(Player player)
        {
            player.EnableEffect<Scp1344>();
            player.EnableEffect<NightVision>(20);
            player.EnableEffect<FogControl>();
        }
    }
    
    public class HandlingEffect : SimpleCoinEffect
    {
        public override string Id => "Handling";
        public override bool IsGood => true;
        public override int DefaultWeight => 16;

        public override string DefaultMessage => "You got better at handling weapons";
        public override float DefaultMessageDuration => 5f;

        public override void Execute(Player player)
        {
            player.EnableEffect<Scp1853>();
        }
    }
    
    public class DamageReductionEffect : SimpleCoinEffect
    {
        public override string Id => "DamageReduction";
        public override bool IsGood => true;
        public override int DefaultWeight => 7;

        public override string DefaultMessage => "Your skin just got harder";

        public override void Execute(Player player)
        {
            player.EnableEffect<DamageReduction>(100);
            player.EnableEffect<BodyshotReduction>(50);
        }
    }
    
    public class Scp500Effect : SimpleCoinEffect
    {
        public override string Id => "SCP500";
        public override bool IsGood => true;
        public override int DefaultWeight => 16;

        public override string DefaultMessage => "You got some nice meds!";
        public override float DefaultMessageDuration => 5f;

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;

            Pickup.Create(ItemType.SCP500, pos).Spawn();
            Pickup.Create(ItemType.SCP500, pos).Spawn();
        }
    }
    
    public class CoinEffect : SimpleCoinEffect
    {
        public override string Id => "Coin";
        public override bool IsGood => true;
        public override int DefaultWeight => 11;

        public override string DefaultMessage => "You got more coins!";

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;

            Pickup.Create(ItemType.Coin, pos).Spawn();
            Pickup.Create(ItemType.Coin, pos).Spawn();
        }
    }
    
    public class Com15Effect : SimpleCoinEffect
    {
        public override string Id => "COM15";
        public override bool IsGood => true;
        public override int DefaultWeight => 12;

        public override string DefaultMessage => "Use it carefully...";

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;

            Pickup.Create(ItemType.GunCOM15, pos).Spawn();
        }
    }
    
    public class MicroEffect : SimpleCoinEffect
    {
        public override string Id => "Micro";
        public override bool IsGood => true;
        public override int DefaultWeight => 9;

        public override string DefaultMessage => "Time to make a microwave meal";

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;

            Pickup.Create(ItemType.MicroHID, pos).Spawn();
        }
    }
    
    public class SpecialWeaponEffect : SimpleCoinEffect
    {
        public override string Id => "SpecialWeapon";
        public override bool IsGood => true;
        public override int DefaultWeight => 6;

        public override string DefaultMessage => "Quite the weapon you have there";

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;
            ItemType item = Utils.SpecialWeapons[
                FunnyCoins.Rng.Next(Utils.SpecialWeapons.Count)
            ];

            Pickup.Create(item, pos).Spawn();
        }
    }
    
    public class BadKeycardEffect : SimpleCoinEffect
    {
        public override string Id => "BadKeycard";
        public override bool IsGood => true;
        public override int DefaultWeight => 17;

        public override string DefaultMessage => "There, have a keycard";

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;
            ItemType card = Utils.BadKeycards[
                FunnyCoins.Rng.Next(Utils.BadKeycards.Count)
            ];

            Pickup.Create(card, pos).Spawn();
        }
    }
    
    public class GoodKeycardEffect : SimpleCoinEffect
    {
        public override string Id => "GoodKeycard";
        public override bool IsGood => true;
        public override int DefaultWeight => 9;

        public override string DefaultMessage => "Just a little something to get you going";
        public override float DefaultMessageDuration => 5f;

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;
            ItemType card = Utils.GoodKeycards[
                FunnyCoins.Rng.Next(Utils.GoodKeycards.Count)
            ];

            Pickup.Create(card, pos).Spawn();
        }
    }
    
    public class WeirdKeycardEffect : SimpleCoinEffect
    {
        public override string Id => "WeirdKeycard";
        public override bool IsGood => true;
        public override int DefaultWeight => 13;

        public override string DefaultMessage => "Now I haven't seen that thing in a long time...";
        public override float DefaultMessageDuration => 6f;

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;
            ItemType card = Utils.WeirdKeycards[
                FunnyCoins.Rng.Next(Utils.WeirdKeycards.Count)
            ];

            Pickup.Create(card, pos).Spawn();
        }
    }
    
    public class O5Effect : SimpleCoinEffect
    {
        public override string Id => "O5";
        public override bool IsGood => true;
        public override int DefaultWeight => 7;

        public override string DefaultMessage => "They keys to the kingdom have been granted";
        public override float DefaultMessageDuration => 6f;

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;

            Pickup.Create(ItemType.KeycardO5, pos).Spawn();
        }
    }
    
    public class HeadgearEffect : SimpleCoinEffect
    {
        public override string Id => "Headgear";
        public override bool IsGood => true;
        public override int DefaultWeight => 7;

        public override string DefaultMessage => "You got some very fashionable headgear!";
        public override float DefaultMessageDuration => 5f;

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;
            ItemType item = Utils.Headgear[
                FunnyCoins.Rng.Next(Utils.Headgear.Count)
            ];

            Pickup.Create(item, pos).Spawn();
        }
    }
    
    public class VaseEffect : SimpleCoinEffect
    {
        public override string Id => "Vase";
        public override bool IsGood => true;
        public override int DefaultWeight => 16;

        public override string DefaultMessage => "Granny!?";

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;
            ItemType scp244 = FunnyCoins.Rng.Next(2) == 0
                ? ItemType.SCP244a
                : ItemType.SCP244b;

            Pickup.Create(scp244, pos).Spawn();
        }
    }
    
    public class DrinkEffect : SimpleCoinEffect
    {
        public override string Id => "Drink";
        public override bool IsGood => true;
        public override int DefaultWeight => 8;

        public override string DefaultMessage => "Thirsty?";

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;

            Pickup.Create(ItemType.SCP207, pos).Spawn();
            Pickup.Create(ItemType.AntiSCP207, pos).Spawn();
        }
    }
    
    public class Candy : SimpleCoinEffect
    {
        public override string Id => "Candy";
        public override bool IsGood => true;
        public override int DefaultWeight => 10;

        public override string DefaultMessage => "You got some sweets";

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;
            
            if (player.IsInventoryFull)
            {
                Item randomItem = player.Items.ElementAtOrDefault(UnityEngine.Random.Range(0, player.Items.Count()));  
                if (randomItem != null)  
                {  
                    player.DropItem(randomItem);  
                }
            }
            
            for (int i = 0; i < Scp330Item.MaxCandies; i++)  
            {
                player.GiveRandomCandy();
            }
        }
    }
    
    public class AdrenalineEffect : SimpleCoinEffect
    {
        public override string Id => "Adrenaline";
        public override bool IsGood => true;
        public override int DefaultWeight => 16;

        public override string DefaultMessage => "I heard you wanted some adrenaline";
        public override float DefaultMessageDuration => 5f;

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;

            Pickup.Create(ItemType.Adrenaline, pos).Spawn();
            Pickup.Create(ItemType.Adrenaline, pos).Spawn();
            Pickup.Create(ItemType.Adrenaline, pos).Spawn();
            Pickup.Create(ItemType.Adrenaline, pos).Spawn();
        }
    }
    
    public class SurfacePassEffect : SimpleCoinEffect
    {
        public override string Id => "SurfacePass";
        public override bool IsGood => true;
        public override int DefaultWeight => 17;

        public override string DefaultMessage => "You want out, right?";

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;

            Pickup.Create(ItemType.SurfaceAccessPass, pos).Spawn();
        }
    }
    
    public class CombatKitEffect : SimpleCoinEffect
    {
        public override string Id => "CombatKit";
        public override bool IsGood => true;
        public override int DefaultWeight => 10;

        public override string DefaultMessage => "Take some cool gear";

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;
            ItemType armor = Utils.Armor[
                FunnyCoins.Rng.Next(Utils.Armor.Count)
            ];

            Pickup.Create(armor, pos).Spawn();
            Pickup.Create(ItemType.Ammo9x19, pos).Spawn();
            Pickup.Create(ItemType.Ammo9x19, pos).Spawn();
            Pickup.Create(ItemType.Ammo556x45, pos).Spawn();
            Pickup.Create(ItemType.GunCOM18, pos).Spawn();
            Pickup.Create(ItemType.Medkit, pos).Spawn();
        }
    }
    
    public class RevolverEffect : SimpleCoinEffect
    {
        public override string Id => "Revolver";
        public override bool IsGood => true;
        public override int DefaultWeight => 14;

        public override string DefaultMessage => "Wanna play some russian roulette?";
        public override float DefaultMessageDuration => 5f;

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;

            Pickup.Create(ItemType.GunRevolver, pos).Spawn();
        }
    }
    
    public class OldManEffect : SimpleCoinEffect
    {
        public override string Id => "OldMan";
        public override bool IsGood => true;
        public override int DefaultWeight => 16;

        public override string DefaultMessage => "Happy now, old man?";
        public override float DefaultMessageDuration => 5f;

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;

            Pickup.Create(ItemType.SCP1576, pos).Spawn();
            Pickup.Create(ItemType.Lantern, pos).Spawn();
        }
    }
    
    public class LMGEffect : SimpleCoinEffect
    {
        public override string Id => "LMG";
        public override bool IsGood => true;
        public override int DefaultWeight => 10;

        public override string DefaultMessage => "Time to make some noise!";
        public override float DefaultMessageDuration => 5f;

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;
            ItemType lmg = Utils.LMGs[
                FunnyCoins.Rng.Next(Utils.LMGs.Count)
            ];

            Pickup.Create(lmg, pos).Spawn();
        }
    }
    
    public class FSPEffect : SimpleCoinEffect
    {
        public override string Id => "FSP";
        public override bool IsGood => true;
        public override int DefaultWeight => 15;

        public override string DefaultMessage => "What will I do with this piece of garbage?";
        public override float DefaultMessageDuration => 6f;

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;

            Pickup.Create(ItemType.GunFSP9, pos).Spawn();
        }
    }
    
    public class GrenadesEffect : SimpleCoinEffect
    {
        public override string Id => "Grenades";
        public override bool IsGood => true;
        public override int DefaultWeight => 13;

        public override string DefaultMessage => "Here's a few 'nades for ya!";
        public override float DefaultMessageDuration => 5f;

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;

            Pickup.Create(ItemType.GrenadeHE, pos).Spawn();
            Pickup.Create(ItemType.GrenadeHE, pos).Spawn();
            Pickup.Create(ItemType.GrenadeFlash, pos).Spawn();
            Pickup.Create(ItemType.SCP2176, pos).Spawn();
        }
    }
    
    public class RifleEffect : SimpleCoinEffect
    {
        public override string Id => "Rifle";
        public override bool IsGood => true;
        public override int DefaultWeight => 12;

        public override string DefaultMessage => "Need a rifle?";

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;
            ItemType rifle = Utils.Rifles[
                FunnyCoins.Rng.Next(Utils.Rifles.Count)
            ];

            Pickup.Create(rifle, pos).Spawn();
        }
    }
    
    public class RadioEffect : SimpleCoinEffect
    {
        public override string Id => "Radio";
        public override bool IsGood => true;
        public override int DefaultWeight => 17;

        public override string DefaultMessage => "Hope this helps!";

        public override void Execute(Player player)
        {
            Vector3 pos = player.Position;

            Pickup.Create(ItemType.Radio, pos).Spawn();
            Pickup.Create(ItemType.Flashlight, pos).Spawn();
        }
    }
}