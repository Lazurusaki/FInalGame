using System;
using System.Collections;
using FinalGame.Develop.CommonUI;
using FinalGame.Develop.Gameplay.Features.Ability.View;
using UnityEngine;
using UnityEngine.UI;

namespace FinalGame.Develop.Gameplay.Features.GameModes.Wave.View
{
    public class WavePrepareView : MonoBehaviour
    {
        public event Action ReadyButtonClicked;
        
        [SerializeField] private Button _readyButton;
        [SerializeField] private SelectAbilityWithPriceListView _abilityListView;
        [SerializeField] private IconsWithTextList _walletView;

        public IconsWithTextList WalletView => _walletView;
        public SelectAbilityWithPriceListView AbilitiesListView => _abilityListView;
        
        public void Subscribe() => _readyButton.onClick.AddListener(OnClicked);

        public void Unsubscribe() => _readyButton.onClick.RemoveListener(OnClicked);

        public YieldInstruction Show()
        {
            return _abilityListView.Show();
        }
        
        
        public YieldInstruction Hide()
        {
            return _abilityListView.Hide();
        }
        
        
        private void OnClicked()
        {
            ReadyButtonClicked?.Invoke();
        }
    }
}