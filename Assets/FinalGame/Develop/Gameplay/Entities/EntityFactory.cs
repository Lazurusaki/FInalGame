using FinalGame.Develop.CommonServices.AssetsManagement;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.Features.Movement;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Entities
{
    public class EntityFactory
    {
        private const string GhostPrefabPath = "Gameplay/Creatures/Ghost";

        private readonly DIContainer _container;
        private readonly ResourcesAssetLoader _assetsLoader;

        public EntityFactory(DIContainer container)
        {
            _container = container;
            _assetsLoader = container.Resolve<ResourcesAssetLoader>();
        }

        public Entity CreateEntity(Vector3 position)
        {
            var prefab = _assetsLoader.LoadResource<Entity>(GhostPrefabPath);

            var instance = Object.Instantiate(prefab, position, Quaternion.identity, null);

            instance
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(10))
                .AddRotationDirection()
                .AddRotationSpeed(new ReactiveVariable<float>(900));

            instance
                .AddBehavior(new CharacterControllerMovementBehavior())
                .AddBehavior(new RotationBehavior());
            
            instance.Initialize();
            
            return instance;
        }
    }
}