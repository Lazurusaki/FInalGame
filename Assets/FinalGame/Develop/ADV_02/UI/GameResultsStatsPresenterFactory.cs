using FinalGame.Develop.CommonUI;
using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay;

namespace FinalGame.Develop.ADV_02.UI
{
    public class GameResultsStatsPresenterFactory
    {
        private readonly GameResultsStatsService _gameResultsStatsService;
        private readonly ConfigsProviderService _configsProviderService;
        
        public GameResultsStatsPresenterFactory(DIContainer container)
        {
            _gameResultsStatsService = container.Resolve<GameResultsStatsService>();
            _configsProviderService = container.Resolve<ConfigsProviderService>();
        }

        public GameResultsStatsPresenter CreateGameResultsStatsPresenter(IconsWithTextList view)
            => new GameResultsStatsPresenter (_gameResultsStatsService, view, this);

        public GameResultCountPresenter CreateGameResultsStatsPresenter(IconWithText view, GameResults gameResult)
            => new GameResultCountPresenter(_gameResultsStatsService.GetGameResultCount(gameResult), gameResult, view,
                _configsProviderService.GameResultIconsConfig);
    }
}