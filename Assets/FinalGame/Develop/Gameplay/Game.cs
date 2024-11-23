using System;
using System.Collections;
using System.Collections.Generic;
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
        private readonly GameModes _gameMode;
        private readonly int _valuesCount;
        private readonly List<char> _values;
        
        public Game(DIContainer container, GameModes gameMode, int valuesCount)
        {
            _container = container;
            _gameMode = gameMode;
            _valuesCount = valuesCount;
            _values = GenerateValues(_gameMode);
        }
        
        public void Launch()
        {
            _container.Resolve<ICoroutinePerformer>().StartPerform(GameLoop());
        }
        
        private List<char> GenerateValues(GameModes gameMode)
        {
            switch (gameMode)
            {
                case GameModes.Numbers:
                    return RandomValuesListGenerator.GenerateNumbers(_valuesCount);
                case GameModes.Letters:
                    return RandomValuesListGenerator.GenerateLetters(_valuesCount);
                default:
                    throw new ArgumentException("Game mode is not exist");
            }
        }
        
        private IEnumerator GameLoop()
        {
            ShowValues(_values);

            bool isFinished = false;
            GameResults gameResult = default;

            List<char> inputValues = new();

            while (isFinished == false)
            {
                foreach (var c in Input.inputString)
                {
                    inputValues.Add(char.ToUpper(c));

                    if (inputValues[^1] != _values[inputValues.Count - 1])
                    {
                        isFinished = true;
                        gameResult = GameResults.Loose;
                    }
                    else if (inputValues.Count == _values.Count)
                    {
                        isFinished = true;
                        gameResult = GameResults.Win;
                    }
                }

                yield return null;
            }

            _container.Resolve<ICoroutinePerformer>().StartPerform(ProcessGameResult(gameResult));
        }

        private IEnumerator ProcessGameResult(GameResults gameResult)
        {
            bool isFinished = false;

            switch (gameResult)
            {
                case GameResults.Win:
                {
                    ShowWinMessage();
                    ShowReturnToMainMenuMessage();
                    break;
                }
                case GameResults.Loose:
                {
                    ShowLooseMessage();
                    ShowTryAgainMessage();
                    break;
                }
            }

            while (isFinished == false)
            {
                if (Input.GetKeyDown(ContinueButton))
                    isFinished = true;

                yield return null;
            }

            ChangeSceneTo(GetNextSceneFromGameResult(gameResult));
        }

        private SceneID GetNextSceneFromGameResult(GameResults gameResult)
            => gameResult == GameResults.Win ? SceneID.MainMenu : SceneID.Gameplay;

        private void ChangeSceneTo(SceneID scene)
        {
            switch (scene)
            {
                case SceneID.MainMenu:
                {
                    _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new GameplaySceneOutputArgs(
                        new MainMenuSceneInputArgs()));
                    break;
                }
                case SceneID.Gameplay:
                {
                    _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new GameplaySceneOutputArgs(
                        new GameplaySceneInputArgs(_gameMode)));
                    break;
                }
            }
        }

        private void ShowValues(List<char> values)
        {
            Debug.Log(string.Join(" ", values));
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
    }
}
