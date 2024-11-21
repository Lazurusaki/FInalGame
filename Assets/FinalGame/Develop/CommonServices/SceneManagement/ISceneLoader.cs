using System.Collections;
using UnityEngine.SceneManagement;

namespace FinalGame.Develop.CommonServices.SceneManagement
{
    public interface ISceneLoader : IService
    {
        IEnumerator LoadAsync(SceneID sceneID, LoadSceneMode loadSceneMode = LoadSceneMode.Single);
    }
}
