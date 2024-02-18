using System;

namespace Lessons.III.MetaGame.Lesson_RealtimeReward
{
    [Serializable]
    public sealed class RewardData
    {
        public CurrencyType CurrencyType;
        public int RewardAmount;
    }
}