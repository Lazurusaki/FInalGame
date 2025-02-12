using System;
using System.Collections.Generic;
using FinalGame.Develop.Configs.Gameplay.Creatures;
using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.AI;
using FinalGame.Develop.Gameplay.AI.Sensors;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Ability;
using FinalGame.Develop.Gameplay.Features.LevelUp;
using FinalGame.Develop.Gameplay.Features.Stats;
using FinalGame.Develop.Gameplay.Features.Team;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.MainHero
{
    public class MainHeroFactory
    {
        private const int Team = TeamTypes.MainHero;

        private readonly EntitiesBuffer _entitiesBuffer;
        private readonly EntityFactory _entityFactory;
        private readonly AIFactory _aiFactory;
        private readonly MainHeroHolderService _mainHeroHolderService;
        private readonly ConfigsProviderService _configs;
        private readonly StatsUpgradeService _statsUpgradeService;
        
        public MainHeroFactory(DIContainer container)
        {
            _entityFactory = container.Resolve<EntityFactory>();
            _entitiesBuffer = container.Resolve<EntitiesBuffer>();
            _aiFactory = container.Resolve<AIFactory>();
            _mainHeroHolderService = container.Resolve<MainHeroHolderService>();
            _configs = container.Resolve<ConfigsProviderService>();
            _statsUpgradeService = container.Resolve<StatsUpgradeService>();
        }

        public Entity Create(Vector3 position, MainHeroConfig config)
        {
            Entity entity = _entityFactory.CreateMainHero(position, GetStats(), config, Team);
            AIStateMachine brain = _aiFactory.CreateMainHeroBehavior(entity, new NearestDamageableTargetSelector(entity.GetTransform(),entity.GetTeam()));
            
            entity
                .AddIsMainHero(new ReactiveVariable<bool>(true))
                .AddLevel(new ReactiveVariable<int>(1))
                .AddExperience()
                .AddCoins()
                .AddAbilityList();

            entity
                .AddBehavior(new StateMachineBrainBehavior(brain))
                .AddBehavior(new LevelUpBehavior(_configs.ExperienceForLevelUplConfig))
                .AddBehavior(new AbilityOnAddActivatorBehavior());
            
            _mainHeroHolderService.Register(entity);
            _entitiesBuffer.Add(entity);
            
            return entity;
        }

        private Dictionary<StatTypes, float> GetStats()
        {
            Dictionary<StatTypes, float> stats = new();

            foreach (StatTypes statType in Enum.GetValues(typeof(StatTypes)))
                stats.Add(statType, _statsUpgradeService.GetCurrentStatValueFor(statType));

            return stats;
        }
    }
}