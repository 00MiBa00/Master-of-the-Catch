using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Views.General;

namespace Views.ChooseDish
{
    public class IconBody : BodyAnimation
    {
        [Space(5)][Header("Body Settings")] 
        [SerializeField] private GameObject _iconItemPrefab;
        [SerializeField] private RectTransform _container;
        [SerializeField] private List<Sprite> _iconSprites;
        [SerializeField] private string _notificationText;
        [SerializeField] private Button _nextBtn;

        private List<IconItemView> _iconItemViews;
        private IconItemView _selectedItem;

        public event Action<Sprite, string> OnPressNextBtnAction;

        private void OnEnable()
        {
            SetIcons();
            
            _nextBtn.onClick.AddListener(OnPressNextBtn);
        }

        private void OnDisable()
        {
            _nextBtn.onClick.RemoveAllListeners();
            
            if (_iconItemViews is { Count: 0 })
            {
                return;
            }

            foreach (var itemView in _iconItemViews)
            {
                itemView.OnPressBtnAction -= OnSelectedIcon;
                Destroy(itemView.gameObject);
            }
            
            _iconItemViews.Clear();
        }

        private void SetIcons()
        {
            _iconItemViews ??= new List<IconItemView>();
            
            foreach (var sprite in _iconSprites)
            {
                GameObject go = Instantiate(_iconItemPrefab, _container);

                IconItemView view = go.GetComponent<IconItemView>();
                
                view.SetState(false);
                view.SetIcon(sprite);
                view.OnPressBtnAction += OnSelectedIcon;
                
                _iconItemViews.Add(view);
            }
        }

        private void OnSelectedIcon(IconItemView view)
        {
            foreach (var itemView in _iconItemViews)
            {
                itemView.SetState(false);
            }

            _selectedItem = view;
            view.SetState(true);
        }

        private void OnPressNextBtn()
        {
            if (_selectedItem == null)
            {
                OnPressNextBtnAction?.Invoke(null, _notificationText);
            }
            else
            {
                int index = _iconItemViews.IndexOf(_selectedItem);
                
                OnPressNextBtnAction?.Invoke(_iconSprites[index], string.Empty);
            }
        }
    }
}