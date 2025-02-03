using System;
using FinalGame.Develop.CommonServices.Results;
using FinalGame.Develop.Configs.Common.Results;
using FinalGame.Develop.Utils.Reactive;
using Unity.VisualScripting;

namespace FinalGame.Develop.CommonUI.Results
{
    public class GameResultCountPresenter : IInitializable, IDisposable
    {
        //model
        private readonly GameResults _gameResult;
        private readonly IReadOnlyVariable<int> _gameResultCount;
        private readonly ResultsIconsConfig _gameResultIconsConfig;
        
        //view
        public IconWithText View { get; }

        public GameResultCountPresenter(IReadOnlyVariable<int> gameResultCount, GameResults gameResult , IconWithText view, ResultsIconsConfig gameResultIconsConfig)
        {
            _gameResult = gameResult;
            _gameResultCount = gameResultCount;
            _gameResultIconsConfig = gameResultIconsConfig;
            View = view;
        }
        
        public void Initialize()
        {
            UpdateValue(_gameResultCount.Value);
            _gameResultCount.Changed += OnGameResultCountChanged;
            View.SetIcon(_gameResultIconsConfig.GetSpriteFor(_gameResult));
        }

        private void OnGameResultCountChanged(int oldValue, int newValue) => UpdateValue(newValue);
        
        public void Dispose()
        {
            _gameResultCount.Changed -= OnGameResultCountChanged;
        }

        private void UpdateValue(int value) => View.SetText(value.ToString());
    }
}