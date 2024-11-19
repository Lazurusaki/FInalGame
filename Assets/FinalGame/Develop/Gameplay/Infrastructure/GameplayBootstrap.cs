using System.Collections;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.DI;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Infrastructure
{
    public class GameplayBootstrap : MonoBehaviour
    {
        private DIContainer _container;
        
        public IEnumerator Run(DIContainer container, GameplaySceneInputArgs sceneInputArgs)
        {
            _container = container;

            ProcessRegistrations();

            yield return new WaitForSeconds(1);
        }

        private void ProcessRegistrations()
        {
            
        }
    }
}
