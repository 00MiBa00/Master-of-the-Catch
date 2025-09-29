using Types;
using UnityEngine;

namespace Utility
{
    public static class MealsUtility
    {
        private const string SelectedMealKey = "MealsUtility.SelectedMeal";
        private const string SelectedRecipeKey = "MealsUtility.SelectedRecipe";
        private const string SavedRecipeFileNameKey = "recipes.json";

        public static string SavedRecipeFileName => SavedRecipeFileNameKey;
        
        public static RecipeType SelectedMeal
        {
            get => (RecipeType)PlayerPrefs.GetInt(SelectedMealKey, 0);
            set => PlayerPrefs.SetInt(SelectedMealKey, (int)value);
        }
        
        public static int SelectedRecipeIndex
        {
            get => PlayerPrefs.GetInt(SelectedRecipeKey, 0);
            set => PlayerPrefs.SetInt(SelectedRecipeKey, value);
        }
    }
}