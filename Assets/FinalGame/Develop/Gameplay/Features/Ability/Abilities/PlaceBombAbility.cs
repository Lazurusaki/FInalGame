using System;
using FinalGame.Develop.Configs.Gameplay.Abilities;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Aim;
using FinalGame.Develop.Gameplay.Features.Input;
using FinalGame.Develop.Utils.Reactive;
using UnityEditor;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Ability.Abilities
{
    public class PlaceBombAbility : Ability
    {
        private PlaceBombAbilityConfig _config;
        private readonly Entity _owner;
        private readonly EntityFactory _factory;
        private readonly IInputService _inputService;
        private readonly AimPresenterCreatorService _aimPresenter;

        private IDisposable _bombPlacedDisposable;

        public PlaceBombAbility(
            PlaceBombAbilityConfig config,
            Entity owner,
            EntityFactory entityFactory,
            int currentLevel,
            IInputService inputService,
            AimPresenterCreatorService aimPresenter) : base(config.ID, currentLevel, config.MaxLevel)
        {
            _factory = entityFactory;
            _config = config;
            _owner = owner;
            _inputService = inputService;
            _aimPresenter = aimPresenter;
        }

        public override void Activate()
        {
             _owner.AddBehavior(new PlaceBombAbilityBehavior(_factory, _inputService));
             _owner.GetIsBombPlaced().Subscribe(Deactivate);
             
             ShowAim();
        }

        private void Deactivate()
        {
            HideAim();
            _owner.TryRemoveBehavior<PlaceBombAbilityBehavior>();
        }

        private void ShowAim()
        {
            _inputService.IsEnabled = true;
            Cursor.visible = false;
            _aimPresenter.Enable();
        }

        private void HideAim()
        {
            _inputService.IsEnabled = false;
            Cursor.visible = true;
            _aimPresenter.Disable();
        }
    }
}