using System.Collections.Generic;
using FitnessMealTracker.Core.Models;

namespace FitnessMealTracker.Business.Services
{
    public interface IMealService
    {
        /// Gets all meals for the current day.
        IReadOnlyList<Meal> GetAllMeals();

        /// Gets the daily summary with nutritional totals.
        DailySummary GetDailySummary();

        /// Adds a new meal.
        /// <exception cref="System.ArgumentNullException">Thrown when meal is null.</exception>
        void AddMeal(Meal meal);

        /// Removes a meal.
        bool RemoveMeal(Meal meal);

        /// Saves all meals to persistent storage.
        /// <exception cref="System.IO.IOException">Thrown when save operation fails.</exception>
        void SaveMeals();

        /// Loads all meals from persistent storage.
        /// <exception cref="System.IO.IOException">Thrown when load operation fails.</exception>
        void LoadMeals();
    }
}

