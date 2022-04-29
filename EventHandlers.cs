using System.Collections.Generic;
using System.Linq;
using CustomPlayerEffects;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs;
using Footprinting;
using InventorySystem.Items.ThrowableProjectiles;
using InventorySystem.Items.Usables.Scp330;
using MEC;
using Random = UnityEngine.Random;

namespace unobtainibleObtainible
{
    public class EventHandlers
    {
        private Config cfg;

        public void OnStartingRound()
        {
            Timing.CallDelayed(4f, () =>
            {
                if(cfg.spawnX3)
                    Plugin.apiInstance.SpawnItem(cfg.SpawnsX3, (ushort)cfg.LimitX3, ItemType.ParticleDisruptor);
            });
        }
        
        public void OnWaitingForPlayers()
        {
            Plugin.Instance.RAChance = -1f;
        }

        public void OnEatingCandy(EatingScp330EventArgs ev)
        {
            if (ev.Candy.Kind == CandyKindID.Pink && Plugin.Instance.Config.enablePinkBoost)
            {
                Scp330Bag bag;
                Scp330Bag.TryGetBag(ev.Player.ReferenceHub, out bag);
                int grenades = ev.Player.Items.ToList().Where(x => x.Type == ItemType.GrenadeHE).Count();
                int otherCandy = bag.Candies.Where(x => x == CandyKindID.Pink).Count() - 1;
                if(grenades > 0)
                {
                    foreach (var item in ev.Player.Items)
                    {
                        if (item.Type == ItemType.GrenadeHE)
                        {
                            ev.Player.RemoveItem(item);
                            break;
                        }
                    }

                    ev.Player.IsGodModeEnabled = true;
                    if (otherCandy > 0)
                    {
                        for(int i = 0; i < otherCandy; i++)
                        {
                            ev.Candy.ServerApplyEffects(ev.Player.ReferenceHub);
                            Timing.WaitForSeconds(0.2f);
                        }
                    }
                    ev.Player.ShowHint($"Grenades Left: {grenades -1}");
                    Timing.CallDelayed(1f, () => ev.Player.IsGodModeEnabled = false);
                }
            }
        }

        public EventHandlers(Plugin plugin)
        {
            cfg = plugin.Config;
        }
    }
}