using System;
using System.Collections.Generic;
using CloudflareSolverRe.Extensions;
using CommandSystem;

namespace unobtainibleObtainible.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class respawnItems : ICommand
    {
        public string Command { get; } = "spawnItems";
        public string[] Aliases { get; } = {"spIt"};
        public string Description { get; } = "Spawns normally unobtainible items again";
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            List<string> loc = new List<string>();
            
            if(Plugin.Instance.Config.spawnX3)
                Plugin.apiInstance.SpawnItem(Plugin.Instance.Config.SpawnsX3, (ushort)Plugin.Instance.Config.LimitX3, ItemType.ParticleDisruptor);
            response = $"Spawned items!";
            return true;
        }
    }
}