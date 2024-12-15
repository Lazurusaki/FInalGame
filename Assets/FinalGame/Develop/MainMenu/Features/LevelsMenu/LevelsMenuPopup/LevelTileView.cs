using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FinalGame.Develop.MainMenu.Features.LevelsMenu.LevelsMenuPopup
{
    public class LevelTileView : MonoBehaviour
    {
        public event Action Clicked;
        
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _levelNumberText;
        [SerializeField] private Button _button;

        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _completedColor;
        [SerializeField] private Color _blockedColor;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }
        
        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }
        
        public void SetLevel(string level) => _levelNumberText.text = level;

        public void SetBlock() => _background.color = _blockedColor;

        public void SetCompleted() => _background.color = _completedColor;

        public void SetActive() => _background.color = _activeColor;
        
        private void OnClick() => Clicked?.Invoke();
    }
}
