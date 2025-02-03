using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GradientBarWithText : MonoBehaviour
{
    [SerializeField] private Gradient _gradient;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _fill;
    [SerializeField] private TMP_Text _text;
    
    public void SetMaxValue(float value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException("Incorrect value");

        _slider.maxValue = value;
    }
    
    public void SetValue(float value)
    {
        if (value > _slider.maxValue || value < 0)
            throw new ArgumentOutOfRangeException("Incorrect value");

        
        _slider.value = value;
        _text.text = ((int)value).ToString();
        
        _fill.color = _gradient.Evaluate(_slider.value / _slider.maxValue);
    }
    
    public void Show()
    {
        if (_slider.gameObject.activeSelf == false)
            _slider.gameObject.SetActive(true);
    }

    public void Hide()
    {
        if (_slider.gameObject.activeSelf)
            _slider.gameObject.SetActive(false);
    }
}
