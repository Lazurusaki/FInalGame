using System;
using FinalGame.Develop.Gameplay.Entities;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class EntityCharacterControllerRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private CharacterController _characterController;

        private void OnValidate()
        {
            _characterController ??= GetComponent<CharacterController>();
        }

        public override void Register(Entity entity)
        {
            entity.AddCharacterController(_characterController);
        }
    }
}