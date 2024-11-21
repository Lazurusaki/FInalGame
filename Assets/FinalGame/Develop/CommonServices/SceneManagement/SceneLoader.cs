using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace FinalGame.Develop.CommonServices.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        public IEnumerator LoadAsync(SceneID sceneID, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            var waitLoading = SceneManager.LoadSceneAsync(sceneID.ToString(), loadSceneMode);

            if (waitLoading is null)
                throw new NullReferenceException($"{sceneID.ToString()} scene is not exist");

            while (waitLoading.isDone == false)
                yield return null;
        }
        
    }
}
