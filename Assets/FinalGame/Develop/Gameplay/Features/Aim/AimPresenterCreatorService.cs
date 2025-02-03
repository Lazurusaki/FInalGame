using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.MainHero;
using FinalGame.Develop.Gameplay.UI;

namespace FinalGame.Develop.Gameplay.Features.Aim
{
    public class AimPresenterCreatorService : IDisposable
    {
        private readonly GameplayUIPresentersFactory _gameplayUIPresentersFactory;
        private AimPresenter _aimPresenter;
        
        private readonly IDisposable _heroRegisteredDisposable;
        private readonly IDisposable _heroUnregisteredDisposable;

        private IDisposable _heroAttackDisposable;
        
        public AimPresenterCreatorService(MainHeroHolderService mainHeroHolderService, GameplayUIPresentersFactory gameplayUIPresentersFactory)
        {
            _gameplayUIPresentersFactory = gameplayUIPresentersFactory;
            _heroRegisteredDisposable = mainHeroHolderService.HeroRegistered.Subscribe(OnMainHeroRegistered);
            _heroUnregisteredDisposable = mainHeroHolderService.HeroUnregistered.Subscribe(OnMainHeroUnregistered);
        }

        private void OnMainHeroRegistered(Entity hero)
        {
            _aimPresenter = _gameplayUIPresentersFactory.CreateAimPresenter(hero);
            _heroAttackDisposable = hero.GetInstantAttackTrigger().Subscribe(OnAttack);
        }

        private void OnAttack() => _aimPresenter.Attack();
        
        private void OnMainHeroUnregistered(Entity hero)
        {
            _aimPresenter = null;
            Disable();
        }

        public void Enable() => _aimPresenter.Enable(); 
        
        public void Disable() => _aimPresenter.Disable();
        
        public void Dispose()
        {
            _heroRegisteredDisposable?.Dispose();
            _heroUnregisteredDisposable?.Dispose();
            _heroAttackDisposable?.Dispose();
        }

        public void Update() => _aimPresenter?.Update();
    }
}