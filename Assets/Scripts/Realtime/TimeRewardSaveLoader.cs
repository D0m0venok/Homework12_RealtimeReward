using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Zenject;

namespace Lessons.III.MetaGame.Lesson_RealtimeReward
{
    public class TimeRewardSaveLoader : IInitializable
    {
        private HashSet<IRealtimeTimer> _realtimeTimers = new();

        [Inject]
        public void Construct(IRealtimeTimer[] timers)
        {
            _realtimeTimers = new HashSet<IRealtimeTimer>(timers);
        }
        public void Initialize()
        {
            foreach (var realtimeTimer in _realtimeTimers)
            {
                Load(realtimeTimer);
            }
        }
        
        
        public void RegisterTimer(IRealtimeTimer realtimeTimer)
        {
            _realtimeTimers.Add(realtimeTimer);
            realtimeTimer.TimerStarted += OnTimerStarted;
        }
        public void UnregisterTimer(IRealtimeTimer realtimeTimer)
        {
            _realtimeTimers.Remove(realtimeTimer);
            realtimeTimer.TimerStarted -= OnTimerStarted;
        }

        private void OnTimerStarted(string id)
        {
            var now = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            PlayerPrefs.SetString(id, now);
            Debug.Log($"Save timer {id}");
        }
        private void Load(IRealtimeTimer timer)
        {
            var offlineTime = CalculateOfflineTime(timer.SaveKey);
            timer.SyncrhonizeTime(offlineTime);
            
            Debug.Log($"OFFLINE TIME {timer.SaveKey}: {offlineTime}");
        }
        private float CalculateOfflineTime(string saveKey)
        {
            var savedTime = PlayerPrefs.GetString(saveKey);

            if (string.IsNullOrEmpty(savedTime))
            {
                savedTime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                PlayerPrefs.SetString(saveKey, savedTime);
            }
            
            var time = DateTime.Parse(savedTime, CultureInfo.InvariantCulture);

            var now = DateTime.Now;
            var timeSpan = now - time;
            return (float)timeSpan.TotalSeconds;
        }
    }
}