using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Views.General
{
    public class IngredientsView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _ingredientsText;

        private void OnDisable()
        {
            _ingredientsText.text = string.Empty;
        }

        public void SetIngredients(List<string> ingredients)
        {
            if (_ingredientsText == null)
            {
                return;
            }

            _ingredientsText.text = string.Empty;

            foreach (var ingredient in ingredients)
            {
                _ingredientsText.text += $"• {ingredient}\n";
            }
        }
    }
}