using System.Collections.Generic;
using UnityEngine;
using ViewModels;
using Views.General;

namespace Views.Ingredients
{
    public class MainPanel : PanelView
    {
        [SerializeField] private TextUpdater _descriptionView;
        [SerializeField] private TextUpdater _directionsView;
        [SerializeField] private IngredientsView _ingredientsView;
        
        protected override void OnPanelEnable()
        {
            
        }

        protected override void OnPanelDisable()
        {
            
        }
        
        public void SetInfo(IngredientsPreview preview)
        {
            SetDescription(preview.Tile);
            SetIngredients(preview.Ingredients);
            SetDirections(preview.Directions);
        }
        
        private void SetDescription(string tile)
        {
            _descriptionView.SetText(tile);
        }

        private void SetIngredients(List<string> ingredients)
        {
            _ingredientsView.SetIngredients(ingredients);
        }

        private void SetDirections(string directions)
        {
            _directionsView.SetText(directions);
        }
    }
}