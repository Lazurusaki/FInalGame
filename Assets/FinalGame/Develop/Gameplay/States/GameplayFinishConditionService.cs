using FinalGame.Develop.Utils.Conditions;

namespace FinalGame.Develop.Gameplay.States
{
    public class GameplayFinishConditionService
    {
        public ICompositeCondition WinCondition { get; } = new CompositeCondition(LogicOperations.AndOperation);
        public ICompositeCondition LooseCondition { get; } = new CompositeCondition(LogicOperations.AndOperation);
    }
}