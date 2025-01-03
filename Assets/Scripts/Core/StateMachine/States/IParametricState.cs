namespace Core.StateMachine.States
{
    public interface IParametricState<TParam> : IExitableState
    {
        public void Enter(TParam param);
    }
}