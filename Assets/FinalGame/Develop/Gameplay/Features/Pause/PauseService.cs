namespace FinalGame.Develop.Gameplay.Features.Pause
{
    public interface IPauseService
    {
        bool isPaused { get; }

        void Pause();

        void Unpause();
    }
}