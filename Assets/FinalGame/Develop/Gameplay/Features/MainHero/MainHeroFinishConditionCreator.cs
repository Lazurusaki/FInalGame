using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.States;
using FinalGame.Develop.Utils.Conditions;

namespace FinalGame.Develop.Gameplay.Features.MainHero
{
    public class MainHeroFinishConditionCreator : IDisposable
    {
        private readonly MainHeroHolderService _mainHeroHolderService;
        private readonly GameplayFinishConditionService _gameplayFinishConditionService;

        private readonly IDisposable _heroRegisteredDisposableEvent;
        private readonly IDisposable _heroUnregisteredDisposableEvent;

        private ICondition _defeatCondition;
        
        public MainHeroFinishConditionCreator(MainHeroHolderService mainHeroHolderService, GameplayFinishConditionService gameplayFinishConditionService)
        {
            _mainHeroHolderService = mainHeroHolderService;
            _gameplayFinishConditionService = gameplayFinishConditionService;

            _heroRegisteredDisposableEvent = _mainHeroHolderService.HeroRegistered.Subscribe(OnMainHeroRegistered);
            _heroUnregisteredDisposableEvent = _mainHeroHolderService.HeroUnregistered.Subscribe(OnMainHeroUnregistered);
        }

        private void OnMainHeroRegistered(Entity hero)
        {
            _defeatCondition = new FuncCondition(() => hero.GetIsDead().Value);
            _gameplayFinishConditionService.LooseCondition.Add(_defeatCondition);
        }
        
        private void OnMainHeroUnregistered(Entity obj)
        {
            _gameplayFinishConditionService.LooseCondition.Remove(_defeatCondition);
            _defeatCondition = null;
        }


        public void Dispose()
        {
            _heroRegisteredDisposableEvent.Dispose();
            _heroUnregisteredDisposableEvent.Dispose();
        }
    }
}