using FinalGame.Develop.Configs.Gameplay.Levels;
using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.MainHero;
using FinalGame.Develop.Gameplay.UI;
using FinalGame.Develop.Utils.Reactive;
using FinalGame.Develop.Utils.StateMachine;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.States
{
    public class InitMainHeroState : State, IUpdatableState
    {
        private readonly MainHeroFactory _mainHeroFactory;
        private readonly ConfigsProviderService _configsProviderService;
        private readonly GameplayUIPresentersFactory _gameplayUIPresentersFactory;
        private readonly LevelConfig _levelConfig;

        public InitMainHeroState(LevelConfig levelConfig,MainHeroFactory mainHeroFactory, ConfigsProviderService configsProviderService, GameplayUIPresentersFactory gameplayUIPresentersFactory)
        {
            _levelConfig = levelConfig;
            _mainHeroFactory = mainHeroFactory;
            _configsProviderService = configsProviderService;
            _gameplayUIPresentersFactory = gameplayUIPresentersFactory;
        }

        public ReactiveEvent MainHeroSetupComplete { get; private set; } = new();

        public override void Enter()
        {
            base.Enter();

            Entity mainHero = _mainHeroFactory.CreateCastle(Vector3.zero, _configsProviderService.CastleConfig, _levelConfig);

            //var damageBlink = _gameplayUIFactory.CreateDamageBlinkPresenter(mainHero);
            
            //damageBlink.Enable();
            
            //Make character preparations here
            
            MainHeroSetupComplete?.Invoke();
        }

        public void Update(float deltaTime)
        {
        }
    }
}