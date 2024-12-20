using System.Collections;
using FinalGame.Develop.CommonServices.CoroutinePerformer;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.CommonServices.Timer
{
    public class TimerService
    {
        private ReactiveVariable<float> _cooldown;

        private ReactiveEvent _cooldownEnded;

        private float _currentTime;

        private ICoroutinePerformer _coroutinePerformer;
        private Coroutine _cooldownProcess;

        public TimerService(float cooldown, ICoroutinePerformer coroutinePerformer)
        {
            _cooldown = new ReactiveVariable<float>(cooldown);

            _cooldownEnded = new ReactiveEvent();
            _coroutinePerformer = coroutinePerformer;
        }

        public IReadonlyEvent CooldownEnded => _cooldownEnded;

        public float CurrentTime => _currentTime;
        public bool IsOver => _currentTime <= 0;

        public void Stop()
        {
            if (_cooldownProcess is not null)
                _coroutinePerformer.StopPerform(_cooldownProcess);
        }
        
        public void Restart()
        {
            Stop();

            _cooldownProcess = _coroutinePerformer.StartPerform(CooldownProcess());
        }

        private IEnumerator CooldownProcess()
        {
            _currentTime = _cooldown.Value;
            
            while (IsOver == false)
            {
                _currentTime -= Time.deltaTime;
                yield return null;
            }
            
            _cooldownEnded.Invoke();
        }
    }
}