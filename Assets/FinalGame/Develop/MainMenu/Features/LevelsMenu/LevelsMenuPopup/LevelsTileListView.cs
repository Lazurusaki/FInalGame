using System.Collections.Generic;
using UnityEngine;

namespace FinalGame.Develop.MainMenu.Features.LevelsMenu.LevelsMenuPopup
{
    public class LevelsTileListView : MonoBehaviour
    {
        [SerializeField] private LevelTileView _levelTileViewPrefab;
        [SerializeField] private Transform _parent;

        private readonly List<LevelTileView> _elements = new();

        public LevelTileView SpawnElement()
        {
            var levelTileView = Instantiate(_levelTileViewPrefab, _parent);
            
            _elements.Add(levelTileView);
            
            return levelTileView;
        }
        
        public void Remove(LevelTileView levelTileView)
        {
            _elements.Remove(levelTileView);
            Destroy(levelTileView.gameObject);
        }
    }
}