using System.Collections.Generic;

namespace ViewModels
{
    public class IngredientsPreview
    {
        public string Tile { get; private set; }
        public string Directions { get; private set; }
        public List<string> Ingredients { get; private set; }

        public IngredientsPreview(List<string> ingredients, string directions, string tile)
        {
            Tile = tile;
            Ingredients = new(ingredients);
            Directions = directions;
        }
    }
}