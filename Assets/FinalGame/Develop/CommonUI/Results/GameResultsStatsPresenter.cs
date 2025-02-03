using System;
using System.Collections.Generic;
using FinalGame.Develop.CommonServices.Results;
using FinalGame.Develop.DI;
using Unity.VisualScripting;
using UnityEngine;

namespace FinalGame.Develop.CommonUI.Results
{
    public class GameResultsStatsPresenter :IInitializeable, IDisposable
    {
        //Model
        private readonly GameResultsStatsService _gameResultsStatsService;
        private readonly GameResultsStatsPresenterFactory _factory;
        private readonly List<GameResultCountPresenter> _gameResultCountPresenters = new();
        
        //view
        private readonly IconsWithTextList _view;
        
        public GameResultsStatsPresenter(GameResultsStatsService gameResultsStatsService, IconsWithTextList view, GameResultsStatsPresenterFactory factory)
        {
            _gameResultsStatsService = gameResultsStatsService;
            _factory = factory;
            _view = view;
        }
        
        public void Initialize()
        {
            SpawnGameResultCountView(GameResults.Win);
            SpawnGameResultCountView(GameResults.Loose);
        }
        
        public void Dispose()
        {
            
        }
        
        private void SpawnGameResultCountView(GameResults gameResult)
        {
            var gameResultsCountView = _view.SpawnElement();
            var gameResultCountPresenter = _factory.CreateGameResultsCountPresenter(gameResultsCountView, gameResult);
            gameResultCountPresenter.Initialize();
            _gameResultCountPresenters.Add(gameResultCountPresenter);
        }
    }
}