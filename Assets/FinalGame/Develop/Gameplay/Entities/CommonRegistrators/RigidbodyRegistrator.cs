using UnityEngine;

namespace FinalGame.Develop.Gameplay.Entities.CommonRegistrators
{
    public class RigidbodyRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private Rigidbody _rigidbody;

        private void OnValidate()
        {
            _rigidbody ??= GetComponent<Rigidbody>();
        }

        public override void Register(Entity entity)
        {
            entity.AddRigidbody(_rigidbody);
        }
    }
}