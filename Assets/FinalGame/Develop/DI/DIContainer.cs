                                                                          using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace FinalGame.Develop.DI
{
    public class DIContainer
    {
        private readonly Dictionary<Type, Registration> _container = new();

        private readonly DIContainer _parent;

        private readonly List<Type> _requests = new();

        public DIContainer() : this(null)
        {
        }

        public DIContainer(DIContainer parent) => _parent = parent;

        public void RegisterAsSingle<T>(Func<DIContainer, T> factory)
        {
            if (_container.ContainsKey(typeof(T)))
                throw new InvalidOperationException($"{typeof(T)} already registered");

            var registration = new Registration(container => factory(container));
            _container[typeof(T)] = registration;
        }

        private T CreateFrom<T>(Registration registration)
        {
            if (registration.Instance == null && registration.Factory != null)
                registration.Instance = registration.Factory(this);

            return (T)registration.Instance;
        }

        public T Resolve<T>()
        {
            if (_requests.Contains(typeof(T)))
                throw new InvalidOperationException($"Cycle resolve for {typeof(T)}");

            _requests.Add(typeof(T));

            try
            {
                if (_container.TryGetValue(typeof(T), out var registration))
                    return CreateFrom<T>(registration);

                if (_parent != null)
                    return _parent.Resolve<T>();
            }
            finally
            {
                _requests.Remove(typeof(T));
            }

            throw new InvalidOperationException($"Registration for {typeof(T)} not found");
        }

        public class Registration
        {
            public Registration(Object instance) => Instance = instance;

            public Registration(Func<DIContainer, Object> factory) => Factory = factory;

            public Func<DIContainer, Object> Factory { get; }

            public Object Instance { get; set; }
        }
    }
}