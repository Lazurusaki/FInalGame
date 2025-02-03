using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Damage.View
{
    public class StructureDamageView : EntityView
    {
        private const float ShowDamage1Value = 2; // HP 30% -
        private const float ShowDamage2Value = 4; // HP 30% -

        [SerializeField] private ParticleSystem _damageEffect1;
        [SerializeField] private ParticleSystem _damageEffect2;
        
        private IReadOnlyVariable<float> _maxHealth;
        private IReadOnlyVariable<float> _health;

        private bool _isInitialized;
        
        protected override void OnEntityInitialized(Entity entity)
        {
            _maxHealth = entity.GetMaxHealth();
            _health = entity.GetHealth();
            _damageEffect1.Stop();
            _damageEffect2.Stop();
            
            _isInitialized = true;
        }

        private void Update()
        {
            
            if (_isInitialized == false)
                return;

            if (_health.Value < _maxHealth.Value / ShowDamage2Value)
            {
                if (_damageEffect2.isPlaying == false)
                    _damageEffect2.Play();
            }
            else if (_damageEffect2.isPlaying)
                _damageEffect2.Stop();
            
            
            if (_health.Value < _maxHealth.Value / ShowDamage1Value)
            {
                if (_damageEffect1.isPlaying == false)
                    _damageEffect1.Play();
            }
            else if (_damageEffect1.isPlaying)
                _damageEffect1.Stop();
        }
    }
}