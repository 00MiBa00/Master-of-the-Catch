using System;
using System.Collections.Generic;
using Types;

namespace Datas
{
    [Serializable]
    public class CategoryBlock
    {
        public RecipeType Category;
        public List<RecipeData> Recipes = new();
    }
}