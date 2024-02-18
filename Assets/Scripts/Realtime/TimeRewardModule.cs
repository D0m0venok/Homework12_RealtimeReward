using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Lessons.III.MetaGame.Lesson_RealtimeReward
{
    public class TimeRewardModule : MonoBehaviour
    {
        [ShowInInspector]
        public IRealtimeTimer[] _timeRewards;
        
        [Inject]
        public void Construct(IRealtimeTimer[] timeReward, TimeRewardSaveLoader timeRewardSaveLoader)
        {
            print("Construct TimeRewardModule");
            _timeRewards = timeReward;
            foreach (var reward in _timeRewards)
            {
                timeRewardSaveLoader.RegisterTimer(reward);
            }
        }
    }
}