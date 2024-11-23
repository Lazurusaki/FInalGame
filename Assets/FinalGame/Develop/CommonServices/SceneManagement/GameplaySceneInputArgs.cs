using FinalGame.Develop.Gameplay;

namespace FinalGame.Develop.CommonServices.SceneManagement
{
    public class GameplaySceneInputArgs : IInputSceneArgs
    {
        public GameplaySceneInputArgs(GameModes gameMode) => GameMode = gameMode;

        public GameModes GameMode;
    }
}
