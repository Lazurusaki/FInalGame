using System.Collections;
using FinalGame.Develop.CommonServices.AssetsManagement;
using FinalGame.Develop.CommonServices.CoroutinePerformer;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.Configs.Gameplay.Levels;
using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.AI;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Ability;
using FinalGame.Develop.Gameplay.Features.Ability.AbilityDrop;
using FinalGame.Develop.Gameplay.Features.Ability.Presenters;
using FinalGame.Develop.Gameplay.Features.Enemy;
using FinalGame.Develop.Gameplay.Features.GameModes;
using FinalGame.Develop.Gameplay.Features.Input;
using FinalGame.Develop.Gameplay.Features.LevelUp;
using FinalGame.Develop.Gameplay.Features.Loot;
using FinalGame.Develop.Gameplay.Features.MainHero;
using FinalGame.Develop.Gameplay.Features.Pause;
using FinalGame.Develop.Gameplay.Features.Team;
using FinalGame.Develop.Gameplay.States;
using FinalGame.Develop.Gameplay.UI;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Infrastructure
{
    public class GameplayBootstrap : MonoBehaviour
    {
        private DIContainer _container;
        private GameplaySceneInputArgs _gameplaySceneInputArgs;
        private GameplayStateMachine _gameplayStateMachine;

        public IEnumerator Run(DIContainer container, GameplaySceneInputArgs sceneInputArgs)
        {
            _container = container;
            _gameplaySceneInputArgs = sceneInputArgs;

            ProcessRegistrations();

            yield return new WaitForSeconds(0.01f);

            _gameplayStateMachine = _container.Resolve<GameplayStateMachinesFactory>()
                .CreateGameplayStateMachine(sceneInputArgs);
            
            _gameplayStateMachine.Enter();
        }

        private void ProcessRegistrations()
        {
            RegisterEntityFactory();
            RegisterAIFactory();
            RegisterMainHeroFactory();
            RegisterEnemyFactory();
            RegisterEntitiesBuffer();
            RegisterInputService();
            RegisterGameModesFactory();
            RegisterMainHeroHolder();
            RegisterGameplayStatesFactory();
            RegisterGameplayStateMachineDisposer();
            RegisterStageProviderService();
            RegisterTimeScalePauseService();
            RegisterGameplayFinishConditionService();
            RegisterMainHeroFinishConditionCreatorService();
            RegisterStateMachinesFactory();
            RegisterAbilityFactory();
            RegisterAbilityDropService();
            RegisterAbilityPresentersFactory();
            RegisterGameplayUIRoot();
            RegisterDropAbilityOnLevelUpService();
            RegisterLootFactory();
            RegisterDropLootService();
            RegisterLootPickupService();
            
            _container.Initialize();
        }


        private SelectAbilityPopupPresenter _popup;
        
        private void Update()
        {
            _gameplayStateMachine?.Update(Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Alpha0))
                foreach (Entity entity in _container.Resolve<EntitiesBuffer>().Elements)
                    if (entity.TryGetTeam(out var team) && team.Value == TeamTypes.Enemies)
                        if (entity.TryGetTakeDamageRequest(out var takeDamageRequest))
                            takeDamageRequest.Invoke(9999);

            if (Input.GetKeyDown(KeyCode.T))
            {
                _container.Resolve<MainHeroHolderService>().MainHero.GetExperience().Value += 400;
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                _container.Resolve<IPauseService>().Pause();
                _popup = _container.Resolve<AbilityPresentersFactory>()
                    .CreateSelectAbilityPopupPresenter(_container.Resolve<MainHeroHolderService>().MainHero);
                _popup.Enable();
            }
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                _container.Resolve<MainHeroHolderService>().MainHero.GetAttackTrigger().Invoke();
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                _popup.Disable(() => _container.Resolve<IPauseService>().Unpause());
            }
        }

        private void RegisterEntityFactory()
            => _container.RegisterAsSingle(c => new EntityFactory(c));

        private void RegisterAIFactory()
            => _container.RegisterAsSingle(c => new AIFactory(c));

        private void RegisterMainHeroFactory()
            => _container.RegisterAsSingle(c => new MainHeroFactory(c));

        private void RegisterEnemyFactory()
            => _container.RegisterAsSingle(c => new EnemyFactory(c));

        private void RegisterEntitiesBuffer()
            => _container.RegisterAsSingle(c => new EntitiesBuffer());

        private void RegisterInputService() //Тип управление регистрируем тут Desktop/mobile 
            => _container.RegisterAsSingle<IInputService>(c => new DesktopInput());

        private void RegisterGameModesFactory()
            => _container.RegisterAsSingle(c => new GameModesFactory(c));

        private void RegisterMainHeroHolder()
            => _container.RegisterAsSingle(c => new MainHeroHolderService());

        private void RegisterGameplayStatesFactory()
            => _container.RegisterAsSingle(c => new GameplayStatesFactory(c));

        private void RegisterGameplayStateMachineDisposer()
            => _container.RegisterAsSingle(c => new GameplayStateMachineDisposer());

        private void RegisterStageProviderService()
            => _container.RegisterAsSingle(c => new StageProviderService(
                c.Resolve<ConfigsProviderService>().LevelsListConfig.GetBy(_gameplaySceneInputArgs.LevelNumber)));

        private void RegisterTimeScalePauseService()
            => _container.RegisterAsSingle<IPauseService>(c => new TimeScalePauseService());

        private void RegisterGameplayFinishConditionService()
            => _container.RegisterAsSingle(c => new GameplayFinishConditionService());

        private void RegisterMainHeroFinishConditionCreatorService()
            => _container.RegisterAsSingle(c => new MainHeroFinishConditionCreator(
                c.Resolve<MainHeroHolderService>(),
                c.Resolve<GameplayFinishConditionService>())).NonLazy();

        private void RegisterStateMachinesFactory()
            => _container.RegisterAsSingle(c => new GameplayStateMachinesFactory(c));

        private void RegisterAbilityFactory()
            => _container.RegisterAsSingle(c => new AbilityFactory(c));

        private void RegisterAbilityDropService()
            => _container.RegisterAsSingle(c => new AbilityDropService(
                c.Resolve<ConfigsProviderService>().LevelsListConfig.GetBy(
                    _gameplaySceneInputArgs.LevelNumber).AbilityDropOptionsConfig,
                    new AbilityDropRules()));

        private void RegisterAbilityPresentersFactory()
            => _container.RegisterAsSingle(c => new AbilityPresentersFactory(c));
        
        private void RegisterGameplayUIRoot()
        {
            _container.RegisterAsSingle( c =>
            {
                var gameplayUIRootPrefab = c.Resolve<ResourcesAssetLoader>().LoadResource<GameplayUIRoot>("Gameplay/UI/GameplayUIRoot");
                return Instantiate(gameplayUIRootPrefab);
            }).NonLazy();
        }

        private void RegisterDropAbilityOnLevelUpService() =>
            _container.RegisterAsSingle(c => new DropAbilityOnLevelUpService(
                c.Resolve<MainHeroHolderService>(),
                c.Resolve<AbilityPresentersFactory>(),
                c.Resolve<IPauseService>(),
                c.Resolve<ICoroutinePerformer>())).NonLazy();
        
        private void RegisterLootFactory() => _container.RegisterAsSingle(c => new LootFactory(c));

        private void RegisterDropLootService() => _container.RegisterAsSingle(c
            => new DropLootService(c.Resolve<ConfigsProviderService>(), c.Resolve<LootFactory>()));

        private void RegisterLootPickupService() => _container.RegisterAsSingle(c
            => new LootPickupService(c.Resolve<EntitiesBuffer>())).NonLazy();
    }
}
