using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.MainHero;
using FinalGame.Develop.Gameplay.UI;

namespace FinalGame.Develop.Gameplay.Features.Damage.Presenters
{
    public class MainHeroDamageBlinkPresenterCreatorService : IDisposable
    {
        private readonly GameplayUIPresentersFactory _gameplayUIPresentersFactory;
        private  DamageBlinkPresenter _damageBlinkPresenter;
        
        private readonly IDisposable _heroRegisteredDisposable;
        private readonly IDisposable _heroUnregisteredDisposable;
        
        public MainHeroDamageBlinkPresenterCreatorService(MainHeroHolderService mainHeroHolderService, GameplayUIPresentersFactory gameplayUIPresentersFactory)
        {
            _gameplayUIPresentersFactory = gameplayUIPresentersFactory;
            _heroRegisteredDisposable = mainHeroHolderService.HeroRegistered.Subscribe(OnMainHeroRegistered);
            _heroUnregisteredDisposable = mainHeroHolderService.HeroUnregistered.Subscribe(OnMainHeroUnregistered);
        }

        private void OnMainHeroRegistered(Entity hero)
        {
            _damageBlinkPresenter = _gameplayUIPresentersFactory.CreateDamageBlinkPresenter(hero);
            _damageBlinkPresenter.Enable();
        }
        
        private void OnMainHeroUnregistered(Entity hero)
        {
            _damageBlinkPresenter.Disable();
            _damageBlinkPresenter = null;
        }
        
        public void Dispose()
        {
            _heroRegisteredDisposable?.Dispose();
            _heroUnregisteredDisposable?.Dispose();
        }
    }
}