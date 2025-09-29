using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Views.General
{
    public class PulseAnimation : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private float _scale = 1.2f;
        [SerializeField] private float _duration = 0.6f;

        private Tween _pulseTween;

        private void OnEnable()
        {
            if (_image == null)
                _image = GetComponent<Image>();
            
            transform.localScale = Vector3.one;
            
            _pulseTween = transform
                .DOScale(_scale, _duration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDisable()
        {
            _pulseTween?.Kill();
            transform.localScale = Vector3.one;
        }
        
        
    }
}