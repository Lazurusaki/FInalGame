using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    public void Initialize(Action action) => _button.onClick.AddListener(() => action?.Invoke());
}
