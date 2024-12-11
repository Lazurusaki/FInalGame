using System;
using FinalGame.Develop.CommonServices.DataManagement.DataProviders;
using FinalGame.Develop.Gameplay;
using FinalGame.Develop.Utils.Reactive;


[Serializable]
public struct GameStats 
{
    public ReactiveVariable<int> Wins; 
    public ReactiveVariable<int>  Losses;
}

namespace FinalGame.Develop.ADV_02
{
    public class GameResultsStatsService: IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private readonly GameStats _gameStats;
        
        public GameResultsStatsService(PlayerDataProvider playerDataProvider)
        {
            _gameStats.Wins = new ();
            _gameStats.Losses = new ();
            
            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }

        public IReadOnlyVariable<int> GetGameResultCount(GameResults gameResult)
        {
            switch (gameResult)
            {
                case GameResults.Win:
                    return _gameStats.Wins;
                case GameResults.Loose:
                    return _gameStats.Losses;
                default:
                    throw new ArgumentException($"Unknown game result {nameof(gameResult)}");
            }
        }

        public void Add(GameResults gameResult)
        {
            switch (gameResult)
            {
                case GameResults.Win:
                    _gameStats.Wins.Value++;
                    break;
                case GameResults.Loose:
                    _gameStats.Losses.Value++;
                    break;
                default:
                    throw new ArgumentException($"Unknown game result {nameof(gameResult)}");
            }
        }

        public void Reset()
        {
            _gameStats.Wins.Value = 0;
            _gameStats.Losses.Value = 0;
        }
        
        public void ReadFrom(PlayerData data)
        {
            _gameStats.Wins.Value = data.GameStats.Wins;
            _gameStats.Losses.Value = data.GameStats.Losses;
        }

        public void WriteTo(PlayerData data)
        {
            data.GameStats.Wins = _gameStats.Wins.Value;
            data.GameStats.Losses = _gameStats.Losses.Value;
        }
    }
}
