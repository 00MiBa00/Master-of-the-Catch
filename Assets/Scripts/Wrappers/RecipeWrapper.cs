using System;
using System.Collections.Generic;
using Datas;

namespace Wrappers
{
    [Serializable]
    public class RecipeWrapper
    {
        public List<CategoryBlock> Categories = new();
    }
}