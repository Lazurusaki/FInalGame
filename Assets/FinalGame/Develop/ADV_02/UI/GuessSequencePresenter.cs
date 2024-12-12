using FinalGame.Develop.DI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace FinalGame.Develop.ADV_02.UI
{
    public class GuessSequencePresenter : IInitializeable
    {
        //model
        private readonly ISequenceGameMode _gameMode;
        
        //view
        private readonly TMP_Text _view;

        public GuessSequencePresenter(ISequenceGameMode gameMode, TMP_Text view)
        {
            _gameMode = gameMode;
            _view = view;
        }

        public void Initialize()
        {
            _view.text = _gameMode.GetSequence();
        }
    }
}