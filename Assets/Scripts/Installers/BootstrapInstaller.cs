using Core.CoroutineRunner;
using Core.CoroutineRunner.Impl;
using Core.SceneLoading;
using Core.SceneLoading.Impl;
using Core.StateMachine;
using Core.StateMachine.States.Impl;
using UnityEngine;
using Views;
using Zenject;

namespace Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private LoadingView loadingView;
        
        public override void InstallBindings()
        {
            Application.targetFrameRate = 60;
            
            var coroutineRunner = new GameObject("CoroutineRunner").AddComponent<CoroutineRunner>();
            DontDestroyOnLoad(coroutineRunner);
            
            var loadingViewObj = Instantiate(loadingView, Vector3.zero, Quaternion.identity);
            DontDestroyOnLoad(loadingViewObj);
            
            Container.Bind<ICoroutineRunner>().FromInstance(coroutineRunner).AsSingle();
            Container.Bind<LoadingView>().FromInstance(loadingViewObj).AsSingle();
            Container.Bind<ISceneLoadingService>().To<SceneLoadingService>().AsSingle();
            Container.Bind<IStateMachine>().To<GameStateMachine>().AsSingle();
            
            Container.Resolve<IStateMachine>().ChangeState<LoadingSceneState, string>("Menu");
        }
    }
}