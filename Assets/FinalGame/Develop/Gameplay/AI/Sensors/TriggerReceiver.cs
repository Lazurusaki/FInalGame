using System;
using System.Collections.Generic;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.AI.Sensors
{
    [RequireComponent(typeof(Collider))]
    public class TriggerReceiver : MonoBehaviour
    {
        [SerializeField] private List<Collider> _ignoredColliders;
        
        private readonly ReactiveEvent<Collider> _enter = new();
        private readonly ReactiveEvent<Collider> _exit = new();
        private readonly ReactiveEvent<Collider> _stay = new();

        public IReadonlyEvent<Collider> Enter => _enter;
        public IReadonlyEvent<Collider> Exit => _exit;
        public IReadonlyEvent<Collider> Stay => _stay;

        private void Awake()
        {
            Collider selfCollider = GetComponent<Collider>();

            foreach (var collider in _ignoredColliders)
                Physics.IgnoreCollision(selfCollider, collider);
        }

        private void OnTriggerEnter(Collider other)
        {
            _enter.Invoke(other);
        }
        
        private void OnTriggerExit(Collider other)
        {
            _exit.Invoke(other);
        }
        private void OnTriggerStay(Collider other)
        {
            _stay.Invoke(other);
        }
        
    }
}