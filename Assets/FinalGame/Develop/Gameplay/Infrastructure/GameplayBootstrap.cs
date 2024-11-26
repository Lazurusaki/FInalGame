using System.Collections;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.DI;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Infrastructure
{
    public class GameplayBootstrap : MonoBehaviour
    {
        private DIContainer _container;
        private IGameMode _gameMode;
        
        public IEnumerator Run(DIContainer container, GameplaySceneInputArgs sceneInputArgs)
        {
            _container = container;
            
            RegisterGameModeFabric();
            RegisterConditionFabric();
            
            _gameMode = _container.Resolve<GameModeFabric>().CreateGameMode(sceneInputArgs.GameModeName);
            
            RegisterGameModeHandler();
            
            ICondition winCondition = _container.Resolve<ConditionFabric>().CreateCondition(EndGameConditions.ValuesGuessed);
            ICondition looseCondition = _container.Resolve<ConditionFabric>().CreateCondition(EndGameConditions.Mistake);
            
            Game game = new(container, winCondition, looseCondition);

            yield return new WaitForSeconds(1);

            game.Start();
        }
        
        private void RegisterGameModeHandler()
        {
            _container.RegisterAsSingle(c => new GameModeHandler(_gameMode));
        }

        private void RegisterGameModeFabric()
        {
            _container.RegisterAsSingle(c => new GameModeFabric());
        }
        
        private void RegisterConditionFabric()
        {
            _container.RegisterAsSingle(c => new ConditionFabric(_container));
        }
        
    }
}