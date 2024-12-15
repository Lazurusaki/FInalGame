using System.Collections.Generic;
using FinalGame.Develop.Configs.Gameplay.Levels;

namespace FinalGame.Develop.MainMenu.Features.LevelsMenu.LevelsMenuPopup
{
    public class LevelsTileListPresenter
    {
        //Model
        private readonly LevelListConfig _levelListConfig;
        private readonly LevelsMenuPopupFactory _factory;
        private readonly List<LevelTilePresenter> _levelTilePresenters = new();
        
        //view
        private readonly LevelsTileListView _view;
        
        public LevelsTileListPresenter(LevelListConfig levelListConfig, LevelsTileListView view,  LevelsMenuPopupFactory factory)
        {
            _levelListConfig = levelListConfig;
            _view = view;
            _factory= factory;
        }

        public void Enable()
        {
            for (var i = 0; i < _levelListConfig.Levels.Count; i++)
            {
                var levelTileView = _view.SpawnElement();

                var levelTilePresenter = _factory.CreateLevelTilePresenter(levelTileView, i + 1);
                levelTilePresenter.Enable();
                
                _levelTilePresenters.Add(levelTilePresenter);
            }
        }

        public void Disable()
        {
            foreach (var presenter in _levelTilePresenters)
            {
                presenter.Disable();
                _view.Remove(presenter.View);
            }
            
            _levelTilePresenters.Clear();
        }
    }
}