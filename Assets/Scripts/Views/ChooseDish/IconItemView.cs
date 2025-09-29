using System;
using UnityEngine;
using UnityEngine.UI;

namespace Views.ChooseDish
{
    public class IconItemView : MonoBehaviour
    {
        [SerializeField] private Button _btn;
        [SerializeField] private Image _bgIcon;
        [SerializeField] private Image _mainIcon;

        public event Action<IconItemView> OnPressBtnAction;

        private void OnEnable()
        {
            _btn.onClick.AddListener(OnPressBtn);
        }

        private void OnDisable()
        {
            _btn.onClick.RemoveAllListeners();
        }

        public void SetIcon(Sprite sprite)
        {
            _mainIcon.sprite = sprite;
        }

        public void SetState(bool value)
        {
            _bgIcon.enabled = value;
            _btn.interactable = !value;
        }

        private void OnPressBtn()
        {
            OnPressBtnAction?.Invoke(this);
        }
    }
}