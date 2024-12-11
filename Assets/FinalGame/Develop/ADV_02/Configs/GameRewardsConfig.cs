using System;
using System.Collections.Generic;
using System.Linq;
using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.Gameplay;
using UnityEngine;

namespace FinalGame.Develop.ADV_02.Configs
{
    [CreateAssetMenu(menuName = "ADV_02/Configs/GameRewardsConfig", fileName = "GameRewardsConfig")]
    public class GameRewardsConfig : ScriptableObject
    {
        [SerializeField] private List<Reward> _rewards;

        public CurrencyReward GetReward(GameResults gameResult)
            => _rewards.First(_rewards => _rewards.GameResult == gameResult).CurrencyReward;

        [Serializable]
        private class Reward
        {
            [field: SerializeField] public GameResults GameResult { get; private set; }
            [field: SerializeField] public CurrencyReward CurrencyReward { get; private set; }
        }
    }
}