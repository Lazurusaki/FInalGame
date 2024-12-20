using System.Collections;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.AI;
using FinalGame.Develop.Gameplay.Entities;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Infrastructure
{
    public class GameplayBootstrap : MonoBehaviour
    {
        [SerializeField] private GameplayTest _gameplayTest;
        
        private DIContainer _container;
        
        public IEnumerator Run(DIContainer container, GameplaySceneInputArgs sceneInputArgs)
        {
            _container = container;

            ProcessRegistrations();
            
            _container.Initialize();
            
            yield return new WaitForSeconds(0.01f);
            
            _gameplayTest.StartProcess(container);
        }

        private void ProcessRegistrations()
        {
            RegisterEntityFactory();
            RegisterAIFactory();
        }

        private void RegisterEntityFactory()
        {
            _container.RegisterAsSingle(c => new EntityFactory(c));
        }
        
        private void RegisterAIFactory()
        {
            _container.RegisterAsSingle(c => new AIFactory(c));
        }
    }
}