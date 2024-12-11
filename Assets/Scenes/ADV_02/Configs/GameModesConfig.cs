using System;
using System.Collections.Generic;
using System.Linq;
using FinalGame.Develop.Gameplay;
using UnityEngine;

namespace FinalGame.Develop.ADV_02.Configs
{
    public class GameModesConfig : ScriptableObject
    {
        [CreateAssetMenu(menuName = "ADV_02/Configs/GameModesConfig", fileName = "GameModesConfig")]
        public class GameResultIconsConfig : ScriptableObject
        {
            [SerializeField] private List<GameStatsIconConfig > _config;
            
            [Serializable]
            private class GameStatsIconConfig 
            {
                [field: SerializeField] public GameModes GameMode { get; private set; }
                [field: SerializeField] public Sprite Icon { get; private set; }
            }
        }
    }
}
