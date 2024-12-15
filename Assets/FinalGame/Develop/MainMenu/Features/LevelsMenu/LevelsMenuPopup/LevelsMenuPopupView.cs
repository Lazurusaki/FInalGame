using System;
using FinalGame.Develop.Configs.Gameplay.Levels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FinalGame.Develop.MainMenu.Features.LevelsMenu.LevelsMenuPopup
{
    public class LevelsMenuPopupView : MonoBehaviour
    {
        public event Action CloseRequest;
        
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _title;

        [SerializeField] private LevelsTileListView _levelsTileList;

        public LevelsTileListView LevelsTileList=> _levelsTileList;
        
        public void SetTitle(string title) => _title.text = title;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClick);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClick);
        }

        private void OnCloseButtonClick() => CloseRequest?.Invoke();
    }
}