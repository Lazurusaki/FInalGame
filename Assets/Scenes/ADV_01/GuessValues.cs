using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinalGame.Develop.Gameplay
{
    public class GuessValues : IGameMode
    {
        private const int Count = 6;
        
        public event Action Success;
        public event Action Fail;

        private readonly List<char> _values;
        
        public GuessValues(ValueTypes valuesType)
        {
            _values = RandomValuesListGenerator.Generate(valuesType, Count);
        }
        
        public IEnumerator Start()
        {
            ShowValues(_values);

            bool isFinished = false;
            
            List<char> inputValues = new();

            while (isFinished == false)
            {
                foreach (var c in Input.inputString)
                {
                    inputValues.Add(char.ToUpper(c));

                    if (inputValues[^1] != _values[inputValues.Count - 1])
                    {
                        isFinished = true;
                        Fail?.Invoke();
                    }
                    else if (inputValues.Count == _values.Count)
                    {
                        isFinished = true;
                        Success?.Invoke();
                    }
                }

                yield return null;
            }
        }
        
        private void ShowValues(List<char> values)
        {
            Debug.Log(string.Join(" ", values));
        }
    }
}
