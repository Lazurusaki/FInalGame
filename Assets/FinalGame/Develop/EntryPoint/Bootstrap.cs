using System.Collections;
using FinalGame.Develop.CommonServices.LoadingScreen;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.DI;
using UnityEngine;

namespace FinalGame.Develop.EntryPoint
{
    //инициализация начала работы игры
    public class Bootstrap : MonoBehaviour
    {
        public IEnumerator Run(DIContainer container)
        {
            //начало инициализации игры

            var loadingScreen = container.Resolve<ILoadingScreen>();
            var sceneSwitcher = container.Resolve<SceneSwitcher>();
            
            loadingScreen.Show();
            
            yield return new WaitForSeconds(1);
            
            loadingScreen.Hide();

            sceneSwitcher.ProcessSwitchSceneFor(new BootstrapSceneOutputArgs(new MainMenuSceneInputArgs()));

            //конец инициализации игры
            //запуск игры
        }
    }
}
