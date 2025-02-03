using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.MainHero;
using FinalGame.Develop.Gameplay.UI;

namespace FinalGame.Develop.Gameplay.Features.Health
{
    public class MainHeroHealthBarCreatorService : IDisposable
    {
        private readonly GameplayUIPresentersFactory _gameplayUIPresentersFactory;
        private  HealthBarPresenter _healthBarPresenter;
        
        private readonly IDisposable _heroRegisteredDisposable;
        private readonly IDisposable _heroUnregisteredDisposable;
        
        public MainHeroHealthBarCreatorService(MainHeroHolderService mainHeroHolderService, GameplayUIPresentersFactory gameplayUIPresentersFactory)
        {
            _gameplayUIPresentersFactory = gameplayUIPresentersFactory;
            _heroRegisteredDisposable = mainHeroHolderService.HeroRegistered.Subscribe(OnMainHeroRegistered);
            _heroUnregisteredDisposable = mainHeroHolderService.HeroUnregistered.Subscribe(OnMainHeroUnregistered);
        }

        private void OnMainHeroRegistered(Entity hero)
        {
            _healthBarPresenter = _gameplayUIPresentersFactory.CreateHealthBarPresenter(hero);
            _healthBarPresenter.Enable();
        }
        
        private void OnMainHeroUnregistered(Entity hero)
        {
            _healthBarPresenter.Disable();
            _healthBarPresenter = null;
        }
        
        public void Dispose()
        {
            _heroRegisteredDisposable?.Dispose();
            _heroUnregisteredDisposable?.Dispose();
        }
    }
}