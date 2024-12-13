using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace FinalGame.Develop.Gameplay
{
    public static class RandomValuesListGenerator
    {
        public static List<char> GenerateFrom(List<char> symbols, int count)
        {
            var values = new List<char>();

            for (var i = 0; i < count; i++)
                values.Add(symbols[Random.Range(0, symbols.Count)]);

            return values;
        }
        
        public static List<char> Generate(ValueTypes valuesType, int count)
        {
            switch (valuesType)
            {
                case ValueTypes.Numbers:
                    return GenerateNumbers(count);
                case ValueTypes.Letters:
                    return GenerateLetters(count);
                default:
                    throw new ArgumentException("Values type is not exist");
            }
        }
        
        private static List<char> GenerateNumbers(int count)
        {
            var values = new List<char>();

            for (var i = 0; i < count; i++)
                values.Add((char)('0' + Random.Range(0, 10)));

            return values;
        }
        
        private static List<char> GenerateLetters(int count)
        {
            var values = new List<char>();

            for (var i = 0; i < count; i++)
                values.Add((char)Random.Range('A', 'Z' + 1));

            return values;
        }
    }
}
