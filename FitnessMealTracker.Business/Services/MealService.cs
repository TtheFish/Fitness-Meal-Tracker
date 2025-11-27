using System;
using System.Collections.Generic;
using System.Linq;
using FitnessMealTracker.Core.Models;
using FitnessMealTracker.Data.Repositories;

namespace FitnessMealTracker.Business.Services
{
    /// <summary>
    /// Provides business logic for meal management operations.
    /// </summary>
    public class MealService : IMealService
    {
        private readonly IMealRepository _mealRepository;
        private readonly DailySummary _dailySummary;

        /// <summary>
        /// Initializes a new instance of the MealService class.
        /// </summary>
        /// <param name="mealRepository">The repository for meal data persistence.</param>
        /// <exception cref="ArgumentNullException">Thrown when mealRepository is null.</exception>
        public MealService(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository ?? throw new ArgumentNullException(nameof(mealRepository), "Meal repository cannot be null.");
            _dailySummary = new DailySummary();
        }

        /// <summary>
        /// Gets all meals for the current day.
        /// </summary>
        /// <returns>A read-only list of meals.</returns>
        public IReadOnlyList<Meal> GetAllMeals()
        {
            return _dailySummary.Meals;
        }

        /// <summary>
        /// Gets the daily summary with nutritional totals.
        /// </summary>
        /// <returns>The daily summary containing all meals and totals.</returns>
        public DailySummary GetDailySummary()
        {
            return _dailySummary;
        }

        /// <summary>
        /// Adds a new meal.
        /// </summary>
        /// <param name="meal">The meal to add.</param>
        /// <exception cref="ArgumentNullException">Thrown when meal is null.</exception>
        public void AddMeal(Meal meal)
        {
            if (meal == null)
            {
                throw new ArgumentNullException(nameof(meal), "Meal cannot be null.");
            }
            _dailySummary.AddMeal(meal);
        }

        /// <summary>
        /// Removes a meal.
        /// </summary>
        /// <param name="meal">The meal to remove.</param>
        /// <returns>True if the meal was removed; otherwise, false.</returns>
        public bool RemoveMeal(Meal meal)
        {
            return _dailySummary.RemoveMeal(meal);
        }

        /// <summary>
        /// Saves all meals to persistent storage.
        /// </summary>
        /// <exception cref="System.IO.IOException">Thrown when save operation fails.</exception>
        public void SaveMeals()
        {
            List<Meal> mealsList = _dailySummary.Meals.ToList();
            _mealRepository.SaveMeals(mealsList);
        }

        /// <summary>
        /// Loads all meals from persistent storage.
        /// </summary>
        /// <exception cref="System.IO.IOException">Thrown when load operation fails.</exception>
        public void LoadMeals()
        {
            IReadOnlyList<Meal> loadedMeals = _mealRepository.LoadMeals();
            _dailySummary.ClearMeals();
            
            foreach (Meal meal in loadedMeals)
            {
                _dailySummary.AddMeal(meal);
            }
        }
    }
}

