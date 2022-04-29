using System;
using System.Collections.Generic;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using InventorySystem.Items;

namespace unobtainibleObtainible.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class reload : ICommand
    {
        public string Command { get; } = "reload";
        public string[] Aliases { get; } = {"r"};
        public string Description { get; } = "Reloads the 3-X particle accelerator";
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player ply = Player.Get(sender);
            if (ply.CurrentItem == null || !(ply.CurrentItem.Type == ItemType.GrenadeFlash))
            {
                response = "You need to be holding a Flashbang in order to do this!";
                return false;
            }
            if (ply.HasItem(ItemType.ParticleDisruptor))
            {
                foreach (var item in ply.Items)
                {
                    if (item.Type == ItemType.ParticleDisruptor)
                    {
                        if (item is Firearm firearm)
                        {
                            if(ply.RemoveHeldItem())
                                firearm.Ammo += (byte)Plugin.Instance.Config.AmmoMult;
                            break;
                        }
                    }
                }

                if (Plugin.Instance.Config.AmmoMult == 1)
                {
                    response = "Gave the 3-X a extra bullet!";
                    return true;
                }
                else
                {
                    response = $"Gave the 3-X {Plugin.Instance.Config.AmmoMult} extra bullets!";
                    return true;
                }
                
            }
            else
            {
                response = "You need a 3-X Particle Accelerator in order to do this!";
                return false;
            }
        }
    }
}