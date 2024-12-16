using System;
using System.Collections.Generic;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Entities
{
    public class Entity : MonoBehaviour
    {
        private readonly Dictionary<EntityValues, object> _values = new();

        private readonly HashSet<IEntityBehavior> _behaviors = new();
        private readonly HashSet<IEntityUpdate> _updatables = new();
        private readonly HashSet<IEntityInitialize> _initializables = new();
        private readonly HashSet<IEntityDispose> _disposables = new();

        private bool _isInitialized;

        private void Awake()
        {
            Install();
        }

        private void Install()
        {
            var registrators = GetComponents<MonoEntityRegistrator>();

            if (registrators is null) return;
            
            foreach (var registrator in registrators)
                registrator.Register(this);
        }
        
        public void Initialize()
        {
            foreach (var initializable in _initializables)
                initializable.OnInit(this);

            _isInitialized = true;
        }
        
        private void Update()
        {
            if (_isInitialized == false)
                throw new InvalidOperationException("Entity is not initialized");
            
            foreach (var updatable in _updatables)
                updatable.OnUpdate(Time.deltaTime);
        }

        private void OnDestroy()
        {
            foreach (var disposable in _disposables)
                disposable.OnDispose();
        }

        public Entity AddValue<TValue>(EntityValues valueType, TValue value)
        {
            if (_values.ContainsKey(valueType))
                throw new ArgumentException(valueType.ToString());
            
            _values.Add(valueType, value);
            return this;
        }

        public bool TryGetValue<TValue>(EntityValues valueType, out TValue value)
        {
            if (_values.TryGetValue(valueType, out object findedObject))
            {
                if (findedObject is TValue findedValue)
                {
                    value = findedValue;
                    return true;
                }
            }

            value = default(TValue);
            return false;
        }

        public TValue GetValue<TValue>(EntityValues valueType)
        {
            if (TryGetValue(valueType, out TValue value) == false)
                throw new ArgumentException($"value not exist {valueType}");

            return value;
        }

        public Entity AddBehavior(IEntityBehavior behavior)
        {
            _behaviors.Add(behavior);

            if (behavior is IEntityUpdate updatable)
                _updatables.Add(updatable);

            if (behavior is IEntityInitialize initializable)
            {
                _initializables.Add(initializable);
                
                if (_isInitialized) 
                    initializable.OnInit(this);
            }

            if (behavior is IEntityDispose disposable)
                _disposables.Add(disposable);

            return this; 
        }
    }
}