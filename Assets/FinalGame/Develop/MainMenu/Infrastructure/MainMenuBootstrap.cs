using System.Collections;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.DI;
using UnityEngine;

namespace FinalGame.Develop.MainMenu.Infrastructure
{
    public class MainMenuBootstrap: MonoBehaviour
    {
        private DIContainer _container;
        
        public IEnumerable Run(DIContainer container, MainMenuSceneInputArgs mainMenuSceneInputArgs)
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
