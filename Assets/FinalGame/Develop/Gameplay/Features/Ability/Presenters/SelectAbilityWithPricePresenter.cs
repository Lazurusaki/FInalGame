using System;
using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.Configs.Common.Wallet;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Ability.View;
using FinalGame.Develop.Gameplay.Features.MainHero;
using FinalGame.Resources.Configs.Gameplay.Abilities;
using UnityEngine;


namespace FinalGame.Develop.Gameplay.Features.Ability.Presenters
{
    public class SelectAbilityWithPricePresenter
    {
        private readonly Color AvailableColor = Color.white;
        private readonly Color UnavailableColor = Color.red;

        private CurrencyIconsConfig _currencyIconsConfig;
        
        public event Action<SelectAbilityWithPricePresenter> Selected;
        
        private AbilityFactory _abilityFactory;
        private MainHeroHolderService _mainHeroHolderService;
        private WalletService _walletService;

        private int _level;

        public SelectAbilityWithPricePresenter(
            CurrencyIconsConfig currencyIconsConfig,
            AbilityConfig abilityConfig, 
            SelectAbilityWithPriceView view,  
            AbilityFactory abilityFactory, 
            MainHeroHolderService mainHeroHolderService,
            WalletService walletService )
        {
            _currencyIconsConfig = currencyIconsConfig;
            _abilityFactory = abilityFactory;
            _mainHeroHolderService = mainHeroHolderService;
            AbilityConfig = abilityConfig;
            View = view;
            _walletService = walletService;
        }

        public AbilityConfig AbilityConfig { get; }
        public SelectAbilityWithPriceView View { get; }

        public void Enable()
        {
            View.SetIcon(AbilityConfig.Icon);
            View.SetCurrencyIcon(_currencyIconsConfig.GetSpriteFor(AbilityConfig.Cost.CurrencyType));
            View.SetCurrencyValue(AbilityConfig.Cost.Value);
            
            if (_walletService.HasEnough(AbilityConfig.Cost.CurrencyType,AbilityConfig.Cost.Value))
                View.Selected += OnViewClicked;

            else
            {
                View.SetTextColor(UnavailableColor);
            }
        }

        public void Disable()
        {
            View.Selected -= OnViewClicked;
        }

        public void Update()
        {
            Disable();
            Enable();
        }
        
        public void Provide()
        {
            Entity mainHero = _mainHeroHolderService.MainHero;

            if (mainHero is null)
                throw new NullReferenceException("Main hero is not found");

            Ability ability = _abilityFactory.CreateAbilityFor(mainHero, AbilityConfig, _level);
            
            _walletService.Spend(AbilityConfig.Cost.CurrencyType, AbilityConfig.Cost.Value);
            
            ability.Activate();
            
            Update();
        }
        
        public void EnableSubscription() => View.Subscribe();
        
        public void DisableSubscription() => View.Unsubscribe();
        
        private void OnViewClicked() => Selected?.Invoke(this);
    }
}