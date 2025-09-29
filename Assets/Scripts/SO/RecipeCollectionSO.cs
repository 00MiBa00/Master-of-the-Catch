using System.Collections.Generic;
using Types;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "FlockSeason/Recipes/Recipe Collection", fileName = "RecipeCollection")]
    public class RecipeCollectionSO : ScriptableObject
    {
        public RecipeType Category;
        public List<RecipeSO> Recipes = new();
        public string Description;
        public Sprite BgSprite;
    }
}