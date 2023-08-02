namespace CodeBase.Infrastructure.Services.Progress
{
    public interface IPlayerProgressService : IService
    {
        public PlayerProgress Progress { get; set; }
    }
}