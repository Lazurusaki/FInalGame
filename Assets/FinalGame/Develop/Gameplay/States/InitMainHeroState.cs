using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.MainHero;
using FinalGame.Develop.Utils.Reactive;
using FinalGame.Develop.Utils.StateMachine;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.States
{
    public class InitMainHeroState : State, IUpdatableState
    {
        private MainHeroFactory _mainHeroFactory;
        private ConfigsProviderService _configsProviderService;

        public InitMainHeroState(MainHeroFactory mainHeroFactory, ConfigsProviderService configsProviderService)
        {
            _mainHeroFactory = mainHeroFactory;
            _configsProviderService = configsProviderService;
        }

        public ReactiveEvent MainHeroSetupComplete { get; private set; } = new();

        public override void Enter()
        {
            base.Enter();

            Entity mainHero = _mainHeroFactory.Create(Vector3.zero, _configsProviderService.MainHeroConfig);
            
            //Make character preparations here
            
            MainHeroSetupComplete?.Invoke();
        }

        public void Update(float deltaTime)
        {
        }
    }
}