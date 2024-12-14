using System.Collections;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.DI;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Infrastructure
{
    public class GameplayBootstrap : MonoBehaviour
    {
        private DIContainer _container;
        private GameplaySceneInputArgs _sceneInputArgs;
        
        public IEnumerator Run(DIContainer container, GameplaySceneInputArgs sceneInputArgs)
        {
            _container = container;
            _sceneInputArgs = sceneInputArgs;
            
            _container.Initialize();
            
            yield return new WaitForSeconds(0.1f);
            
        }
    }
}