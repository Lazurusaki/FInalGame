using System.Collections;
using FinalGame.Develop.CommonServices.DataManagement.DataProviders;
using FinalGame.Develop.CommonServices.LoadingScreen;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.ConfigsManagement;
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

            container.Resolve<ConfigsProviderService>().LoadAll();
            container.Resolve<PlayerDataProvider>().Load();
            
            yield return new WaitForSeconds(0.01f);
            
            loadingScreen.Hide();

            sceneSwitcher.ProcessSwitchSceneFor(new BootstrapSceneOutputArgs(new GameplaySceneInputArgs(1)));

            //конец инициализации игры
            //запуск игры
        }
    }
}
