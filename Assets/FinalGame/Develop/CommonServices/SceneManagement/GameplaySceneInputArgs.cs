using FinalGame.Develop.Gameplay;

namespace FinalGame.Develop.CommonServices.SceneManagement
{
    public class GameplaySceneInputArgs : IInputSceneArgs
    {
        private int _levelNumber;
        
        public GameplaySceneInputArgs(int levelNumber)
        {
            _levelNumber = levelNumber;
        }
    }
}
