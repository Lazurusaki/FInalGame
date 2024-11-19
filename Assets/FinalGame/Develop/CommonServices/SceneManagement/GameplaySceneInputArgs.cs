namespace FinalGame.Develop.CommonServices.SceneManagement
{
    public class GameplaySceneInputArgs : IInputSceneArgs
    {
        public GameplaySceneInputArgs(int levelIndex) => LevelIndex = levelIndex;
        
        public int LevelIndex { get; }
    }
}
