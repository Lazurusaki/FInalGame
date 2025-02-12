using FinalGame.Develop.CommonServices.CoroutinePerformer;
using FinalGame.Develop.CommonServices.SceneManagement;
using UnityEngine.SceneManagement;

namespace FinalGame.Develop.MainMenu.Features.StatsUpgradeMenu
{
    public class CharacterPreviewPresenter
    {
        private ISceneLoader _sceneLoader;
        private ICoroutinePerformer _coroutinePerformer;

        public CharacterPreviewPresenter(ISceneLoader sceneLoader, ICoroutinePerformer coroutinePerformer)
        {
            _sceneLoader = sceneLoader;
            _coroutinePerformer = coroutinePerformer;
        }

        public void Enable()
        {
            //_coroutinePerformer.StartPerform(_sceneLoader.LoadAsync(SceneID.CharacterPreviewScene, LoadSceneMode.Additive));
        }

        public void Disable()
        {
            //_coroutinePerformer.StartPerform(_sceneLoader.UnloadAsync(SceneID.CharacterPreviewScene));
        }
    }
}