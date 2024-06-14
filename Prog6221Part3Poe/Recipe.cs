using System.Collections.Generic;

namespace Prog6221Part3Poe
{
    internal class Recipe
    {
        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
        public string FoodGroup { get; set; }
        public int Calories { get; set; }
    }
}