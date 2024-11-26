using System;
using FinalGame.Develop.DI;

namespace FinalGame.Develop.Gameplay
{
    public class GameModeFabric
    {
        public IGameMode CreateGameMode(GameModes gameModeName)
        {
            IGameMode gameMode;
            
            switch (gameModeName)
            {
                case GameModes.Numbers:
                case GameModes.Letters:
                    gameMode =  new GuessValues(gameModeName);
                    break;
                
                default: throw new ArgumentException("Unknown game mode");
            }

            return gameMode;
        }
    }
}
