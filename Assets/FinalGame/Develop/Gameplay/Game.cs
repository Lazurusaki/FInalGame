using System.Collections.Generic;
using UnityEngine;

namespace FinalGame.Develop.Gameplay
{
    public class Game
    {
        private List<int> _numbers;

        private List<int> GenerateNumbers(int count)
        {
            var numbers = new List<int>();

            for (var i = 0; i < count; i++)
                numbers.Add(Random.Range(0, 10));

            return numbers;
        }

        private List<char> GenerateLetters(int count)
        {
            var letters = new List<char>();

            for (var i = 0; i < count; i++)
                letters.Add((char)Random.Range('A', 'Z' + 1));

            return letters;
        }
    }
}
