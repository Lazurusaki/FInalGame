using System;
using System.Collections.Generic;
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
        
        public ISequenceGameMode CreateGameMode(GameModes gameModeName)
        {
            ISequenceGameMode sequenceGameMode;
            
            switch (gameModeName)
            {
                case GameModes.Numbers:
                case GameModes.Letters:
                    sequenceGameMode =  new GuessValues(_gameModeValueTypes[gameModeName]);
                    break;
                
                default: throw new ArgumentException("Unknown game mode");
            }

            return sequenceGameMode;
        }
    }
}