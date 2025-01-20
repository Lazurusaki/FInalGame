using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FinalGame.Develop.Gameplay.Features.Ability.View
{
    public class AbilityIcon : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Transform _levelParent;
        [SerializeField] private TMP_Text _level;

        public void HideLevel() => _level.gameObject.SetActive(false);
        
        public void ShowLevel() => _levelParent.gameObject.SetActive(true);

        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        public void SetLevel(string level)
        {
            _level.text = level;
        }
    }
}