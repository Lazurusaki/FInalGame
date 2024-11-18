using System.Collections;
using UnityEngine;

namespace FinalGame.Develop.CommonServices.CoroutinePerformer
{
    public class CoroutinePerformer : MonoBehaviour, ICoroutinePerformer
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public Coroutine StartPerform(IEnumerator coroutineFunction) => StartCoroutine(coroutineFunction);

        public void StopPerform(Coroutine coroutine) => StopCoroutine(coroutine);
    }
}
