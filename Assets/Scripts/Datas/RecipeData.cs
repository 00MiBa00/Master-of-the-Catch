using System;
using System.Collections.Generic;

namespace Datas
{
    [Serializable]
    public class RecipeData
    {
        public string Tile;
        public int IconIndex;
        public string Instructions;
        public List<string> Ingredients = new();
    }
}