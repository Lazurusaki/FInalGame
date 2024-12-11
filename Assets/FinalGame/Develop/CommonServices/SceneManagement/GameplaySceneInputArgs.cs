using FinalGame.Develop.Gameplay;

namespace FinalGame.Develop.CommonServices.SceneManagement
{
    public class GameplaySceneInputArgs : IInputSceneArgs
    {
        public readonly GameModes GameModeName;
        
        public GameplaySceneInputArgs(GameModes gameModeName) => GameModeName = gameModeName;
    }
}
