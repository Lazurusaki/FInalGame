using System;
using System.Collections.Generic;
using FinalGame.Develop.CommonServices.DataManagement.DataProviders;
using FinalGame.Develop.CommonServices.LevelsService;
using FinalGame.Develop.CommonServices.Results;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.Configs.Gameplay.Levels;
using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Aim;
using FinalGame.Develop.Gameplay.Features.Attack;
using FinalGame.Develop.Gameplay.Features.GameModes;
using FinalGame.Develop.Gameplay.Features.GameModes.Wave.Presenters;
using FinalGame.Develop.Gameplay.Features.Input;
using FinalGame.Develop.Gameplay.Features.MainHero;
using FinalGame.Develop.Gameplay.Features.Pause;
using FinalGame.Develop.Gameplay.UI;
using FinalGame.Develop.Utils.Conditions;

namespace FinalGame.Develop.Gameplay.States
{
    public class GameplayStatesFactory
    {
        private readonly DIContainer _container;
        
        public GameplayStatesFactory(DIContainer container)
        {
            _container = container;
        }

        public InitMainHeroState CreateInitMainCharacterState(GameplaySceneInputArgs gameplaySceneInputArgs)
        {
            return new InitMainHeroState(_container.Resolve<ConfigsProviderService>().LevelsListConfig.GetBy(gameplaySceneInputArgs.LevelNumber),
                _container.Resolve<MainHeroFactory>(), 
                _container.Resolve<ConfigsProviderService>(), 
                _container.Resolve<GameplayUIPresentersFactory>());
        }
        
        public GameplayStateMachine CreateGameplayLoopState(GameplaySceneInputArgs gameplaySceneInputArgs)
        {
            GameplayStatesFactory gameplayStatesFactory = _container.Resolve<GameplayStatesFactory>();
            GameplayFinishConditionService  gameplayFinishConditionService = _container.Resolve<GameplayFinishConditionService>();
            StageProviderService stageProviderService = _container.Resolve<StageProviderService>();
            
            NextStagePrepareState nextStagePrepareState = gameplayStatesFactory.CreateNextStagePrepareState();
            StageProcessState stageProcessState = gameplayStatesFactory.CreateStageProcessState(gameplaySceneInputArgs);

            
            //ADD CREATE TRANSITION CONDITIONS
            ActionCondition preparationStageToProcessStageCondition =
                new ActionCondition(nextStagePrepareState.OnNextStageTriggerComplete);

            ActionCondition stageProcessToPreparationStageCondition =
                new ActionCondition(stageProcessState.StageComplete);

            gameplayFinishConditionService.WinCondition
                .Add(new FuncCondition(() => stageProviderService.StageResult == StageResults.Completed &&
                     stageProviderService.HasNextStage() == false));
            
            List<IDisposable> disposables = new List<IDisposable>
            {
                preparationStageToProcessStageCondition,
                stageProcessToPreparationStageCondition
            };

            //CREATE STATE MACHINE
            GameplayStateMachine gameplayLoopState = new GameplayStateMachine(disposables);

            //ADD STATES
            gameplayLoopState.AddState(nextStagePrepareState);
            gameplayLoopState.AddState(stageProcessState);

            //ADD TRANSITIONS
            gameplayLoopState.AddTransition(nextStagePrepareState, stageProcessState, preparationStageToProcessStageCondition);
            gameplayLoopState.AddTransition(stageProcessState, nextStagePrepareState, stageProcessToPreparationStageCondition);

            return gameplayLoopState;
        }

        public NextStagePrepareState CreateNextStagePrepareState()
        {
            return new NextStagePrepareState(_container.Resolve<WavePreparePresenter>() , _container.Resolve<IInputService>());
        }

        public StageProcessState CreateStageProcessState(GameplaySceneInputArgs gameplaySceneInputArgs)
        {
            return new StageProcessState(
                _container.Resolve<ConfigsProviderService>().LevelsListConfig.GetBy(gameplaySceneInputArgs.LevelNumber),
                _container.Resolve<GameModesFactory>(),
                _container.Resolve<StageProviderService>(),
                _container.Resolve<IInputService>(),
                _container.Resolve<AimPresenterCreatorService>(),
                _container.Resolve<MainHeroHolderService>(),
                _container.Resolve<EntityFactory>());
        }

        public WinState CreateWinState(GameplaySceneInputArgs gameplaySceneInputArgs)
        {
            return new WinState(
                _container.Resolve<IInputService>(),
                _container.Resolve<PlayerDataProvider>(),
                _container.Resolve<ConfigsProviderService>().LevelsListConfig.GetBy(gameplaySceneInputArgs.LevelNumber),
                _container.Resolve<SceneSwitcher>(),
                _container.Resolve<GameplayUIPresentersFactory>(),
                _container.Resolve<WalletService>(),
                    _container.Resolve<GameResultsStatsService>());
        }
        
        public LooseState CreateLooseState(GameplaySceneInputArgs gameplaySceneInputArgs)
        {
            return new LooseState(
                _container.Resolve<IInputService>(),
                _container.Resolve<PlayerDataProvider>(),
                _container.Resolve<SceneSwitcher>(),
                _container.Resolve<ConfigsProviderService>().LevelsListConfig.GetBy(gameplaySceneInputArgs.LevelNumber),
                _container.Resolve<GameplayUIPresentersFactory>(),
                _container.Resolve<GameResultsStatsService>());
        }
    }
}