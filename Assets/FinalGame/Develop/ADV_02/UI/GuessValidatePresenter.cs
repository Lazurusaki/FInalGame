using System;
using FinalGame.Develop.DI;
using UnityEngine;

namespace FinalGame.Develop.ADV_02.UI
{
    public class GuessValidatePresenter : IInitializeable, IDisposable
    {
        //model
        private readonly ISequenceGameMode _gameMode;
        
        //view
        private readonly InputFieldWithButton _view;

        public GuessValidatePresenter(ISequenceGameMode gameMode, InputFieldWithButton view)
        {
            _gameMode = gameMode;
            _view = view;
        }
        
        public void Initialize()
        {
            _view.GetButton.onClick.AddListener(OnGuessButtonClicked);
        }
        
        public void Dispose()
        {
            _view.GetButton.onClick.RemoveListener(OnGuessButtonClicked);
        }

        private void OnGuessButtonClicked()
        {
            if (_view.GetText != string.Empty)
                _gameMode.ValidateSequence(_view.GetText);
        }
    }
}