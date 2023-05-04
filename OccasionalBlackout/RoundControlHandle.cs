using System;
using System.Collections.Generic;
using Exiled.Events.EventArgs.Server;
using MEC;
using PluginAPI.Core;
using Map = Exiled.API.Features.Map;

namespace OccasionalBlackout;

public class RoundControlHandle
{
    private CoroutineHandle mainCoroutine;
    public bool ShouldAnnounceMessage = OccasionalBlackout.BlackoutPlugin.Instance.Config.ShouldAnnounceMessage;
    public string AnnounceMessage = OccasionalBlackout.BlackoutPlugin.Instance.Config.AnnounceMessage;
    public int DelayMessageAndEvent = OccasionalBlackout.BlackoutPlugin.Instance.Config.DelayMessageAndEvent;
    public int MinDelayTime = OccasionalBlackout.BlackoutPlugin.Instance.Config.MinDelayTime;
    public int MaxDelayTime = OccasionalBlackout.BlackoutPlugin.Instance.Config.MaxDelayTime;
    public int MinTime = OccasionalBlackout.BlackoutPlugin.Instance.Config.MinTime;
    public int MaxTime = OccasionalBlackout.BlackoutPlugin.Instance.Config.MaxTime;
    public bool Debug = OccasionalBlackout.BlackoutPlugin.Instance.Config.Debug;
    public int Chance = OccasionalBlackout.BlackoutPlugin.Instance.Config.Chance;
    public bool BlackoutRoundAnnounce = OccasionalBlackout.BlackoutPlugin.Instance.Config.BlackoutRoundAnnounce;
    public string BlackoutRoundAnnouncementMessage = OccasionalBlackout.BlackoutPlugin.Instance.Config.BlackoutRoundAnnouncementMessage;
    public bool ShouldAnnounceBackMessage = OccasionalBlackout.BlackoutPlugin.Instance.Config.ShouldAnnounceBackMessage;
    public string AnnounceBackMessage = OccasionalBlackout.BlackoutPlugin.Instance.Config.AnnounceBackMessage;
    public void onRoundEnd(RoundEndedEventArgs roundEndedEventArgs)
    {
        if (mainCoroutine != null)
        {
            if (Debug)
            {
                Log.Debug("Attempting to kill coroutine!");
            }

            if (mainCoroutine.IsRunning)
            {
                try
                {
                    Timing.KillCoroutines(mainCoroutine);
                }
                catch (Exception e)
                {
                    Log.Error(e.ToString());
                }
            }
        }
    }
    
    public void onRoundRestart()
    {
        if (mainCoroutine != null)
        {
            if (Debug)
            {
                Log.Debug("Attempting to kill coroutine!");
            }

            if (mainCoroutine.IsRunning)
            {
                try
                {
                    Timing.KillCoroutines(mainCoroutine);
                }
                catch (Exception e)
                {
                    Log.Error(e.ToString());
                }
            }
        }
    }

    public void stopCoroutine()
    {
        if (mainCoroutine != null)
        {
            if (Debug)
            {
                Log.Debug("Attempting to kill coroutine!");
            }
            if (mainCoroutine.IsRunning)
            {
                try
                {
                    Timing.KillCoroutines(mainCoroutine);
                }
                catch (Exception e)
                {
                    Log.Error(e.ToString());
                }
            }
        }
    }
    
    public void onRoundStart()
    {
        if (Debug)
        {
            Log.Debug("Attempting to run coroutine!");
        }

        Random random = new Random();
        if (random.Next(0,100) < Chance)
        {
            OccasionalBlackout.BlackoutPlugin.Instance.ShouldHandleLightsOut = true;
            mainCoroutine = Timing.RunCoroutine(Coroutine());
            if (BlackoutRoundAnnounce)
            {
                Timing.CallDelayed(10f, () =>
                {
                    Cassie.Message(BlackoutRoundAnnouncementMessage,false, true, true);
                });
            }
        }
        else
        {
            if (Debug)
            {
                Log.Debug("Unlucky! There will be no blackouts this round!");
            }
        }
        
    }

    public IEnumerator<float> Coroutine()
    {
        while (OccasionalBlackout.BlackoutPlugin.Instance.ShouldHandleLightsOut)
        {
            if (Debug)
            {
                Log.Debug("Start of coroutine");
            }
            Random random = new Random();
            if (!OccasionalBlackout.BlackoutPlugin.Instance.ShouldHandleLightsOut)
            {
                yield break;
            }
            int wait = random.Next(MinDelayTime, MaxDelayTime);
            if (Debug)
            {
                Log.Debug($"Waiting for {wait}s");
            }
            yield return Timing.WaitForSeconds(wait);
            if (!OccasionalBlackout.BlackoutPlugin.Instance.ShouldHandleLightsOut)
            {
                yield break;
            }
            if (ShouldAnnounceMessage)
            {
                if (Debug)
                {
                    Log.Debug("Announcing cassie");
                }
                Cassie.Message(AnnounceMessage, false, true, true);
            }
            yield return Timing.WaitForSeconds(DelayMessageAndEvent);
            int duration = random.Next(MinTime, MaxTime);
            if (Debug)
            {
                Log.Debug($"Turning off all lights for {duration}s");
            }
            Map.TurnOffAllLights(duration);
            yield return Timing.WaitForSeconds(duration);
            if (ShouldAnnounceBackMessage)
            {
                Cassie.Message(AnnounceBackMessage,false,true,true);
            }
        }
    }
}