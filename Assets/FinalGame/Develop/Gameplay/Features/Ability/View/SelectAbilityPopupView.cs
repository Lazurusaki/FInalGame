using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Ability.View
{
    public class SelectAbilityPopupView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _selectAbilityText;

        [SerializeField] private SelectAbilityListView _abilityListView;

        public SelectAbilityListView AbilityListView => _abilityListView;
        
        public void SetTitle(string title) => _title.text = title;

        public void SetAdditionalText(string additionalText) => _selectAbilityText.text = additionalText;

        private Tween _animation;
        
        private void Awake()
        {
            _canvasGroup.alpha = 0;
        }

        public YieldInstruction Show()
        {
            _animation?.Kill();

            return StartCoroutine(ShowAnimation());
        }


        public YieldInstruction Hide()
        {
            _animation?.Kill();

            return StartCoroutine(HideAnimation());
        }
        
        private IEnumerator ShowAnimation()
        {
            _animation = _canvasGroup
                .DOFade(1, 0.2f)
                .From(0)
                .SetUpdate(true);

            yield return _animation.WaitForCompletion();
            yield return _abilityListView.Show();
        }

        private IEnumerator HideAnimation()
        {
            yield return _abilityListView.Hide();
            
            _animation = _canvasGroup
                .DOFade(0, 0.2f)
                .From(1)
                .SetUpdate(true);

            yield return _animation.WaitForCompletion();
        }
    }
}
