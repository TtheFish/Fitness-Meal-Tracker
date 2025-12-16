using System;
using System.Collections.Generic;
using System.Linq;
using FitnessMealTracker.Core.Models;
using FitnessMealTracker.Data.Repositories;

namespace FitnessMealTracker.Business.Services
{
    public class MealService : IMealService
    {
        private readonly IMealRepository _mealRepository;
        private readonly DailySummary _dailySummary;

        /// Initializes a new instance of the MealService class.
        /// <exception cref="ArgumentNullException">Thrown when mealRepository is null.</exception>
        public MealService(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository ?? throw new ArgumentNullException(nameof(mealRepository), "Meal repository cannot be null.");
            _dailySummary = new DailySummary();
        }

        /// Gets all meals for the current day.
        public IReadOnlyList<Meal> GetAllMeals()
        {
            return _dailySummary.Meals;
        }

        /// Gets the daily summary with nutritional totals.
        public DailySummary GetDailySummary()
        {
            return _dailySummary;
        }

        /// Adds a new meal.
        /// <exception cref="ArgumentNullException">Thrown when meal is null.</exception>
        public void AddMeal(Meal meal)
        {
            if (meal == null)
            {
                throw new ArgumentNullException(nameof(meal), "Meal cannot be null.");
            }
            _dailySummary.AddMeal(meal);
        }

        /// Removes a meal.
        public bool RemoveMeal(Meal meal)
        {
            return _dailySummary.RemoveMeal(meal);
        }

        /// Saves all meals to persistent storage.
        /// <exception cref="System.IO.IOException">Thrown when save operation fails.</exception>
        public void SaveMeals()
        {
            List<Meal> mealsList = _dailySummary.Meals.ToList();
            _mealRepository.SaveMeals(mealsList);
        }

        /// Loads all meals from persistent storage.
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

