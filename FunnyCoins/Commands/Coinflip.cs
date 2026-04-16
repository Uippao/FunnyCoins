using System;
using System.Text;
using CommandSystem;
using LabApi.Features.Permissions;
using FunnyCoins.Effects;
using LabApi.Features.Wrappers;

namespace FunnyCoins.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Coinflip : ICommand
    {
        public string Command { get; set; } = "coinflip";
        public string[] Aliases { get; set; } = { "cf" };
        public string Description => "Simulates a coinflip. Use an effect ID to run a specific effect, or 'list' to list all effects.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.HasPermissions("funnycoins.admin"))
            {
                response = "You don't have permission to use this command.";
                return false;
            }

            Player player = Player.Get(sender);
            if (player == null || !player.IsAlive)
            {
                response = "The command can only be run by an alive player.";
                return false;
            }

            string arg = arguments.Count > 0 ? arguments.At(0).ToLowerInvariant() : null;

            if (arg == "list")
            {
                var sb = new StringBuilder();

                sb.AppendLine("Good Effects:");
                foreach (var e in EffectRegistry.GoodEffects)
                {
                    sb.AppendLine($"- {e.Id}" + (Utils.IsExternal(e) ? " [EXT]" : ""));
                }

                sb.AppendLine("Bad Effects:");
                foreach (var e in EffectRegistry.BadEffects)
                {
                    sb.AppendLine($"- {e.Id}" + (Utils.IsExternal(e) ? " [EXT]" : ""));
                }

                response = sb.ToString();
                return true;
            }

            ICoinEffect effect;

            if (string.IsNullOrEmpty(arg))
            {
                bool good = FunnyCoins.Rng.NextDouble() < FunnyCoins.Instance.Config.GoodEffectChance;
                var pool = good ? EffectRegistry.GoodEffects : EffectRegistry.BadEffects;
                effect = EffectRegistry.PickRandom(pool);
            }
            else
            {
                effect = EffectRegistry.GetEffectById(arg);
                if (effect == null)
                {
                    response = $"Effect with ID '{arg}' not found.";
                    return false;
                }
            }

            effect.Execute(player);
            if (!effect.HandlesOwnMessage)
                FunnyCoins.Instance.ShowEffectMessage(player, effect);

            response = $"Coinflip executed: {(string.IsNullOrEmpty(arg) ? effect.Id : arg)}";
            return true;
        }
    }
}