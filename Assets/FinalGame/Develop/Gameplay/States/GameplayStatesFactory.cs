using System;
using System.Collections.Generic;
using FinalGame.Develop.CommonServices.DataManagement.DataProviders;
using FinalGame.Develop.CommonServices.LevelsService;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.Configs.Gameplay.Levels;
using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.GameModes;
using FinalGame.Develop.Gameplay.Features.Input;
using FinalGame.Develop.Gameplay.Features.Loot;
using FinalGame.Develop.Gameplay.Features.MainHero;
using FinalGame.Develop.Gameplay.Features.Pause;
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

        public InitMainHeroState CreateInitMainCharacterState()
        {
            return new InitMainHeroState(_container.Resolve<MainHeroFactory>(), _container.Resolve<ConfigsProviderService>());
        }
        
        public GameplayStateMachine CreateGameplayLoopState(GameplaySceneInputArgs gameplaySceneInputArgs)
        {

            GameplayFinishConditionService  gameplayFinishConditionService = _container.Resolve<GameplayFinishConditionService>();
            StageProviderService stageProviderService = _container.Resolve<StageProviderService>();
            
            NextStagePrepareState nextStagePrepareState = CreateNextStagePrepareState();
            StageProcessState stageProcessState = CreateStageProcessState(gameplaySceneInputArgs);
            CollectLootState collectLootState = CreateCollectLootState();
            
            //ADD CREATE TRANSITION CONDITIONS
            ActionCondition preparationStageToProcessStageCondition =
                new ActionCondition(nextStagePrepareState.OnNextStageTriggerComplete);
            
            ActionCondition collectLootStageToPreparationStageCondition =
                new ActionCondition(collectLootState.LootCollected);

            ActionCondition stageProcessToCollectLootStageCondition =
                new ActionCondition(stageProcessState.StageComplete);

            gameplayFinishConditionService.WinCondition
                .Add(new ActionCondition(nextStagePrepareState.OnNextStageTriggerComplete))
                .Add(new FuncCondition(() => stageProviderService.StageResult == StageResults.Completed &&
                     stageProviderService.HasNextStage() == false));

            List<IDisposable> disposables = new List<IDisposable>
            {
                preparationStageToProcessStageCondition,
                stageProcessToCollectLootStageCondition,
                collectLootStageToPreparationStageCondition
            };

            //CREATE STATE MACHINE
            GameplayStateMachine gameplayLoopState = new GameplayStateMachine(disposables);

            //ADD STATES
            gameplayLoopState.AddState(nextStagePrepareState);
            gameplayLoopState.AddState(stageProcessState);
            gameplayLoopState.AddState(collectLootState);

            //ADD TRANSITIONS
            gameplayLoopState.AddTransition(nextStagePrepareState, stageProcessState, preparationStageToProcessStageCondition);
            gameplayLoopState.AddTransition(stageProcessState, collectLootState, stageProcessToCollectLootStageCondition);
            gameplayLoopState.AddTransition(collectLootState, nextStagePrepareState, collectLootStageToPreparationStageCondition);

            return gameplayLoopState;
        }

        public NextStagePrepareState CreateNextStagePrepareState()
        {
            return new NextStagePrepareState(_container.Resolve<EntityFactory>());
        }

        public StageProcessState CreateStageProcessState(GameplaySceneInputArgs gameplaySceneInputArgs)
        {
            return new StageProcessState(
                _container.Resolve<ConfigsProviderService>().LevelsListConfig.GetBy(gameplaySceneInputArgs.LevelNumber),
                _container.Resolve<GameModesFactory>(),
                _container.Resolve<StageProviderService>());
        }

        public WinState CreateWinState(GameplaySceneInputArgs gameplaySceneInputArgs)
        {
            return new WinState(
                _container.Resolve<IPauseService>(),
                _container.Resolve<IInputService>(),
                _container.Resolve<CompletedLevelsService>(),
                _container.Resolve<PlayerDataProvider>(),
                gameplaySceneInputArgs,
                _container.Resolve<SceneSwitcher>(),
                _container.Resolve<WalletService>(),
                _container.Resolve<MainHeroHolderService>());
        }
        
        public LooseState CreateLooseState()
        {
            return new LooseState(
                _container.Resolve<SceneSwitcher>(),
                _container.Resolve<IPauseService>(),
                _container.Resolve<IInputService>());
        }

        public CollectLootState CreateCollectLootState()
        {
            return new CollectLootState(
                _container.Resolve<LootPickupService>(),
                _container.Resolve<MainHeroHolderService>());
        }
    }
}