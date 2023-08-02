namespace CodeBase.Infrastructure.Services.Progress
{
    public class PlayerProgressService : IPlayerProgressService
    {
        public PlayerProgress Progress { get; set; }

        public PlayerProgressService()
        {
            Progress = new PlayerProgress();
        }
    }
}