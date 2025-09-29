using UnityEngine;
using DG.Tweening;

namespace Views.General
{
    public class BodyAnimation : MonoBehaviour
    {
        [Header("Animation settings")]
        [SerializeField]
        private float _duration = 0.5f;
        
        private RectTransform _rectTransform;
        private Vector2 _centerPos;
        private Vector2 _rightOffPos;
        private Vector2 _leftOffPos;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            
            _centerPos = _rectTransform.anchoredPosition;
            
            float screenWidth = Screen.width;

            _rightOffPos = new Vector2(screenWidth * 1.2f, _centerPos.y);
            _leftOffPos  = new Vector2(-screenWidth * 1.2f, _centerPos.y);
            
            _rectTransform.anchoredPosition = _rightOffPos;
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
            
            _rectTransform
                .DOAnchorPos(_centerPos, _duration)
                .SetEase(Ease.OutQuad);
        }

        public void Hide()
        {
            _rectTransform
                .DOAnchorPos(_leftOffPos, _duration)
                .SetEase(Ease.OutQuad).OnComplete(OnCompletedHide);
        }

        private void OnCompletedHide()
        {
            _rectTransform.anchoredPosition = _centerPos;
            
            gameObject.SetActive(false);
        }
    }
}