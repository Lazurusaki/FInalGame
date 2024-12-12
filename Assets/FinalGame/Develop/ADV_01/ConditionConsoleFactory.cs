using System;
using FinalGame.Develop.DI;

namespace FinalGame.Develop.Gameplay
{
    public class ConditionConsoleFactory
    {
        private readonly DIContainer _container;
        
        public ConditionConsoleFactory(DIContainer container) => _container = container;

        public ICondition CreateCondition(EndGameConditions conditionName)
        {
            ICondition condition;
            
            switch (conditionName)
            {
                case EndGameConditions.ValuesGuessed:
                    condition = new ValuesGuessedConsole(_container.Resolve<GameModeConsoleHandler>().GameModeConsole);
                    break;
                case EndGameConditions.Mistake:
                    condition = new MistakeConsole(_container.Resolve<GameModeConsoleHandler>().GameModeConsole);
                    break;
                
                default: throw new ArgumentException("Unknown condition.", nameof(conditionName));
            }

            return condition;
        }
    }
}
