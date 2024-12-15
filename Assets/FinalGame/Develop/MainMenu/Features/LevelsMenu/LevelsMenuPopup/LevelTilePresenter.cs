using FinalGame.Develop.CommonServices.LevelsService;
using FinalGame.Develop.CommonServices.SceneManagement;
using UnityEngine;

namespace FinalGame.Develop.MainMenu.Features.LevelsMenu.LevelsMenuPopup
{
    public class LevelTilePresenter
    {
        private const int FirstLevelNumber = 1;

        //model
        private readonly CompletedLevelsService _levelsService;
        private readonly SceneSwitcher _sceneSwitcher;
        private readonly int _levelNumber;

        //view
        private readonly LevelTileView _view;

        private bool _isBlocked;

        public LevelTilePresenter(CompletedLevelsService levelsService, SceneSwitcher sceneSwitcher, int levelNumber,
            LevelTileView view)
        {
            _levelsService = levelsService;
            _sceneSwitcher = sceneSwitcher;
            _levelNumber = levelNumber;
            _view = view;
        }

        public LevelTileView View => _view;

        public void Enable()
        {
            _isBlocked = _levelNumber != FirstLevelNumber && PreviousLevelCompleted() == false;
            
            _view.SetLevel(_levelNumber.ToString());

            if (_isBlocked)
            {
                _view.SetBlock();
            }
            else
            {
                if (_levelsService.IsLevelCompleteted(_levelNumber))
                    _view.SetCompleted();
                else
                    _view.SetActive();
            }

            _view.Clicked += OnViewClicked;
        }
        
        public void Disable()
        {
            _view.Clicked -= OnViewClicked;
        }

        private void OnViewClicked()
        {
            if (_isBlocked)
            {
                Debug.Log("Level disabled");
                return;
            }

            if (_levelsService.IsLevelCompleteted(_levelNumber))
            {
                Debug.Log("Level already completed");
                return;
            }
            
            _sceneSwitcher.ProcessSwitchSceneFor(new MainMenuSceneOutputArgs(new GameplaySceneInputArgs(_levelNumber)));
        }
        
        private bool PreviousLevelCompleted()
            => _levelsService.IsLevelCompleteted(_levelNumber - 1);
    }
}