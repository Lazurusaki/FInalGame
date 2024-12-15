using System;
using FinalGame.Develop.ADV_02.UI;
using FinalGame.Develop.CommonServices.DataManagement.DataProviders;
using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay;
using static FinalGame.Develop.Gameplay.GameResults;

namespace FinalGame.Develop.ADV_02
{
    public class Game
    {
        private readonly DIContainer _container;
        private readonly ISequenceGameMode _gameMode;

        private readonly ICondition _winCondition;
        private readonly ICondition _looseCondition;

        public event Action<GameResults> GameOver;

        public Game(DIContainer container, ICondition winCondition, ICondition looseCondition)
        {
            _container = container;
            _gameMode = _container.Resolve<GameModeHandler>().GameMode;

            _winCondition = winCondition;
            _looseCondition = looseCondition;
        }

        private void OnWin()
        {
            Stop();
            UpdatePlayerData(Win);
            GameOver?.Invoke(Win);
        }

        private void OnLoose()
        {
            Stop();
            UpdatePlayerData(Loose);
            GameOver?.Invoke(Loose);
        }

        public void Start()
        {
            _winCondition.Completed += OnWin;
            _looseCondition.Completed += OnLoose;
            _winCondition.Start();
            _looseCondition.Start();
        }

        private void Stop()
        {
            _winCondition.Completed -= OnWin;
            _looseCondition.Completed -= OnLoose;
            _winCondition.Reset();
            _looseCondition.Reset();
        }

        private void UpdatePlayerData(GameResults gameResult)
        {
            _container.Resolve<GameResultsStatsService>().Add(gameResult);
    
            var rewardData = _container.Resolve<ConfigsProviderService>().GameRewardsConfig.GetReward(gameResult);
        
            switch (gameResult)
            {
                case GameResults.Win:
                    HandleWin(rewardData);
                    break;
                case GameResults.Loose:
                    HandleLoose(rewardData);
                    break;
                default:
                    throw new ArgumentException($"Unexpected game result - {gameResult}");
            }
            
            SavePlayerData();
        }

        private void HandleWin(Currency rewardData)
        {
            _container.Resolve<WalletService>().Add(rewardData.CurrencyType, rewardData.Value);
        }
        
        private void HandleLoose(Currency rewardData)
        {
            var walletService = _container.Resolve<WalletService>();
            
            if (walletService.HasEnough(rewardData.CurrencyType,rewardData.Value ))
                walletService.Spend(rewardData.CurrencyType, rewardData.Value);
        }

        private void SavePlayerData()
        {
            _container.Resolve<PlayerDataProvider>().Save();
        }
    }
}