using FinalGame.Develop.CommonServices.AssetsManagement;
using FinalGame.Develop.CommonServices.CoroutinePerformer;
using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.Configs.Common.Wallet;
using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Ability.AbilityDrop;
using FinalGame.Develop.Gameplay.Features.Ability.View;
using FinalGame.Develop.Gameplay.Features.MainHero;
using FinalGame.Develop.Gameplay.UI;
using FinalGame.Resources.Configs.Gameplay.Abilities;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Ability.Presenters
{
    public class AbilityPresentersFactory
    {
        private DIContainer _container;

        public AbilityPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public SelectAbilityPresenter CreateSelectAbilityPresenter(
            AbilityConfig abilityConfig, SelectAbilityView view, Entity entity, int level)
        {
            return new SelectAbilityPresenter(abilityConfig, view, _container.Resolve<AbilityFactory>(), entity , level);
        }
        
        public SelectAbilityWithPricePresenter CreateSelectAbilityWithPricePresenter(
            AbilityConfig abilityConfig,SelectAbilityWithPriceView view)
        {
            return new SelectAbilityWithPricePresenter(_container.Resolve<ConfigsProviderService>().CurrencyIconsConfig, 
                abilityConfig, view, _container.Resolve<AbilityFactory>() , 
                _container.Resolve<MainHeroHolderService>(), 
                _container.Resolve<WalletService>());
        }

        public SelectAbilityListPresenter CreateSelectAbilityListPresenter(
            SelectAbilityListView view, Entity entity)
        {
            return new SelectAbilityListPresenter(view, entity, _container.Resolve<AbilityDropService>(), this);
        }

        public SelectAbilityPopupPresenter CreateSelectAbilityPopupPresenter(Entity entity)
        {
            SelectAbilityPopupView viewPrefab = _container.Resolve<ResourcesAssetLoader>()
                .LoadResource<SelectAbilityPopupView>("Gameplay/UI/Abilities/SelectionAbilityPopup");

            SelectAbilityPopupView view = Object.Instantiate(viewPrefab, _container.Resolve<GameplayUIRoot>().PopupsLayer);

            return new SelectAbilityPopupPresenter(view, entity, _container.Resolve<ICoroutinePerformer>(), this);
        }
    }
}