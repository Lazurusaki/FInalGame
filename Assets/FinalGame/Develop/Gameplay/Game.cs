using System;
using System.Collections;
using FinalGame.Develop.CommonServices.CoroutinePerformer;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.DI;
using UnityEngine;

namespace FinalGame.Develop.Gameplay
{
    public class Game
    {
        private const KeyCode ContinueButton = KeyCode.Space;

        private readonly DIContainer _container;
        private readonly IGameMode _gameMode;

        private readonly ICondition _winCondition;
        private readonly ICondition _looseCondition;
        
        public event Action<GameResults> GameOver ;

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
            ShowWinMessage();
            ShowReturnToMainMenuMessage();

            HandleGameEnd(() => { GameOver?.Invoke(GameResults.Win); });
        }

        private void OnLoose()
        {
            Stop();
            ShowLooseMessage();
            ShowTryAgainMessage();

            HandleGameEnd(() => { GameOver?.Invoke(GameResults.Loose); });
        }

        private void HandleGameEnd(Action endGameAction)
        {
            _container.Resolve<ICoroutinePerformer>().StartPerform(WaitForContinue(endGameAction));
        }

        private IEnumerator WaitForContinue(Action onComplete)
        {
            var isFinished = false;

            while (isFinished == false)
            {
                if (Input.GetKeyDown(ContinueButton))
                    isFinished = true;

                yield return null;
            }

            onComplete.Invoke();
        }

        private void ShowWinMessage()
        {
            Debug.Log("You Win!");
        }

        private void ShowLooseMessage()
        {
            Debug.Log("You Loose...");
        }

        private void ShowTryAgainMessage()
        {
            Debug.Log($"Press {ContinueButton} to try again");
        }

        private void ShowReturnToMainMenuMessage()
        {
            Debug.Log($"Press {ContinueButton} to return to main menu");
        }
        
        private void Stop()
        {
            _winCondition.Completed -= OnWin;
            _looseCondition.Completed -= OnLoose;
            _winCondition.Reset();
            _looseCondition.Reset();
        }
        
        public void Start()
        {
            Debug.Log($"GameMode - {_gameMode}");

            _container.Resolve<ICoroutinePerformer>().StartPerform(_gameMode.Start());

            _winCondition.Completed += OnWin;
            _looseCondition.Completed += OnLoose;
            _winCondition.Start();
            _looseCondition.Start();
        }
    }
}