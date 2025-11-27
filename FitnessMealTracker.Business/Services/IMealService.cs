using System.Collections.Generic;
using FitnessMealTracker.Core.Models;

namespace FitnessMealTracker.Business.Services
{
    /// <summary>
    /// Defines the contract for meal management operations.
    /// </summary>
    public interface IMealService
    {
        /// <summary>
        /// Gets all meals for the current day.
        /// </summary>
        /// <returns>A read-only list of meals.</returns>
        IReadOnlyList<Meal> GetAllMeals();

        /// <summary>
        /// Gets the daily summary with nutritional totals.
        /// </summary>
        /// <returns>The daily summary containing all meals and totals.</returns>
        DailySummary GetDailySummary();

        /// <summary>
        /// Adds a new meal.
        /// </summary>
        /// <param name="meal">The meal to add.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when meal is null.</exception>
        void AddMeal(Meal meal);

        /// <summary>
        /// Removes a meal.
        /// </summary>
        /// <param name="meal">The meal to remove.</param>
        /// <returns>True if the meal was removed; otherwise, false.</returns>
        bool RemoveMeal(Meal meal);

        /// <summary>
        /// Saves all meals to persistent storage.
        /// </summary>
        /// <exception cref="System.IO.IOException">Thrown when save operation fails.</exception>
        void SaveMeals();

        /// <summary>
        /// Loads all meals from persistent storage.
        /// </summary>
        /// <exception cref="System.IO.IOException">Thrown when load operation fails.</exception>
        void LoadMeals();
    }
}

