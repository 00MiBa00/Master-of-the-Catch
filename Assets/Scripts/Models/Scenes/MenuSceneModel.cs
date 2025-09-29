using Types;
using Utility;

namespace Models.Scenes
{
    public class MenuSceneModel
    {
        public void SetMeal(int index)
        {
            MealsUtility.SelectedMeal = (RecipeType)index;
        }
    }
}