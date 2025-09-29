using UnityEngine;

namespace ViewModels
{
    public class DishPreview
    {
        public string Title { get; private set; }
        public Sprite Icon { get; private set; }
        public int BgSpriteIndex { get; private set; }

        public DishPreview(string title, Sprite icon, int index)
        {
            Title = title;
            Icon = icon;
            BgSpriteIndex = index;
        }
    }
}