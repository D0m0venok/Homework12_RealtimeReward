using System;
using Elementary;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Lessons.III.MetaGame.Lesson_RealtimeReward
{
    public class TimeReward : IRealtimeTimer, IGameStart 
    {
        public event Action<string> TimerStarted;

        public string SaveKey { get; private set; }

        private TimeRewardConfig _timeRewardConfig;
        private StorageProvider _storageProvider;

        [ShowInInspector]
        private Countdown _timer = new();
        
        //[Inject]
        public TimeReward(TimeRewardConfig timeRewardConfig, StorageProvider storageProvider)
        {
            _timeRewardConfig = timeRewardConfig;
            _storageProvider = storageProvider;
            SaveKey = timeRewardConfig.Id;
            _timer.Duration = _timeRewardConfig.Duration;
            _timer.RemainingTime = _timeRewardConfig.Duration;
            _timer.OnEnded += () => Debug.Log($"Timer {SaveKey} ended");
        }
        
        public void OnStart()
        {
            PlayTimer();
        }
        
        private void PlayTimer()
        {
            if (_timer.Progress == 0f)
            {
                Debug.Log($"Timer {SaveKey} started");
                TimerStarted?.Invoke(SaveKey);
            }
            _timer.Play();
        }
        
        public void SyncrhonizeTime(float time)
        {
            _timer.RemainingTime -= time;
        }
        
        [Button]
        private void ReceiveReward()
        {
            if (CanReceiveReward())
            {
                Debug.Log("You received reward");
                var reward = _timeRewardConfig.Rewards[Random.Range(0, _timeRewardConfig.Rewards.Length)];
                var storage = _storageProvider.GetStorage(reward.CurrencyType);
                storage.EarnValue(reward.RewardAmount);
                _timer.ResetTime();
                PlayTimer();
            }
            else
            {
                Debug.Log("You can't receive reward");
            }
        }

        private bool CanReceiveReward()
        {
            return _timer.Progress >= 1f;
        }
    }
}
