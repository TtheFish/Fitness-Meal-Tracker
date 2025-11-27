using System;
using System.Collections.Generic;
using System.Linq;

namespace FitnessMealTracker.Core.Models
{
    /// <summary>
    /// Represents a daily summary of meals and their nutritional totals.
    /// </summary>
    public class DailySummary
    {
        private readonly List<Meal> _meals = new List<Meal>();

        /// <summary>
        /// Gets a read-only collection of meals for the day.
        /// </summary>
        public IReadOnlyList<Meal> Meals => _meals.AsReadOnly();

        /// <summary>
        /// Gets the total calories consumed from all meals.
        /// </summary>
        public double TotalCalories => _meals.Sum(meal => meal.TotalCalories);

        /// <summary>
        /// Gets the total protein consumed from all meals.
        /// </summary>
        public double TotalProtein => _meals.Sum(meal => meal.TotalProtein);

        /// <summary>
        /// Gets the total carbohydrates consumed from all meals.
        /// </summary>
        public double TotalCarbs => _meals.Sum(meal => meal.TotalCarbs);

        /// <summary>
        /// Gets the total fat consumed from all meals.
        /// </summary>
        public double TotalFat => _meals.Sum(meal => meal.TotalFat);

        /// <summary>
        /// Adds a meal to the daily summary.
        /// </summary>
        /// <param name="meal">The meal to add.</param>
        /// <exception cref="ArgumentNullException">Thrown when meal is null.</exception>
        public void AddMeal(Meal meal)
        {
            if (meal == null)
            {
                throw new ArgumentNullException(nameof(meal), "Meal cannot be null.");
            }
            _meals.Add(meal);
        }

        /// <summary>
        /// Removes a meal from the daily summary.
        /// </summary>
        /// <param name="meal">The meal to remove.</param>
        /// <returns>True if the meal was removed; otherwise, false.</returns>
        public bool RemoveMeal(Meal meal)
        {
            return meal != null && _meals.Remove(meal);
        }

        /// <summary>
        /// Clears all meals from the daily summary.
        /// </summary>
        public void ClearMeals()
        {
            _meals.Clear();
        }
    }
}
