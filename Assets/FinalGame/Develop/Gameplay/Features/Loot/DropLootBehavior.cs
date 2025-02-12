using FinalGame.Develop.Configs.Loot;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Conditions;
using FinalGame.Develop.Utils.Reactive;

namespace FinalGame.Develop.Gameplay.Features.Loot
{
    public class DropLootBehavior : IEntityInitialize, IEntityUpdate
    {
        private DropLootService _dropLootService;
        private ICompositeCondition _dropLootCondition;
        private ReactiveVariable<bool> _isLootDropped;
        private Entity _entity;

        public DropLootBehavior(DropLootService dropLootService)
        {
            _dropLootService = dropLootService;
        }

        public void OnInit(Entity entity)
        {
            _entity = entity;
            _dropLootCondition = entity.GetDropLootCondition();
            _isLootDropped = entity.GetIsLootDropped();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_dropLootCondition.Evaluate())
            {
                _dropLootService.DropLootFor(_entity);
                _isLootDropped.Value = true;
            }
        }
    }
}