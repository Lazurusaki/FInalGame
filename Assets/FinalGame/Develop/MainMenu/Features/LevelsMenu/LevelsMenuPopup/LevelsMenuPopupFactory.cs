using FinalGame.Develop.CommonServices.AssetsManagement;
using FinalGame.Develop.CommonServices.LevelsService;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.DI;
using FinalGame.Develop.MainMenu.UI;
using UnityEngine;

namespace FinalGame.Develop.MainMenu.Features.LevelsMenu.LevelsMenuPopup
{
    public class LevelsMenuPopupFactory
    {
        private readonly DIContainer _container;
        private readonly ResourcesAssetLoader _resourcesAssetLoader;
        private readonly MainMenuUIRoot _mainMenuUiRoot;

        public LevelsMenuPopupFactory(DIContainer container)
        {
            _container = container;
            _resourcesAssetLoader = _container.Resolve<ResourcesAssetLoader>();
            _mainMenuUiRoot = _container.Resolve<MainMenuUIRoot>();
        }

        public LevelTilePresenter CreateLevelTilePresenter(LevelTileView view, int levelNumber)
        {
            return new LevelTilePresenter(_container.Resolve<CompletedLevelsService>(),
                _container.Resolve<SceneSwitcher>(),
                levelNumber,
                view);
        }

        public LevelsTileListPresenter CreateLevelsTileListPresenter(LevelsTileListView view)
        {
            return new LevelsTileListPresenter(_container.Resolve<ConfigsProviderService>().LevelsListConfig,
                view,
                this);
        }

        public LevelsMenuPopupPresenter CreateLevelsMenuPopupPresenter()
        {
            LevelsMenuPopupView levelsMenuPopupViewPrefab =
                _resourcesAssetLoader.LoadResource<LevelsMenuPopupView>(
                    "MainMenu/UI/LevelsPopupMenu/LevelsMenuPopupView");

            LevelsMenuPopupView levelsMenuPopupView =
                Object.Instantiate(levelsMenuPopupViewPrefab, _mainMenuUiRoot.PopupsLayer);
            
            return new LevelsMenuPopupPresenter(this, levelsMenuPopupView);
        }
    }
}