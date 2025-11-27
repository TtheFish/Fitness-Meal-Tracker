using FitnessMealTracker.Core.Models;

namespace FitnessMealTracker.Business.Services
{
    /// <summary>
    /// Defines the contract for nutrition calculation operations.
    /// </summary>
    public interface INutritionCalculator
    {
        /// <summary>
        /// Calculates the remaining calories needed to reach the daily goal.
        /// </summary>
        /// <param name="dailySummary">The current daily summary.</param>
        /// <param name="userGoal">The user's daily goals.</param>
        /// <returns>The remaining calories. Negative if over the goal.</returns>
        double CalculateRemainingCalories(DailySummary dailySummary, UserGoal userGoal);

        /// <summary>
        /// Calculates the remaining protein needed to reach the daily goal.
        /// </summary>
        /// <param name="dailySummary">The current daily summary.</param>
        /// <param name="userGoal">The user's daily goals.</param>
        /// <returns>The remaining protein in grams. Negative if over the goal.</returns>
        double CalculateRemainingProtein(DailySummary dailySummary, UserGoal userGoal);

        /// <summary>
        /// Calculates the remaining carbohydrates needed to reach the daily goal.
        /// </summary>
        /// <param name="dailySummary">The current daily summary.</param>
        /// <param name="userGoal">The user's daily goals.</param>
        /// <returns>The remaining carbs in grams. Negative if over the goal.</returns>
        double CalculateRemainingCarbs(DailySummary dailySummary, UserGoal userGoal);

        /// <summary>
        /// Calculates the remaining fat needed to reach the daily goal.
        /// </summary>
        /// <param name="dailySummary">The current daily summary.</param>
        /// <param name="userGoal">The user's daily goals.</param>
        /// <returns>The remaining fat in grams. Negative if over the goal.</returns>
        double CalculateRemainingFat(DailySummary dailySummary, UserGoal userGoal);
    }
}

