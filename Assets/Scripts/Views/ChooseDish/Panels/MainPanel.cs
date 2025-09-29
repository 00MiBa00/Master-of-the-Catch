using System;
using System.Collections.Generic;
using UnityEngine;
using ViewModels;
using Views.General;

namespace Views.ChooseDish.Panels
{
    public class MainPanel : PanelView
    {
        [SerializeField] private GameObject _dishItemPrefab;
        [SerializeField] private RectTransform _container;

        private List<DishItemView> _dishItemViews;

        public event Action<int> OnSelectedDishAction;
        
        protected override void OnPanelEnable()
        {
            
        }

        protected override void OnPanelDisable()
        {
            foreach (var view in _dishItemViews)
            {
                view.OnPressBtnAction -= OnPressDishBtn;
            }
        }

        public void SetDishesInfo(List<DishPreview> dishPreviews)
        {
            TryClearDishItemViews();
            
            for (int i = 0; i < dishPreviews.Count; i++)
            {
                GameObject go = Instantiate(_dishItemPrefab, _container);
                DishItemView view = go.GetComponent<DishItemView>();
                
                view.SetInfo(dishPreviews[i]);
                view.OnPressBtnAction += OnPressDishBtn;
                
                _dishItemViews.Add(view);
            }
        }

        private void TryClearDishItemViews()
        {
            if (_dishItemViews is { Count: >0 })
            {
                foreach (var view in _dishItemViews)
                {
                    Destroy(view.gameObject);
                }
                
                _dishItemViews.Clear();
            }
            else
            {
                _dishItemViews ??= new List<DishItemView>();
            }
        }

        private void OnPressDishBtn(DishItemView view)
        {
            int index = _dishItemViews.IndexOf(view);
            
            OnSelectedDishAction?.Invoke(index);
        }
    }
}