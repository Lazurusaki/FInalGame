using System;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FinalGame.Develop.Gameplay.Features.Ability.View
{
    public class SelectAbilityView : MonoBehaviour
    {
        public event Action Selected;

        [SerializeField] private CanvasGroup _canvasGroup;

        [SerializeField] private Button _button;

        [SerializeField] private  AbilityIcon _icon;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;

        [SerializeField] private TMP_Text _textOnTablet;
        
        [SerializeField] private Image _selectImage;

        public AbilityIcon Icon => _icon;

        public void Subscribe() => _button.onClick.AddListener(OnClicked);

        public void Unsubscribe() => _button.onClick.RemoveListener(OnClicked);

        public void SetName(string name) => _name.text = name;

        public void SetDescription(string description) => _textOnTablet.text = description;

        public void SetTabletText(string tabletText) => _textOnTablet.text = tabletText;

        public void Select() => _selectImage.gameObject.SetActive(true);

        public void Unselect() => _selectImage.gameObject.SetActive(false);

        private void OnClicked()
        {
            Selected?.Invoke(); 
        }
    }
}
