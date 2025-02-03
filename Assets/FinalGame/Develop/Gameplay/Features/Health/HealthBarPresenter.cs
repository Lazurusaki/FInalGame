using FinalGame.Develop.Gameplay.Entities;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Health
{
    public class HealthBarPresenter
    {
        private readonly Entity _entity;
        private readonly GradientBarWithText _view;
        
        public HealthBarPresenter(Entity entity, GradientBarWithText view)
        {
            _entity = entity;
            _view = view;
        }
        
        public void Enable()
        {
            _view.SetMaxValue(_entity.GetMaxHealth().Value);
            _view.SetValue(_entity.GetHealth().Value);
            
            _entity.GetHealth().Changed += OnHealthChanged;
            _entity.GetMaxHealth().Changed += OnMaxHealthChanged;
            
            _view.Show();
        }

        private void OnHealthChanged(float arg1, float newValue)
        {
            _view.SetValue(newValue);
        }

        private void OnMaxHealthChanged(float arg1, float newValue)
        {
            _view.SetMaxValue(newValue);
        }

        public void Disable()
        {
            _entity.GetHealth().Changed -= OnHealthChanged;
            _entity.GetMaxHealth().Changed -= OnMaxHealthChanged;
            _view.Hide();
        }
    }
}