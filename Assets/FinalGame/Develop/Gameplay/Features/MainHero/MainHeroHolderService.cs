using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.MainHero
{
    public class MainHeroHolderService : IDisposable
    {
        private ReactiveEvent<Entity> _heroRegistered = new();
        private ReactiveEvent<Entity> _heroUnregistered = new();

        private Entity _mainHero;

        public IReadonlyEvent<Entity> HeroRegistered => _heroRegistered;
        public IReadonlyEvent<Entity> HeroUnregistered => _heroUnregistered;
        
        public Entity MainHero => _mainHero;

        public void Dispose()
        {
            _mainHero = null;
        }
        
        public void Register(Entity mainHero)
        {
            if (_mainHero is not null)
                throw new ArgumentException("Main hero already registered");

            _mainHero = mainHero;
            
            _heroRegistered?.Invoke(mainHero);
        }

        public void Unregister()
        {
            if (_mainHero is null)
                throw new ArgumentException("No registered main hero");
            
            _heroUnregistered?.Invoke(_mainHero);
            _mainHero = null;
        }
    }
}