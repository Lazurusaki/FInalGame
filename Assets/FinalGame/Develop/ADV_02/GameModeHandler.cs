namespace FinalGame.Develop.ADV_02
{
    public class GameModeHandler
    {
        public ISequenceGameMode GameMode { get; }

        public GameModeHandler(ISequenceGameMode gameMode) => GameMode = gameMode;
    }
}