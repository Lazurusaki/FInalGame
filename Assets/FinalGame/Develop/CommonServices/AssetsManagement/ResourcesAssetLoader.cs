using UnityEngine;

namespace FinalGame.Develop.CommonServices.AssetsManagement
{
    public class ResourcesAssetLoader
    {
        public T LoadResource<T>(string path) where T : Object
            => UnityEngine.Resources.Load<T>(path);
    }
}
