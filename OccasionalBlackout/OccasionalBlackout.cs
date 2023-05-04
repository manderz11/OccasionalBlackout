using System;
using Exiled.API.Features;

namespace OccasionalBlackout
{
    public class BlackoutPlugin : Plugin<Config>
    {
        public override string Name => "Occasional Blackout";
        public override string Author => "manderz11";
        public override Version Version => new Version(1, 0, 3);
        private static readonly BlackoutPlugin Singleton = new();
        private RoundControlHandle roundControlHandle;
        public bool ShouldHandleLightsOut = false;
        private BlackoutPlugin(){}

        public static BlackoutPlugin Instance => Singleton;
        
        public override void OnEnabled()
        {
            roundControlHandle = new RoundControlHandle();
            Exiled.Events.Handlers.Server.RoundEnded += roundControlHandle.onRoundEnd;
            Exiled.Events.Handlers.Server.RoundStarted += roundControlHandle.onRoundStart;
            Exiled.Events.Handlers.Server.RestartingRound += roundControlHandle.onRoundRestart;
            ShouldHandleLightsOut = false;
            base.OnEnabled();
        }

        public override void OnReloaded()
        {
            ShouldHandleLightsOut = false;
            roundControlHandle.stopCoroutine();
            base.OnReloaded();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.RoundEnded -= roundControlHandle.onRoundEnd;
            Exiled.Events.Handlers.Server.RoundStarted -= roundControlHandle.onRoundStart;
            Exiled.Events.Handlers.Server.RestartingRound -= roundControlHandle.onRoundRestart;
            roundControlHandle.stopCoroutine();
            ShouldHandleLightsOut = false;
            roundControlHandle = null;
            base.OnDisabled();
        }
    }
}