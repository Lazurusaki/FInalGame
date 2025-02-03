using DG.Tweening;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Aim
{
    public class AimView : MonoBehaviour
    {
        [SerializeField] private RectTransform _aimRoot;
        [SerializeField] private RectTransform _aimInner;
        [SerializeField] private RectTransform _aimOuter;

        private Sequence _aimAnimationInner;
        private Sequence _aimAnimationOuter;
        private Sequence _interactAnimation;

        private void Awake()
        {
            _aimAnimationInner = DOTween.Sequence()
                .Append(_aimInner.DOLocalRotate(Vector3.forward * 360, 4f, RotateMode.FastBeyond360)
                    .SetEase(Ease.Linear))
                .SetLoops(-1, LoopType.Incremental);

            _aimAnimationOuter = DOTween.Sequence()
                .Append(_aimOuter.DOLocalRotate(Vector3.back * 360, 4f, RotateMode.FastBeyond360)
                    .SetEase(Ease.Linear))
                .SetLoops(-1, LoopType.Incremental);
        }
        

        public void Show()
        {
            if (_aimRoot.gameObject.activeSelf == false)
                _aimRoot.gameObject.SetActive(true);

            _aimAnimationInner.Play();
            _aimAnimationOuter.Play();
        }

        public void Hide()
        {
            _aimAnimationInner.Pause();
            _aimAnimationOuter.Pause();
            
            if (_aimRoot.gameObject.activeSelf)
                _aimRoot.gameObject.SetActive(false);
        }

        public void Interact()
        {
            _interactAnimation = DOTween.Sequence();
            
            _interactAnimation
                .Append(_aimInner.DOScale(2f, 0.05f))
                .Append(_aimInner.DOScale(1, 0.3f));
        }
    }
}