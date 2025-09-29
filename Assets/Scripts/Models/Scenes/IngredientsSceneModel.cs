using System.Collections.Generic;
using System.IO;
using System.Linq;
using SO;
using Types;
using UnityEngine;
using Utility;
using ViewModels;
using Wrappers;

namespace Models.Scenes
{
    public class IngredientsSceneModel
    {
        private RecipeWrapper _wrapper;
        public RecipeType GetSelectedRecipe => MealsUtility.SelectedMeal;

        public IngredientsSceneModel()
        {
            LoadFromJson(MealsUtility.SavedRecipeFileName);
        }

        public IngredientsPreview GetSelectedIngredient(List<RecipeSO> recipeSos)
        {
            int index = MealsUtility.SelectedRecipeIndex;

            List<IngredientsPreview> ingredients = new();
            ingredients.AddRange(GetRecipeSOToIngredientsPreviewConverter(recipeSos));
            ingredients.AddRange(GetJsonToIngredientsPreviewConverter());

            return ingredients[index];
        }

        private List<IngredientsPreview> GetRecipeSOToIngredientsPreviewConverter(List<RecipeSO> recipeSos)
        {
            return recipeSos
                .Select(recipe => new IngredientsPreview(recipe.Ingredients, recipe.Instructions, recipe.Title))
                .ToList();
        }

        private List<IngredientsPreview> GetJsonToIngredientsPreviewConverter()
        {
            var block = _wrapper.Categories.FirstOrDefault(c => c.Category == GetSelectedRecipe);
            if (block == null)
                return new List<IngredientsPreview>();

            return block.Recipes
                .Select(r => new IngredientsPreview(r.Ingredients, r.Instructions, r.Tile))
                .ToList();
        }
        
        private void LoadFromJson(string fileName)
        {
            string path = Path.Combine(Application.persistentDataPath, fileName);

            if (!File.Exists(path))
            {
#if UNITY_EDITOR
                Debug.LogWarning($"File not found: {path}");
#endif
                _wrapper = new RecipeWrapper();
                return;
            }

            string json = File.ReadAllText(path);
            _wrapper = JsonUtility.FromJson<RecipeWrapper>(json);
        }
    }
}