using System;
using System.Collections.Generic;
using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay;

namespace FinalGame.Develop.ADV_02
{
    public class GameModeFactory
    {
        private readonly Dictionary<GameModes, ValueTypes> _gameModeValueTypes = new ()
        {
            { GameModes.Numbers, ValueTypes.Numbers },
            { GameModes.Letters, ValueTypes.Letters }
        };
        
        public ISequenceGameMode CreateGameMode(DIContainer container, GameModes gameModeName)
        {
            ISequenceGameMode sequenceGameMode;
            
            switch (gameModeName)
            {
                case GameModes.Numbers:
                case GameModes.Letters:
                    sequenceGameMode =  new GuessValues(
                        container.Resolve<ConfigsProviderService>().GameModesConfig.GetSymbolsFor(gameModeName),
                        container.Resolve<ConfigsProviderService>().GameModesConfig.GetSymbolsCount(gameModeName)
                        );
                    break;
                
                default: throw new ArgumentException("Unknown game mode");
            }

            return sequenceGameMode;
        }
    }
}