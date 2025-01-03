using Core.SceneLoading;

namespace Core.StateMachine.States.Impl
{
    public class LoadingSceneState : IParametricState<string>
    {
        private readonly ISceneLoadingService _sceneLoadingService;
        private readonly IStateMachine _stateMachine;
        public LoadingSceneState(
            IStateMachine stateMachine,
            ISceneLoadingService sceneLoadingService
        )
        {
            _sceneLoadingService = sceneLoadingService;
            _stateMachine = stateMachine;
        }
        
        public void Enter(string sceneName)
        {
            _sceneLoadingService.LoadSceneAsync(sceneName);
        }

        public void Exit()
        {
        }
    }
}