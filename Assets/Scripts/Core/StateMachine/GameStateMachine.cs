using System;
using System.Collections.Generic;
using Core.SceneLoading;
using Core.StateMachine.States;
using Core.StateMachine.States.Impl;

namespace Core.StateMachine
{
    public class GameStateMachine : IStateMachine
    {
        private readonly ISceneLoadingService _sceneLoadingService;
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;
        
        public IExitableState CurrentState => _currentState;
        
        public GameStateMachine(ISceneLoadingService sceneLoadingService)
        {
            _sceneLoadingService = sceneLoadingService;
            _states = new Dictionary<Type, IExitableState>()
            {
                {typeof(LoadingSceneState), new LoadingSceneState(this ,sceneLoadingService)},
                {typeof(GameLoopState), new GameLoopState()}
            };
        }
        
        public void ChangeState<TState>() where TState : class, IState
        {
            _currentState?.Exit();
            var state = GetState<TState>();
            _currentState = state;
            state.Enter();
        }

        public void ChangeState<TState, TParam>(TParam param) where TState : class, IParametricState<TParam>
        {
            _currentState?.Exit();
            var state = GetState<TState>();
            _currentState = state;
            state.Enter(param);
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}