using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "FlockSeason/Recipes/Recipe", fileName = "NewRecipe")]
    public class RecipeSO : ScriptableObject
    {
        public string Title;
        public Sprite Icon;
        [TextArea] public string Instructions;
        public List<string> Ingredients = new();
    }
}