using FinalGame.Develop.CommonUI;
using FinalGame.Develop.Gameplay.Entities;

namespace FinalGame.Develop.Gameplay.Features.Damage.Presenters
{
    public class DamageBlinkPresenter
    {
        private readonly Entity _entity;
        private readonly CanvasBlink _view;
        
        public DamageBlinkPresenter(Entity entity, CanvasBlink view)
        {
            _entity = entity;
            _view = view;
        }
        
        public void Enable()
        {
            _entity.GetHealth().Changed += OnHealthChanged;
        }

        private void OnHealthChanged(float oldValue, float newValue)
        {
            if (newValue < oldValue)
                _view.Activate();
        }
        
        public void Disable()
        {
            _entity.GetHealth().Changed -= OnHealthChanged;
        }
    }
}