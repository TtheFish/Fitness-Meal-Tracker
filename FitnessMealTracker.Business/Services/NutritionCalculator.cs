using System;
using FitnessMealTracker.Core.Models;

namespace FitnessMealTracker.Business.Services
{
    public class NutritionCalculator : INutritionCalculator
    {
        /// Calculates the remaining calories needed to reach the daily goal.
        /// <exception cref="ArgumentNullException">Thrown when dailySummary or userGoal is null.</exception>
        public double CalculateRemainingCalories(DailySummary dailySummary, UserGoal userGoal)
        {
            ValidateArguments(dailySummary, userGoal);
            return userGoal.DailyCalorieGoal - dailySummary.TotalCalories;
        }

        /// Calculates the remaining protein needed to reach the daily goal.
        /// <exception cref="ArgumentNullException">Thrown when dailySummary or userGoal is null.</exception>
        public double CalculateRemainingProtein(DailySummary dailySummary, UserGoal userGoal)
        {
            ValidateArguments(dailySummary, userGoal);
            return userGoal.DailyProteinGoal - dailySummary.TotalProtein;
        }

        /// Calculates the remaining carbohydrates needed to reach the daily goal.
        /// <exception cref="ArgumentNullException">Thrown when dailySummary or userGoal is null.</exception>
        public double CalculateRemainingCarbs(DailySummary dailySummary, UserGoal userGoal)
        {
            ValidateArguments(dailySummary, userGoal);
            return userGoal.DailyCarbGoal - dailySummary.TotalCarbs;
        }

        /// Calculates the remaining fat needed to reach the daily goal.
        /// <exception cref="ArgumentNullException">Thrown when dailySummary or userGoal is null.</exception>
        public double CalculateRemainingFat(DailySummary dailySummary, UserGoal userGoal)
        {
            ValidateArguments(dailySummary, userGoal);
            return userGoal.DailyFatGoal - dailySummary.TotalFat;
        }

        private static void ValidateArguments(DailySummary dailySummary, UserGoal userGoal)
        {
            if (dailySummary == null)
            {
                throw new ArgumentNullException(nameof(dailySummary), "Daily summary cannot be null.");
            }
            if (userGoal == null)
            {
                throw new ArgumentNullException(nameof(userGoal), "User goal cannot be null.");
            }
        }
    }
}

