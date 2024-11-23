using System.Collections;
using FinalGame.Develop.CommonServices.AssetsManagement;
using FinalGame.Develop.CommonServices.CoroutinePerformer;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.DI;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Infrastructure
{
    public class GameplayBootstrap : MonoBehaviour
    {
        private const int ValuesCount = 6;
        
        public IEnumerator Run(DIContainer container, GameplaySceneInputArgs sceneInputArgs)
        {
            Debug.Log($"Gameplay scene, GameMode - {sceneInputArgs.GameMode}");
            
            ProcessRegistrations(container);
            
            Game game = new(container, sceneInputArgs.GameMode, ValuesCount);

            yield return new WaitForSeconds(1);

            game.Launch();
        }
        
        private void ProcessRegistrations(DIContainer container)
        {
            RegisterCoroutinePerformer(container);
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
    }
}