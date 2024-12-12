namespace FinalGame.Develop.Gameplay
{
    public class GameModeConsoleHandler
    {
        public IGameModeConsole GameModeConsole { get; }

        public GameModeConsoleHandler(IGameModeConsole gameModeConsole) => GameModeConsole = gameModeConsole;
    }
}
