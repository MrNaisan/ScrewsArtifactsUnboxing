using Core.CoroutineRunner;
using Core.CoroutineRunner.Impl;
using Core.SceneLoading;
using Core.SceneLoading.Impl;
using Core.StateMachine;
using Core.StateMachine.States.Impl;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Application.targetFrameRate = 60;
            
            var coroutineRunner = new GameObject("CoroutineRunner").AddComponent<CoroutineRunner>();
            DontDestroyOnLoad(coroutineRunner);
            
            Container.Bind<ICoroutineRunner>().FromInstance(coroutineRunner).AsSingle();
            Container.Bind<ISceneLoadingService>().To<SceneLoadingService>().AsSingle();
            Container.Bind<IStateMachine>().To<GameStateMachine>().AsSingle();
            
            Container.Resolve<IStateMachine>().ChangeState<LoadingSceneState, string>("Game");
        }
    }
}