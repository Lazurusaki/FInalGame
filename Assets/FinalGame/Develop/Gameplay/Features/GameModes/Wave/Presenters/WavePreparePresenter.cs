using System;
using System.Collections;
using FinalGame.Develop.CommonServices.CoroutinePerformer;
using FinalGame.Develop.CommonUI.Wallet;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.Features.Ability.Presenters;
using FinalGame.Develop.Gameplay.Features.GameModes.Wave.View;

namespace FinalGame.Develop.Gameplay.Features.GameModes.Wave.Presenters
{
    public class WavePreparePresenter : IInitializeable, IDisposable
    {
        public event Action Ready;
        
        private readonly WavePrepareView _view;
        private readonly WalletPresenter _walletPresenter;
        private readonly SelectAbilityWithPriceListPresenter _abilityListPresenter;
        private readonly ICoroutinePerformer _coroutinePerformer;
        
        public WavePreparePresenter(WavePrepareView view, WalletPresenter walletPresenter, SelectAbilityWithPriceListPresenter abilityListPresenter, ICoroutinePerformer coroutinePerformer)
        {
            _view = view;
            _walletPresenter = walletPresenter;
            _abilityListPresenter = abilityListPresenter;
            _coroutinePerformer = coroutinePerformer;
        }
        
        public void Enable()
        {
            _abilityListPresenter.Enable();
            _view.ReadyButtonClicked += OnReady;

            _coroutinePerformer.StartPerform(Show());
        }
        
        public void Disable()
        {
            _abilityListPresenter.Disable();
            _view.ReadyButtonClicked -= OnReady;
            
            _coroutinePerformer.StartPerform(Hide());
        }

        private IEnumerator Show()
        {
            _view.gameObject.SetActive(true);
            yield return _view.Show();

            _view.Subscribe();
            _abilityListPresenter.EnableSubscription();
        }
        
        private IEnumerator Hide()
        {
            yield return _view.Hide();
            _view.gameObject.SetActive(false);
            //callback?.Invoke();
            _view.Unsubscribe();
            _abilityListPresenter.DisableSubscription();
        }

        private void OnReady()
        {
            Ready?.Invoke();
        }
        
        public void Initialize()
        {
            _walletPresenter.Initialize();
        }
        
        public void Dispose()
        {
            _walletPresenter.Dispose();
        }
    }
}