using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Attack
{
    [RequireComponent(typeof(Animator))]
    public class InstantAttackView : EntityView
    {
        private readonly int IsAttackKey = Animator.StringToHash("IsAttack");
        
        [SerializeField] private Animator _animator;

        private ReactiveVariable<bool> _isAttackProcess;
        private ReactiveEvent _instantAttackEvent;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        protected override void OnEntityInitialized(Entity entity)
        {
            _isAttackProcess = entity.GetIsAttackProcess();
            _instantAttackEvent = entity.GetInstantAttackEvent();
            
            _isAttackProcess.Changed += OnAttackProcessChanged;
        }

        protected override void OnEntityDisposed(Entity entity)
        {
            base.OnEntityDisposed(entity);

            _isAttackProcess.Changed -= OnAttackProcessChanged;
        }

        public void OnAttackAnimationEnded()
        {
            _isAttackProcess.Value = false;
        }

        public void OnInstantAttack()
        {
            if (_isAttackProcess.Value)
                _instantAttackEvent.Invoke();
        }

        private void OnAttackProcessChanged(bool arg1, bool isAttack)
        {
                _animator.SetBool(IsAttackKey,isAttack);
        }
    }
}