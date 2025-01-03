namespace Core.StateMachine.States
{
    public interface IState : IExitableState
    {
        public void Enter();
    }
}