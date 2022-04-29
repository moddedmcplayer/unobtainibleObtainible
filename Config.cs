using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.API.Interfaces;
using Exiled.CustomItems.API;
using YamlDotNet.Serialization;

namespace unobtainibleObtainible
{
    public class Config : IConfig
    {
        [Description("Enable plugin?")]
        public bool IsEnabled { get; set; } = true;

        [Description("New weight for pink candy (O do disable)")]
        public float weight { get; set; } = 1f;

        [Description("Whether or not to boost pink candy explosion depending on the items present")]
        public bool enablePinkBoost { get; set; } = true;
        
        [Description("Whether or not to spawn X-3 Particle Disrupters")]
        public bool spawnX3 { get; set; } = true;
        
        [Description("Places the X-3 Particle Disrupters can spawn at")]
        public List<DynamicSpawnPoint> SpawnsX3 { get; set; } = new List<DynamicSpawnPoint>()
        {
            new DynamicSpawnPoint()
            {
                Chance = 10f,
                Location = SpawnLocation.InsideHid
            },
            new DynamicSpawnPoint()
            {
                Chance = 10f,
                Location = SpawnLocation.InsideSurfaceNuke
            },
            new DynamicSpawnPoint()
            {
                Chance = 10f,
                Location = SpawnLocation.InsideHczArmory
            }
        };

        [Description("The limit of X-3 Particle Disrupter that can spawn per round")]
        public int LimitX3 { get; set; } = 1;

        [Description("The amount of ammo a flashbang gives")]
        public int AmmoMult { get; set; } = 3;
    }
}