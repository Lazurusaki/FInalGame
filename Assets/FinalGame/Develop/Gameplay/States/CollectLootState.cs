using System;
using FinalGame.Develop.Gameplay.Features.Loot;
using FinalGame.Develop.Gameplay.Features.MainHero;
using FinalGame.Develop.Utils.Reactive;
using FinalGame.Develop.Utils.StateMachine;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.States
{
    public class CollectLootState : State, IUpdatableState
    {
        private ReactiveEvent _lootCollected = new();

        private LootPickupService _lootPickupService;
        private MainHeroHolderService _mainHeroHolderService;

        private IDisposable _pickupDisposable;

        public CollectLootState(LootPickupService lootPickupService, MainHeroHolderService mainHeroHolderService)
        {
            _lootPickupService = lootPickupService;
            _mainHeroHolderService = mainHeroHolderService;
        }
        
        public IReadonlyEvent LootCollected => _lootCollected;
        
        public override void Enter()
        {
            base.Enter();

            _pickupDisposable = _lootPickupService.AllCollected.Subscribe(OnLootCollected);
            _lootPickupService.PickupTo(_mainHeroHolderService.MainHero);
        }

        private void OnLootCollected()
        {
            _lootCollected?.Invoke();
        }

        public override void Exit()
        {
            base.Exit();
            
            _pickupDisposable?.Dispose();
            _lootPickupService.Reset();
        }

        public void Update(float deltaTime)
        {
        }
    }
}