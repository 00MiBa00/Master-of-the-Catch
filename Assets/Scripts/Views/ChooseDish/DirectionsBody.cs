using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Views.General;

namespace Views.ChooseDish
{
    public class DirectionsBody : BodyAnimation
    {
        [Space(5)][Header("Body Settings")]
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _addButton;
        [SerializeField] private Button _nextBtn;
        [SerializeField] private string _notificationText;
        [SerializeField] private TextUpdater _resultBody;

        private string _directionText;

        public event Action<bool, string> OnPressNextBtnAction;

        private void OnEnable()
        {
            _directionText = string.Empty;
            
            _inputField.onEndEdit.AddListener(OnEndEditInputField);
            _addButton.onClick.AddListener(OnPressAddBtn);
            _nextBtn.onClick.AddListener(OnPressNextBtn);
        }

        private void OnDisable()
        {
            _inputField.onEndEdit.RemoveAllListeners();
            _addButton.onClick.RemoveAllListeners();
            _nextBtn.onClick.RemoveAllListeners();
            
            ClearInputField();
        }

        private void OnEndEditInputField(string input)
        {
            _addButton.interactable = !string.IsNullOrWhiteSpace(input);
        }

        private void OnPressAddBtn()
        {
            _addButton.interactable = false;
            
            if (string.IsNullOrEmpty(_directionText))
            {
                _directionText = _inputField.text;
            }
            else
            {
                _directionText += " " + _inputField.text;
            }
            
            _resultBody.SetText(_directionText);
            
            ClearInputField();
        }

        private void OnPressNextBtn()
        {
            bool isDirectionCompleted = !string.IsNullOrWhiteSpace(_directionText);
            string text = isDirectionCompleted ? _directionText : _notificationText;
            
            OnPressNextBtnAction?.Invoke(isDirectionCompleted, text);
        }

        private void ClearInputField()
        {
            _inputField.text = string.Empty;
        }
    }
}