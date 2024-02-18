using UnityEngine;

namespace Lessons.III.MetaGame.Lesson_RealtimeReward
{
    [CreateAssetMenu(menuName = "Lesson/Realtime/MoneyRewardConfig", fileName = "TimeRewardConfig")]
    public class TimeRewardConfig : ScriptableObject
    {
        public string Id;
        public float Duration = 5f;
        public RewardData[] Rewards;
    }
}