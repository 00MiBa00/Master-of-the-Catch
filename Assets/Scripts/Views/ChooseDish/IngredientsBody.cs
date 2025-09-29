using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Views.General;

namespace Views.ChooseDish
{
    public class IngredientsBody : BodyAnimation
    {
        [Space(5)][Header("Body Settings")]
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private string _notificationText;
        [SerializeField] private Button _addbtn;
        [SerializeField] private Button _nextBtn;
        [SerializeField] private IngredientsView _resultBody;

        private List<string> _ingredients;

        public event Action<List<string>, string> OnPressNextBtnAction; 

        private void OnEnable()
        {
            _addbtn.interactable = false;
            _addbtn.onClick.AddListener(OnPressAddBtn);
            _nextBtn.onClick.AddListener(OnPressNextBtn);

            _ingredients = new();
            
            _inputField.onEndEdit.AddListener(OnEndEditInputField);
        }

        private void OnDisable()
        {
            ClearInputField();
            _addbtn.onClick.RemoveAllListeners();
            _nextBtn.onClick.RemoveAllListeners();
            
            _inputField.onEndEdit.RemoveAllListeners();
        }

        private void OnEndEditInputField(string input)
        {
            _addbtn.interactable = !string.IsNullOrWhiteSpace(input);
        }

        private void ClearInputField()
        {
            _inputField.text = string.Empty;
        }

        private void OnPressAddBtn()
        {
            _ingredients.Add(_inputField.text);
            ClearInputField();
            _addbtn.interactable = false;
            _resultBody.SetIngredients(_ingredients);
        }

        private void OnPressNextBtn()
        {
            if (_ingredients is { Count: 0 })
            {
                OnPressNextBtnAction?.Invoke(null, _notificationText);
            }
            else
            {
                OnPressNextBtnAction?.Invoke(_ingredients, string.Empty);
            }
        }
    }
}