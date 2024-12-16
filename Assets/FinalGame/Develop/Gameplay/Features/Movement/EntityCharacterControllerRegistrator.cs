using FinalGame.Develop.Gameplay.Entities;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Movement
{
    public class EntityCharacterControllerRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private CharacterController _characterController;
        
        public override void Register(Entity entity)
        {
            entity.AddCharacterController(_characterController);
        }
    }
}