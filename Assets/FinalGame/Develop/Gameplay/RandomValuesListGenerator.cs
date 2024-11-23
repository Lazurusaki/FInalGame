using System.Collections.Generic;
using UnityEngine;

namespace FinalGame.Develop.Gameplay
{
    public static class RandomValuesListGenerator
    {
        public static List<char> GenerateNumbers(int count)
        {
            var values = new List<char>();

            for (var i = 0; i < count; i++)
                values.Add((char)('0' + Random.Range(0, 10)));

            return values;
        }

        public static List<char> GenerateLetters(int count)
        {
            var values = new List<char>();

            for (var i = 0; i < count; i++)
                values.Add((char)Random.Range('A', 'Z' + 1));

            return values;
        }
    }
}
