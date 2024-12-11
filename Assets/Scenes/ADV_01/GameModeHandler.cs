namespace FinalGame.Develop.Gameplay
{
    public class GameModeHandler
    {
        public IGameMode GameMode { get; }

        public GameModeHandler(IGameMode gameMode) => GameMode = gameMode;
    }
}
