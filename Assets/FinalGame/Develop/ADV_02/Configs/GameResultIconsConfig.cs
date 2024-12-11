using System;
using System.Collections.Generic;
using System.Linq;
using FinalGame.Develop.Gameplay;
using UnityEngine;

namespace FinalGame.Develop.ADV_02.Configs
{
    [CreateAssetMenu(menuName = "ADV_02/Configs/GameResultIconsConfig", fileName = "GameResulIconsConfig")]
    public class GameResultIconsConfig : ScriptableObject
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
