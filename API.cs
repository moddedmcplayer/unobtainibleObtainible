using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using UnityEngine;

namespace unobtainibleObtainible
{
    public class API
    {
        public void SpawnItem(List<DynamicSpawnPoint> spawns, uint limit, ItemType itemType)
        {
            Item item = Item.Create(itemType);
            uint spawned = 0;

            foreach (SpawnPoint spawnPoint in spawns)
            {
                if (UnityEngine.Random.Range(1, 101) >= spawnPoint.Chance || (limit > 0 && spawned >= limit))
                    continue;
                spawned++;
                item.Spawn(spawnPoint.Position);
            }
        }
    }
}