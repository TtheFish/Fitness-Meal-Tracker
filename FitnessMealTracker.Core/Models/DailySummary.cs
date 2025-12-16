using System;
using System.Collections.Generic;
using System.Linq;

namespace FitnessMealTracker.Core.Models
{
    public class DailySummary
    {
        private readonly List<Meal> _meals = new List<Meal>();

        public IReadOnlyList<Meal> Meals => _meals.AsReadOnly();

        public double TotalCalories => _meals.Sum(meal => meal.TotalCalories);

        public double TotalProtein => _meals.Sum(meal => meal.TotalProtein);

        public double TotalCarbs => _meals.Sum(meal => meal.TotalCarbs);

        public double TotalFat => _meals.Sum(meal => meal.TotalFat);

        /// Adds a meal to the daily summary.
        /// <exception cref="ArgumentNullException">Thrown when meal is null.</exception>
        public void AddMeal(Meal meal)
        {
            if (meal == null)
            {
                throw new ArgumentNullException(nameof(meal), "Meal cannot be null.");
            }
            _meals.Add(meal);
        }

        /// Removes a meal from the daily summary.
        public bool RemoveMeal(Meal meal)
        {
            return meal != null && _meals.Remove(meal);
        }

        /// Clears all meals from the daily summary.
        public void ClearMeals()
        {
            _meals.Clear();
        }
    }
}
