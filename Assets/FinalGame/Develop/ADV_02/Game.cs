using System;
using FinalGame.Develop.ADV_02.UI;
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

        public event Action <GameResults> GameOver;

        public Game(DIContainer container, ICondition winCondition, ICondition looseCondition)
        {
            _container = container;
            _gameMode= _container.Resolve<GameModeHandler>().GameMode;
            
            

            _winCondition = winCondition;
            _looseCondition = looseCondition;
        }

        private void OnWin()
        {
            Stop();
            GameOver?.Invoke(Win);
        }

        private void OnLoose()
        {
            Stop();
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
    }
}