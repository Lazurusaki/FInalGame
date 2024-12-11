using System;
using System.Collections.Generic;

namespace FinalGame.Develop.Gameplay
{
    public class GameModeFactory
    {
        private readonly Dictionary<GameModes, ValueTypes> _gameModeValueTypes = new ()
        {
            { GameModes.Numbers, ValueTypes.Numbers },
            { GameModes.Letters, ValueTypes.Letters }
        };
        
        public IGameMode CreateGameMode(GameModes gameModeName)
        {
            IGameMode gameMode;
            
            switch (gameModeName)
            {
                case GameModes.Numbers:
                case GameModes.Letters:
                    gameMode =  new GuessValues(_gameModeValueTypes[gameModeName]);
                    break;
                
                default: throw new ArgumentException("Unknown game mode");
            }

            return gameMode;
        }
    }
}
