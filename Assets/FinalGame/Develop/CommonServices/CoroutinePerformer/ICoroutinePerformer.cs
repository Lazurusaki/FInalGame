using System.Collections;
using UnityEngine;

namespace FinalGame.Develop.CommonServices.CoroutinePerformer
{
    public interface ICoroutinePerformer : IService
    {
        Coroutine StartPerform(IEnumerator coroutineFunction);

        void StopPerform(Coroutine coroutine);
    }
}
