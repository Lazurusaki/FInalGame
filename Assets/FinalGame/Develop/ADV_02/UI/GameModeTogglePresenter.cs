using System;
using System.Collections.Generic;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay;

namespace FinalGame.Develop.ADV_02.UI
{
    public class GameModeTogglePresenter : IInitializeable, IDisposable
    {
        private readonly Dictionary<float, GameModes> SliderPositionValues = new() { { 0, GameModes.Numbers }, { 1, GameModes.Letters} }; 
    
        //model
        private readonly GameModeNameHandler _gameModeNameHandler;
    
        //view
        private readonly ToggleSwitchWithText _view;
        
        public GameModeTogglePresenter(GameModeNameHandler gameModeNameHandler, ToggleSwitchWithText view)
        {
            _gameModeNameHandler = gameModeNameHandler;
            _view = view;
        }
        
        public void Initialize()
        {
            _view.SetText(Positions.Left, Enum.GetValues(typeof(GameModes)).GetValue(0).ToString().ToUpper());
            _view.SetText(Positions.Right, Enum.GetValues(typeof(GameModes)).GetValue(1).ToString().ToUpper());
            _view.GetSlider.onValueChanged.AddListener(OnGameModeToggle);
        }
        
        public void Dispose() => _view.GetSlider.onValueChanged.RemoveListener(OnGameModeToggle);
        
        private void OnGameModeToggle(float value) => _gameModeNameHandler.GameMode = SliderPositionValues[value];
    }
}
