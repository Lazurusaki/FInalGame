using System;
using FinalGame.Develop.ADV_02.Configs;
using FinalGame.Develop.CommonUI;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay;
using FinalGame.Develop.Utils.Reactive;
using Unity.VisualScripting;
using UnityEngine;

namespace FinalGame.Develop.ADV_02.Prefabs
{
    public class GameResultCountPresenter : IInitializeable, IDisposable
    {
        //model
        private readonly GameResults _gameResult;
        private readonly IReadOnlyVariable<int> _gameResultCount;
        private readonly GameResultIconsConfig _gameResultIconsConfig;
        
        //view
        public IconWithText View { get; }

        public GameResultCountPresenter(IReadOnlyVariable<int> gameResultCount, GameResults gameResult , IconWithText view, GameResultIconsConfig gameResultIconsConfig)
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