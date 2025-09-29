using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Views.General;

namespace Views.ChooseDish
{
    public class TileBody : BodyAnimation
    {
        [Space(5)][Header("Body Settings")]
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _nextBtn;
        [SerializeField] private string _notificationText;

        public event Action<bool,string> OnPressNextBtnAction;

        private void OnEnable()
        {
            _nextBtn.onClick.AddListener(OnPressNextBtn);
        }

        private void OnDisable()
        {
            _nextBtn.onClick.RemoveAllListeners();
            
            _inputField.text = string.Empty;
        }

        private void OnPressNextBtn()
        {
            bool canPressNextBtn = !string.IsNullOrWhiteSpace(_inputField.text);

            string value = canPressNextBtn ? _inputField.text : _notificationText;

            OnPressNextBtnAction?.Invoke(canPressNextBtn,value);
        }
    }
}