using Lessons.III.MetaGame.Lesson_RealtimeReward;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private TimeRewardModule _timeRewardModule;
        [SerializeField] private TimeRewardConfig[] _timeRewardConfigs;
        
        public override void InstallBindings()
        {
            Container.Bind<StorageProvider>().AsSingle();

            foreach (var config in _timeRewardConfigs)
            {
                Container.BindInterfacesTo<TimeReward>().FromNew().
                    AsCached().WithArguments(config);
            }
            Container.Bind<TimeRewardModule>().FromInstance(_timeRewardModule).AsSingle();
            
            Container.BindInterfacesAndSelfTo<TimeRewardSaveLoader>().AsSingle();
        }

        [Button]
        public void OnStart()
        {
            var starts = Container.Resolve<IGameStart[]>();

            foreach (var gameStart in starts)
            {
                gameStart.OnStart();
            }
        }

    }
}