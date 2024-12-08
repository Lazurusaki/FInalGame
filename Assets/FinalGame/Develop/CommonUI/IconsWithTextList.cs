using System;
using System.Collections.Generic;
using UnityEngine;

namespace FinalGame.Develop.CommonUI
{
    public class IconsWithTextList : MonoBehaviour
    {
        [SerializeField] private IconWithText _iconWithTextPrefab;
        [SerializeField] private Transform _parent;

        private readonly List<IconWithText> _elements = new();

        public IconWithText SpawnElement()
        {
            var iconWithText = Instantiate(_iconWithTextPrefab, _parent);
            _elements.Add(iconWithText);
            
            return iconWithText;
        }

        public void Remove(IconWithText iconWithText)
        {
            _elements.Remove(iconWithText);
            Destroy(iconWithText.gameObject);
        }
    }
}
