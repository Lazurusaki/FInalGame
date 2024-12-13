using System;
using System.Collections;
using System.Collections.Generic;
using FinalGame.Develop.ADV_02.UI;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay;
using TMPro.EditorUtilities;
using UnityEngine;

namespace FinalGame.Develop.ADV_02
{
    public class GuessValues : ISequenceGameMode
    {
        public event Action Success;
        public event Action Fail;
        
        private readonly List<char> _values;
        
        public GuessValues(List<Char> symbols, int count)
        {
            _values = RandomValuesListGenerator.GenerateFrom(symbols, count);
        }
        
        public string GetSequence()
        {
            return new string(_values.ToArray());
        }
        
        public void Start()
        {
            
        }

        public void ValidateSequence(string sequence)
        {
            if (GetSequence().Equals(sequence, StringComparison.OrdinalIgnoreCase))
                Success?.Invoke();
            else
                Fail?.Invoke();
        }
    }
}