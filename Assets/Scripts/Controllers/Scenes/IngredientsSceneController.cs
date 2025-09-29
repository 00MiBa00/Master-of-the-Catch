using System.Collections.Generic;
using Models.Scenes;
using SO;
using Types;
using UnityEngine;
using ViewModels;
using Views.General;
using Views.Ingredients;

namespace Controllers.Scenes
{
    public class IngredientsSceneController : AbstractSceneController
    {
        [SerializeField] private List<RecipeCollectionSO> _recipeCollectionSos;
        [SerializeField] private MainPanel _mainPanel;
        [SerializeField] private BgView _bgView;
        
        private IngredientsSceneModel _model;
        
        protected override void OnSceneEnable()
        {
            OpenMainPanel();
        }

        protected override void OnSceneStart()
        {
            
        }

        protected override void OnSceneDisable()
        {
            
        }

        protected override void Initialize()
        {
            _model = new IngredientsSceneModel();
        }

        protected override void Subscribe()
        {
            
        }

        protected override void Unsubscribe()
        {
            
        }

        private void OnReceiveAnswerMainPanel(int answer)
        {
            _mainPanel.OnPressBtnAction -= OnReceiveAnswerMainPanel;
            
            _mainPanel.Close();
            
            base.LoadScene(SceneType.ChooseDishScene);
        }

        private void OpenMainPanel()
        {
            RecipeCollectionSO collection = GetCurrentCollection();
            
            _bgView.SetSprite(collection.BgSprite);
            
            IngredientsPreview preview = _model.GetSelectedIngredient(collection.Recipes);
            
            _mainPanel.SetInfo(preview);

            _mainPanel.OnPressBtnAction += OnReceiveAnswerMainPanel;
            _mainPanel.Open();
        }

        private RecipeCollectionSO GetCurrentCollection()
        {
            RecipeType type = _model.GetSelectedRecipe;

            return _recipeCollectionSos.Find(x => x.Category == type);
        }
    }
}