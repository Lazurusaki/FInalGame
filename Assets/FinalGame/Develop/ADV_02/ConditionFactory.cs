using System;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay;

namespace FinalGame.Develop.ADV_02
{
    public class ConditionFactory
    {
        private readonly DIContainer _container;
        
        public ConditionFactory(DIContainer container) => _container = container;

        public ICondition CreateCondition(EndGameConditions conditionName)
        {
            ICondition condition;
            
            switch (conditionName)
            {
                case EndGameConditions.ValuesGuessed:
                    condition = new ValuesGuessed(_container.Resolve<GameModeHandler>().GameMode);
                    break;
                case EndGameConditions.Mistake:
                    condition = new Mistake(_container.Resolve<GameModeHandler>().GameMode);
                    break;
                
                default: throw new ArgumentException("Unknown condition.", nameof(conditionName));
            }

            return condition;
        }
    }
}