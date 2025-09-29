using System.Collections.Generic;
using Models.Scenes;
using SO;
using Types;
using UnityEngine;
using ViewModels;
using Views.ChooseDish.Panels;
using Views.General;

namespace Controllers.Scenes
{
    public class ChooseDishSceneController : AbstractSceneController
    {
        [SerializeField] private List<RecipeCollectionSO> _recipeCollectionSos;
        [SerializeField] private List<Sprite> _iconSprites;
        
        [SerializeField] private MainPanel _mainPanel;
        [SerializeField] private AddDishPanel _addDishPanel;
        [SerializeField] private TextUpdater _descriptionView;
        [SerializeField] private BgView _bgView;
        
        private ChooseDishSceneModel _model;
        
        protected override void OnSceneEnable()
        {
        }

        protected override void OnSceneStart()
        {
            OpenMenuPanel();
        }

        protected override void OnSceneDisable()
        {
            _model.SaveToJson();
        }

        protected override void Initialize()
        {
            _model = new ChooseDishSceneModel();
        }

        protected override void Subscribe()
        {
            
        }

        protected override void Unsubscribe()
        {
            
        }

        private void SetDescription()
        {
            RecipeCollectionSO collection = GetCurrentCollection();
            
            _bgView.SetSprite(collection.BgSprite);
            
            _descriptionView.SetText(collection.Description);
        }

        private void SetDishes()
        {
            RecipeCollectionSO collection = GetCurrentCollection();

            List<DishPreview> previews = _model.GetRecipeDishes(collection.Recipes, _iconSprites, (int)collection.Category);
            
            _mainPanel.SetDishesInfo(previews);
        }

        private RecipeCollectionSO GetCurrentCollection()
        {
            RecipeType type = _model.GetSelectedRecipe;

            return _recipeCollectionSos.Find(x => x.Category == type);
        }

        private void OpenMenuPanel()
        {
            SetDishes();
            SetDescription();
            SubscribeMainPanel();
            OpenPanel(_mainPanel);
        }

        private void OpenAddDishPanel()
        {
            SubscribeAddDishPanel();
            OpenPanel(_addDishPanel);
        }

        private void OnReceiveAnswerMainPanel(int answer)
        {
            UnsubscribeMainPanel();

            switch (answer)
            {
                case 0:
                    base.LoadScene(SceneType.MenuScene);
                    break;
                case 1:
                    UnsubscribeMainPanel();
                    ClosePanel(_mainPanel);
                    OpenAddDishPanel();
                    break;
            }
        }

        private void OnReceiveAnswerAddDishPanel(int answer)
        {
            UnsubscribeAddDishPanel();
            
            ClosePanel(_addDishPanel);
            OpenMenuPanel();
        }

        private void SubscribeMainPanel()
        {
            _mainPanel.OnPressBtnAction += OnReceiveAnswerMainPanel;
            _mainPanel.OnSelectedDishAction += OnSelectedRecipe;
        }

        private void SubscribeAddDishPanel()
        {
            _addDishPanel.OnSetTileAction += OnSetTile;
            _addDishPanel.OnSetSpriteAction += OnSetIcon;
            _addDishPanel.OnSetIngredientsAction += OnSetIngredients;
            _addDishPanel.OnSetDirectionsActions += OnSetDirections;
            _addDishPanel.OnPressBtnAction += OnReceiveAnswerAddDishPanel;
        }

        private void UnsubscribeAddDishPanel()
        {
            _addDishPanel.OnSetTileAction -= OnSetTile;
            _addDishPanel.OnSetSpriteAction -= OnSetIcon;
            _addDishPanel.OnSetIngredientsAction -= OnSetIngredients;
            _addDishPanel.OnSetDirectionsActions -= OnSetDirections;
            _addDishPanel.OnPressBtnAction -= OnReceiveAnswerAddDishPanel;
        }

        private void UnsubscribeMainPanel()
        {
            _mainPanel.OnPressBtnAction -= OnReceiveAnswerMainPanel;
            _mainPanel.OnSelectedDishAction -= OnSelectedRecipe;
        }

        private void OnSetTile(string tile)
        {
            _model.SetTile(tile);
        }

        private void OnSetIcon(Sprite sprite)
        {
            int index = _iconSprites.IndexOf(sprite);
            
            _model.SetIconIndex(index);
        }

        private void OnSetIngredients(List<string> ingredients)
        {
            _model.SetIngredients(ingredients);
        }

        private void OnSetDirections(string instructions)
        {
            _model.SetInstructions(instructions);
            _model.SaveCurrentRecipe();
            
            UnsubscribeAddDishPanel();
            ClosePanel(_addDishPanel);
            OpenMenuPanel();
        }

        private void OnSelectedRecipe(int index)
        {
            _model.SetSelectedRecipe(index);
            
            base.LoadScene(SceneType.IngredientsScene);
        }

        private void OpenPanel(PanelView view)
        {
            view.Open();
        }

        private void ClosePanel(PanelView view)
        {
            view.Close();
        }
    }
}