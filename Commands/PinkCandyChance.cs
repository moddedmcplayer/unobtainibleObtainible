using System;
using CommandSystem;
using HarmonyLib;

namespace unobtainibleObtainible.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class PinkCandyChance : ICommand
    {
        public string Command { get; } = "pinkCandyChance";
        public string[] Aliases { get; } = { "pcc" };
        public string Description { get; } = "Sets the chance of the next candy gotten to be pink";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count == 0)
            {
                response = "please provide an argument!";
                return false;
            }

            if (arguments.At(0) == "null" || arguments.At(0) == "reset")
            {
                Plugin.Instance.RAChance = -1f;
                Plugin.Instance.harmony.UnpatchAll(Plugin.Instance.harmony.Id);
                Plugin.Instance.harmony.PatchAll();
                response = "Reset pink candy weight to default!";
                return true;
            }

            float output;
            if (!float.TryParse(arguments.At(0), out output) || (output < 0))
            {
                response = "Please enter a valid number above 0!";
                return false;
            }

            Plugin.Instance.RAChance = float.Parse(arguments.At(0));
            Plugin.Instance.harmony.UnpatchAll(Plugin.Instance.harmony.Id);
            // Plugin.Instance.harmony = null;
            // Plugin.Instance.harmony = new Harmony($"unobtainibleObtainible.{DateTime.UtcNow.Ticks}");
            Plugin.Instance.harmony.PatchAll();
            response = $"Set the pink candy weight to {float.Parse(arguments.At(0))}";
            return true;
        }
    }
}