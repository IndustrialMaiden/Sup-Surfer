namespace CodeBase.Infrastructure.States
{
    public interface IState : IExitableState
    {
        /// <summary>
        /// Запускается при входе в стейт
        /// </summary>
        public void Enter();
    }

    public interface IPayloadedState<TPayload> : IExitableState
    {
        /// <summary>
        /// Запускается при входе в стейт
        /// </summary>
        /// <param name="payload">Параметр</param>
        public void Enter(TPayload payload);
    }
    
    /// <summary>
    /// Запускается при выходе из стейта
    /// </summary>
    public interface IExitableState
    {
        public void Exit();
    }
}