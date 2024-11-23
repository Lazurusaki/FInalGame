using System;
using System.Collections;
using System.Collections.Generic;
using FinalGame.Develop.CommonServices.AssetsManagement;
using FinalGame.Develop.CommonServices.CoroutinePerformer;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.DI;
using Unity.VisualScripting;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Infrastructure
{
    public class GameplayBootstrap : MonoBehaviour
    {
        private const int ValuesCount = 6;
        private const KeyCode ContinueButton = KeyCode.Space;

        private DIContainer _container;
        private GameModes _gameMode;

        public IEnumerator Run(DIContainer container, GameplaySceneInputArgs sceneInputArgs)
        {
            Debug.Log($"Gameplay scene, GameMode - {sceneInputArgs.GameMode}");

            _container = container;
            _gameMode = sceneInputArgs.GameMode;

            var values = GenerateValues(_gameMode);

            ProcessRegistrations();

            yield return new WaitForSeconds(1);

            LaunchGame(values);
        }

        private List<char> GenerateValues(GameModes gameMode)
        {
            switch (gameMode)
            {
                case GameModes.Numbers:
                    return RandomValuesListGenerator.GenerateNumbers(ValuesCount);
                case GameModes.Letters:
                    return RandomValuesListGenerator.GenerateLetters(ValuesCount);
                default:
                    throw new ArgumentException("Game mode is not exist");
            }
        }

        private void LaunchGame(List<char> values)
        {
            _container.Resolve<ICoroutinePerformer>().StartPerform(Game(values));
        }

        private void ProcessRegistrations()
        {
            RegisterCoroutinePerformer(_container);
        }

        private void RegisterCoroutinePerformer(DIContainer container)
        {
            container.RegisterAsSingle<ICoroutinePerformer>(c =>
            {
                var resourcesAssetLoader = container.Resolve<ResourcesAssetLoader>();
                var coroutinePerformerPrefab =
                    resourcesAssetLoader.LoadResource<CoroutinePerformer>(InfrastructureAssetPaths
                        .CoroutinePerformerPath);
                return Instantiate(coroutinePerformerPrefab);
            });
        }

        private IEnumerator Game(List<char> values)
        {
            ShowValues(values);

            bool isFinished = false;
            GameResults gameResult = default;

            List<char> inputValues = new();

            while (isFinished == false)
            {
                foreach (var c in Input.inputString)
                {
                    inputValues.Add(char.ToUpper(c));

                    if (inputValues[^1] != values[inputValues.Count - 1])
                    {
                        isFinished = true;
                        gameResult = GameResults.Loose;
                    }
                    else if (inputValues.Count == values.Count)
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