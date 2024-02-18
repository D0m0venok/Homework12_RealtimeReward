using System;

namespace Lessons.III.MetaGame.Lesson_RealtimeReward
{
    public interface IRealtimeTimer
    {
        event Action<string> TimerStarted;
        void SyncrhonizeTime(float time);
        string SaveKey { get; }
    }
}