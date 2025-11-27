using System;
using FitnessMealTracker.Core.Models;

namespace FitnessMealTracker.Business.Services
{
    /// <summary>
    /// Provides calculations for nutrition tracking and goal progress.
    /// </summary>
    public class NutritionCalculator : INutritionCalculator
    {
        /// <summary>
        /// Calculates the remaining calories needed to reach the daily goal.
        /// </summary>
        /// <param name="dailySummary">The current daily summary.</param>
        /// <param name="userGoal">The user's daily goals.</param>
        /// <returns>The remaining calories. Negative if over the goal.</returns>
        /// <exception cref="ArgumentNullException">Thrown when dailySummary or userGoal is null.</exception>
        public double CalculateRemainingCalories(DailySummary dailySummary, UserGoal userGoal)
        {
            ValidateArguments(dailySummary, userGoal);
            return userGoal.DailyCalorieGoal - dailySummary.TotalCalories;
        }

        /// <summary>
        /// Calculates the remaining protein needed to reach the daily goal.
        /// </summary>
        /// <param name="dailySummary">The current daily summary.</param>
        /// <param name="userGoal">The user's daily goals.</param>
        /// <returns>The remaining protein in grams. Negative if over the goal.</returns>
        /// <exception cref="ArgumentNullException">Thrown when dailySummary or userGoal is null.</exception>
        public double CalculateRemainingProtein(DailySummary dailySummary, UserGoal userGoal)
        {
            ValidateArguments(dailySummary, userGoal);
            return userGoal.DailyProteinGoal - dailySummary.TotalProtein;
        }

        /// <summary>
        /// Calculates the remaining carbohydrates needed to reach the daily goal.
        /// </summary>
        /// <param name="dailySummary">The current daily summary.</param>
        /// <param name="userGoal">The user's daily goals.</param>
        /// <returns>The remaining carbs in grams. Negative if over the goal.</returns>
        /// <exception cref="ArgumentNullException">Thrown when dailySummary or userGoal is null.</exception>
        public double CalculateRemainingCarbs(DailySummary dailySummary, UserGoal userGoal)
        {
            ValidateArguments(dailySummary, userGoal);
            return userGoal.DailyCarbGoal - dailySummary.TotalCarbs;
        }

        /// <summary>
        /// Calculates the remaining fat needed to reach the daily goal.
        /// </summary>
        /// <param name="dailySummary">The current daily summary.</param>
        /// <param name="userGoal">The user's daily goals.</param>
        /// <returns>The remaining fat in grams. Negative if over the goal.</returns>
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

