using FinalGame.Develop.CommonServices.AssetsManagement;
using FinalGame.Develop.CommonServices.CoroutinePerformer;
using FinalGame.Develop.CommonUI;
using FinalGame.Develop.CommonUI.Wallet;
using FinalGame.Develop.Configs.Gameplay.Levels;
using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Ability.Presenters;
using FinalGame.Develop.Gameplay.Features.Ability.View;
using FinalGame.Develop.Gameplay.Features.Aim;
using FinalGame.Develop.Gameplay.Features.Damage.Presenters;
using FinalGame.Develop.Gameplay.Features.GameModes.Wave.Presenters;
using FinalGame.Develop.Gameplay.Features.GameModes.Wave.View;
using FinalGame.Develop.Gameplay.Features.Health;
using FinalGame.Develop.Gameplay.Features.MainHero;
using FinalGame.Develop.Gameplay.States.Presenters;
using FinalGame.Develop.Gameplay.States.View;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.UI
{
    public class GameplayUIPresentersFactory
    {
        private const string HealthBarViewPrefabPath = "Gameplay/UI/Common/MainHeroHealthBar";
        private const string AimViewPrefabPath = "Gameplay/UI/Common/Aim";
        private const string DamageBlinkPrefab = "Gameplay/UI/Common/DangerBlink";
        private const string VictoryPrefab = "Gameplay/UI/Common/VictoryScreen";
        private const string DefeatPrefab = "Gameplay/UI/Common/DefeatScreen";
        private const string WavePrepareViewPrefab = "Gameplay/UI/Wave/WawePreparePopupView"; 

        private readonly Vector3 MainHeroHealthBarPosition = new Vector3(0, 380, 0);
        
        private readonly DIContainer _container;

        public GameplayUIPresentersFactory(DIContainer container)
        {
            _container = container;
        }
        
        public WavePreparePresenter CreateWavePreparePresenter()
        {
            var viewPrefab = _container.Resolve<ResourcesAssetLoader>()
                .LoadResource<WavePrepareView>(WavePrepareViewPrefab);

            var view = Object.Instantiate(viewPrefab, _container.Resolve<GameplayUIRoot>().PopupsLayer );

            //create wallet presenter
            var walletPresenter = _container.Resolve<WalletPresenterFactory>().CreateWalletPresenter(view.WalletView);
            var selectAbilityWithPriceListPresenter = CreateSelectAbilityWithPriceListPresenter(view.AbilitiesListView);
            
            
            return new WavePreparePresenter(view, walletPresenter, selectAbilityWithPriceListPresenter , _container.Resolve<ICoroutinePerformer>());
        }

        public SelectAbilityWithPriceListPresenter CreateSelectAbilityWithPriceListPresenter(SelectAbilityWithPriceListView abilityListView)
            => new SelectAbilityWithPriceListPresenter(abilityListView, 
                _container.Resolve<MainHeroHolderService>().MainHero, 
                _container.Resolve<AbilityPresentersFactory>(),
                _container.Resolve<ConfigsProviderService>().AbilitiesConfigContainer);

        
        public HealthBarPresenter CreateHealthBarPresenter(Entity entity)
        {
            GradientBarWithText viewPrefab = _container.Resolve<ResourcesAssetLoader>()
                .LoadResource<GradientBarWithText>(HealthBarViewPrefabPath);

            GradientBarWithText view = Object.Instantiate(viewPrefab, _container.Resolve<GameplayUIRoot>().HUDLayer);
            view.transform.localPosition = MainHeroHealthBarPosition;

            return new HealthBarPresenter(entity,view);
        }
        
        public AimPresenter CreateAimPresenter(Entity entity)
        {
            AimView viewPrefab = _container.Resolve<ResourcesAssetLoader>()
                .LoadResource<AimView>(AimViewPrefabPath);

            AimView view = Object.Instantiate(viewPrefab);
            
            return new AimPresenter(entity,view);
        }

        public DamageBlinkPresenter CreateDamageBlinkPresenter(Entity entity)
        {
            CanvasBlink viewPrefab = _container.Resolve<ResourcesAssetLoader>()
                .LoadResource<CanvasBlink>(DamageBlinkPrefab);

            CanvasBlink view = Object.Instantiate(viewPrefab, _container.Resolve<GameplayUIRoot>().HUDLayer );
            
            return new DamageBlinkPresenter(entity,view);
        }
        
        public WinResultPresenter CreateVictoryResultPresenter(LevelConfig levelConfig)
        {
            WinResultView viewPrefab = _container.Resolve<ResourcesAssetLoader>()
                .LoadResource<WinResultView>(VictoryPrefab);

            WinResultView view = Object.Instantiate(viewPrefab, _container.Resolve<GameplayUIRoot>().HUDLayer );
            
            return new WinResultPresenter(view.Main, view.RewardView, levelConfig , _container.Resolve<ICoroutinePerformer>());
        }
        
        public ResultPresenter CreateDefeatResultPresenter()
        {
            ResultView viewPrefab = _container.Resolve<ResourcesAssetLoader>()
                .LoadResource<ResultView>(DefeatPrefab);

            ResultView view = Object.Instantiate(viewPrefab, _container.Resolve<GameplayUIRoot>().HUDLayer );
            
            return new ResultPresenter(view,_container.Resolve<ICoroutinePerformer>());
        }
    }
}