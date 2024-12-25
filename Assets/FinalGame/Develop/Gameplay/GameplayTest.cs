using System;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Enemy;
using FinalGame.Develop.Gameplay.Features.MainHero;
using UnityEngine;

namespace FinalGame.Develop.Gameplay
{
    public class GameplayTest : MonoBehaviour
    {
        private DIContainer _container;

        private Entity _mainHero;

        public void StartProcess(DIContainer container)
        {
            _container = container;
            _mainHero = _container.Resolve<MainHeroFactory>().Create(Vector3.zero );
            _container.Resolve<EnemyFactory>().CreateGhost(Vector3.zero + Vector3.forward * -2);
            _container.Resolve<EnemyFactory>().CreateGhost(Vector3.zero + Vector3.right * -2);
            _container.Resolve<EnemyFactory>().CreateGhost(Vector3.zero + Vector3.right * 2);
        }

        public void Update()
        {
          /*  
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("R");
                _mainHero.GetAttackTrigger()?.Invoke();
            }
            */
        }
    }
}