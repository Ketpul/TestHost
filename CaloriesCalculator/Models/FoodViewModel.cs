using CaloriesCalculator.Models;
using System.Collections.Generic;

namespace CaloriesCalculator.ViewModels
{
    public class FoodViewModel
    {
        public List<Food> Suggestions { get; set; }
        public List<SelectedFood> SelectedFoods { get; set; }
        public double TotalCalories { get; set; }

    }
}
