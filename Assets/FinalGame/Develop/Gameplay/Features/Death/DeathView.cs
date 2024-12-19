using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Death
{
    [RequireComponent(typeof(Animator))]
    public class DeathView : EntityView
    {
        private readonly int IsDeadKey = Animator.StringToHash("IsDead");
        
        [SerializeField] private Animator _animator;

        private ReactiveVariable<bool> _isDead;
        private ReactiveVariable<bool> _isDeathProcess;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }
        
        protected override void OnEntityInitialized(Entity entity)
        {
            _isDead = entity.GetIsDead();
            _isDeathProcess = entity.GetIsDeathProcess();

            _isDead.Changed += OnIsDeadChanged;
        }
        
        protected override void OnEntityDisposed(Entity entity)
        {
            base.OnEntityDisposed(entity);
            
            _isDead.Changed -= OnIsDeadChanged;
        }

        public void OnDeathAnimationEnded() => _isDeathProcess.Value = false;

        private void OnIsDeadChanged(bool isDeadOld, bool isDead) => _animator.SetBool(IsDeadKey, isDead);
    }
}