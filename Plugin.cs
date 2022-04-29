using System;
using Exiled.API.Features;
using Exiled.Events.Handlers;
using HarmonyLib;
using Player = Exiled.Events.Handlers.Player;
using Server = Exiled.Events.Handlers.Server;

namespace unobtainibleObtainible
{
    public class Plugin : Plugin<Config>
    {
        public override string Author { get; } = "moddedmcplayer";
        public override string Name { get; } = "unobtainibleObtainible";
        public override Version Version { get; } = new Version(1, 1, 0);
        public override Version RequiredExiledVersion { get; } = new Version(5, 1, 0);

        public Harmony harmony;
        public EventHandlers EventHandler;
        public static API apiInstance;
        public static Plugin Instance { get; private set; }

        public float RAChance { get; set; } = -1f;
        
        public override void OnEnabled()
        {
            Instance = this;
            EventHandler = new EventHandlers(this);
            apiInstance = new API();
            harmony = new Harmony($"unobtainibleObtainible.{DateTime.UtcNow.Ticks}");
            harmony.PatchAll();
            
            Scp330.EatingScp330 += EventHandler.OnEatingCandy;
            Server.RoundStarted += EventHandler.OnStartingRound;
            Server.WaitingForPlayers += EventHandler.OnWaitingForPlayers;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Scp330.EatingScp330 -= EventHandler.OnEatingCandy;
            Server.WaitingForPlayers -= EventHandler.OnWaitingForPlayers;
            
            harmony?.UnpatchAll(harmony.Id);
            harmony = null;
            EventHandler = null;
            Instance = null;
            apiInstance = null;
            
            base.OnDisabled();
        }
    }
}