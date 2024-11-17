using FinalGame.Develop.DI;
using UnityEngine;

namespace FinalGame.Develop.EntryPoint
{
    //регистрация глобальных сервисов для старта игры
    public class EntryPoint: MonoBehaviour
    {
        [SerializeField] private Bootstrap _gameBootstrap;
        
        private void Awake()
        {
            SetupAppSettings();

            var projectContainer = new DIContainer();
            
            //регистрация глобальных сервисов
            //аналог Global Context в популярных DI фреймворках
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 144;
        }
    }
}
