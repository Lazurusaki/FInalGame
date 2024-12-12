using System;
using System.Collections.Generic;
using FinalGame.Develop.ADV_02;

namespace FinalGame.Develop.Gameplay
{
    public class GameModeConsoleFactory
    {
        private readonly Dictionary<GameModes, ValueTypes> _gameModeValueTypes = new ()
        {
            { GameModes.Numbers, ValueTypes.Numbers },
            { GameModes.Letters, ValueTypes.Letters }
        };
        
        public IGameModeConsole CreateGameMode(GameModes gameModeName)
        {
            IGameModeConsole gameModeConsole;
            
            switch (gameModeName)
            {
                case GameModes.Numbers:
                case GameModes.Letters:
                    gameModeConsole =  new GuessValuesConsole(_gameModeValueTypes[gameModeName]);
                    break;
                
                default: throw new ArgumentException("Unknown game mode");
            }

            return gameModeConsole;
        }
    }
}
