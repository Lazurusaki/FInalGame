using System;
using System.Collections.Generic;
using System.Linq;
using FinalGame.Develop.CommonServices.Results;
using UnityEngine;

namespace FinalGame.Develop.Configs.Common.Results
{
    [CreateAssetMenu(menuName = "Configs/Common/Results/ResultsIconsConfig", fileName = "ResultsIconsConfig")]
    public class ResultsIconsConfig : ScriptableObject
    {
        [SerializeField] private List<GameStatsIconConfig > _config;

        public Sprite GetSpriteFor(GameResults gameResult) =>
            _config.First(config => config.GameResult == gameResult).Icon;
        
        [Serializable]
        private class GameStatsIconConfig 
        {
            [field: SerializeField] public GameResults GameResult { get; private set; }
            [field: SerializeField] public Sprite Icon { get; private set; }
        }
    }
}