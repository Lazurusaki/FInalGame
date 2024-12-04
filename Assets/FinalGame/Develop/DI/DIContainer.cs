using System;
using System.Collections.Generic;
using Object = System.Object;

namespace FinalGame.Develop.DI
{
    public class DIContainer : IDisposable
    {
        private readonly Dictionary<Type, Registration> _container = new();

        private readonly DIContainer _parent;

        private readonly List<Type> _requests = new();

        public DIContainer() : this(null)
        {
        }

        public DIContainer(DIContainer parent) => _parent = parent;

        public Registration RegisterAsSingle<T>(Func<DIContainer, T> factory)
        {
            if (IsAlreadyRegistered<T>())
                throw new InvalidOperationException($"{typeof(T)} already registered");

            var registration = new Registration(container => factory(container));
            _container[typeof(T)] = registration;

            return registration;
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

                if (_parent is not null)
                    return _parent.Resolve<T>();
            }
            finally
            {
                _requests.Remove(typeof(T));
            }

            throw new InvalidOperationException($"Registration for {typeof(T)} not found");
        }

        public void Initialize()
        {
            foreach (Registration registration in _container.Values)
            {
                if (registration.Instance is null && registration.IsNonLazy)
                    registration.Instance = registration.Factory(this);

                if (registration.Instance is not null)
                    if (registration.Instance is IInitializeable initializeable)
                        initializeable.Initialize();
            }
        }
        
        public void Dispose()
        {
            foreach (Registration registration in _container.Values)
            {
                if (registration.Instance is not null)
                    if (registration.Instance is IDisposable disposable)
                     disposable.Dispose();
            }
        }
        
        private T CreateFrom<T>(Registration registration)
        {
            if (registration.Instance == null && registration.Factory != null)
                registration.Instance = registration.Factory(this);

            return (T)registration.Instance;
        }

        public bool IsAlreadyRegistered<T>()
        {
            if (_container.ContainsKey(typeof(T)))
                return true;

            return _parent is not null && _parent.IsAlreadyRegistered<T>();
        }
        
        public class Registration
        {
            public Func<DIContainer, Object> Factory { get; }
            
            public Object Instance { get; set; }
            public bool IsNonLazy { get; private set; }
            
            public Registration(Object instance) => Instance = instance;
            
            public Registration(Func<DIContainer, Object> factory) => Factory = factory;

            public void NonLazy() => IsNonLazy = true;
        }

        
    }
}