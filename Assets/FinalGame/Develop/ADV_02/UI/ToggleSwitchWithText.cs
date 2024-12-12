using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Positions
{
    Left,
    Right
}

namespace FinalGame.Develop.ADV_02.UI
{
    public class ToggleSwitchWithText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textLeft;
        [SerializeField] private TMP_Text _textRight;
        [SerializeField] private Slider _slider;

        public Slider GetSlider => _slider;
        
        public void SetText(Positions position, string text)
        {
            switch (position)
            {
                case Positions.Left:
                    _textLeft.text = text;
                    break;
                case Positions.Right:
                    _textRight.text = text;
                    break;
                default:
                    throw new ArgumentException($"Unknown position {nameof(position)}");
            }
        }
    }
}
