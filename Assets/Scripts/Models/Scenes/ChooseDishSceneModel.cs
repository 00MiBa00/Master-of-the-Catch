using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Datas;
using SO;
using Types;
using UnityEngine;
using Utility;
using ViewModels;
using Wrappers;

namespace Models.Scenes
{
    public class ChooseDishSceneModel
    {
        private RecipeData _currentRecipe;
        
        private RecipeWrapper _wrapper = new();

        public RecipeType GetSelectedRecipe => MealsUtility.SelectedMeal;

        public ChooseDishSceneModel()
        {
            LoadFromJson(MealsUtility.SavedRecipeFileName);
        }

        public void SetSelectedRecipe(int index)
        {
            MealsUtility.SelectedRecipeIndex = index;
        }

        public void SetTile(string tile)
        {
            _currentRecipe = new RecipeData();
            _currentRecipe.Tile = tile;
        }

        public void SetIconIndex(int index)
        {
            if (_currentRecipe == null) return;
            _currentRecipe.IconIndex = index;
        }

        public void SetIngredients(List<string> ingredients)
        {
            _currentRecipe.Ingredients = new List<string>(ingredients);
        }

        public void SetInstructions(string instructions)
        {
            _currentRecipe.Instructions = instructions;
        }

        public void SaveCurrentRecipe()
        {
            var categoryBlock = _wrapper.Categories.Find(c => c.Category == GetSelectedRecipe);
            if (categoryBlock == null)
            {
                categoryBlock = new CategoryBlock { Category = GetSelectedRecipe };
                _wrapper.Categories.Add(categoryBlock);
            }

            categoryBlock.Recipes.Add(_currentRecipe);
            _currentRecipe = null;
        }

        public void SaveToJson()
        {
            string path = Path.Combine(Application.persistentDataPath, MealsUtility.SavedRecipeFileName);
            string json = JsonUtility.ToJson(_wrapper, true);
            File.WriteAllText(path, json);
        }

        public List<DishPreview> GetRecipeDishes(List<RecipeSO> recipeSos, List<Sprite> iconSprites, int index)
        {
            List<DishPreview> dishes = new List<DishPreview>();
            
            dishes.AddRange(GetRecipeSOToDishPreviewConverter(recipeSos, index));
            dishes.AddRange(GetJsonToDishPreviewConverter(iconSprites, index));

            return dishes;
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

        private List<DishPreview> GetRecipeSOToDishPreviewConverter(List<RecipeSO> recipeSos, int index)
        {
            return recipeSos
                .Select(recipe => new DishPreview(recipe.Title, recipe.Icon, index))
                .ToList();
        }

        private List<DishPreview> GetJsonToDishPreviewConverter(List<Sprite> iconSprites, int index)
        {
            var block = _wrapper.Categories.FirstOrDefault(c => c.Category == GetSelectedRecipe);
            if (block == null)
                return new List<DishPreview>();

            return block.Recipes
                .Select(r => new DishPreview(r.Tile, iconSprites[r.IconIndex], index))
                .ToList();
        }
    }
}