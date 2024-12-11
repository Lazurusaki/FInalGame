using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay;

namespace FinalGame.Develop.MainMenu
{
    public class StartGame : IMenuCommand
    {
        private DIContainer _container;
        private readonly GameModes _gameModeName;

        public StartGame(GameModes gameModeName) => _gameModeName = gameModeName;

        public void Initialize(DIContainer container) => _container = container;
        
        public void Execute()
        {
            _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new MainMenuSceneOutputArgs(
                new GameplaySceneInputArgs(_gameModeName)));
        }
    }
}
