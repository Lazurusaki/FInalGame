using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.Features.Enemy;
using FinalGame.Develop.Gameplay.Features.GameModes.Wave;

namespace FinalGame.Develop.Configs.Gameplay.Levels
{
    public class GameModesFactory
    {
        private DIContainer _container;

        public GameModesFactory(DIContainer container)
        {
            _container = container;
        }

        public WaveGameMode CreateWaveGameMode()
        {
            return new WaveGameMode(_container.Resolve<EnemyFactory>());
        }
    }
}