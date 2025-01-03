using Core.StateMachine.States;

namespace Core.StateMachine
{
    public interface IStateMachine
    {
        public void ChangeState<TState>() where TState : class, IState;
        public void ChangeState<TState, TParam>(TParam param) where TState : class, IParametricState<TParam>;
    }
}