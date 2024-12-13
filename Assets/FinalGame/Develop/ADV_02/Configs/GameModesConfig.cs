using System;
using System.Collections.Generic;
using System.Linq;
using FinalGame.Develop.Gameplay;
using UnityEngine;

namespace FinalGame.Develop.ADV_02.Configs
{
    [CreateAssetMenu(menuName = "ADV_02/Configs/GameModesConfig", fileName = "GameModesConfig")]
    public class GameModesConfig : ScriptableObject
    {
        [SerializeField] private List<GameModeConfig> _gameModeConfigs;

        public int GetSymbolsCount(GameModes gameMode) =>
            _gameModeConfigs.First(config => config.GameMode == gameMode).SymbolsCount;
        
        public List<char> GetSymbolsFor(GameModes gameMode) =>
            _gameModeConfigs.First(config => config.GameMode == gameMode).Symbols;

        [Serializable]
        private class GameModeConfig
        {
            [field: SerializeField] public GameModes GameMode;
            [field: SerializeField] public int  SymbolsCount;
            [field: SerializeField] public List<char> Symbols;
        }
    }
}