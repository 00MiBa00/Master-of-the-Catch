using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ViewModels;

namespace Views.ChooseDish
{
    public class DishItemView : MonoBehaviour
    {
        [SerializeField] private Button _btn;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _bgImage;
        [SerializeField] private List<Sprite> _bgSprites;
        [SerializeField] private TextMeshProUGUI _name;

        public event Action<DishItemView> OnPressBtnAction;

        private void OnEnable()
        {
            _btn.onClick.AddListener(OnPressBtn);
        }

        private void OnDisable()
        {
            _btn.onClick.RemoveAllListeners();
        }

        public void SetInfo(DishPreview preview)
        {
            _icon.sprite = preview.Icon;
            _name.text = preview.Title;
            _bgImage.sprite = _bgSprites[preview.BgSpriteIndex];
        }

        private void OnPressBtn()
        {
            OnPressBtnAction?.Invoke(this);
        }
    }
}