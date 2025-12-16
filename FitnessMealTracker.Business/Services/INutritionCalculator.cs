using FitnessMealTracker.Core.Models;

namespace FitnessMealTracker.Business.Services
{
    public interface INutritionCalculator
    {
        /// Calculates the remaining calories needed to reach the daily goal.
        double CalculateRemainingCalories(DailySummary dailySummary, UserGoal userGoal);

        /// Calculates the remaining protein needed to reach the daily goal.
        double CalculateRemainingProtein(DailySummary dailySummary, UserGoal userGoal);

        /// Calculates the remaining carbohydrates needed to reach the daily goal.
        double CalculateRemainingCarbs(DailySummary dailySummary, UserGoal userGoal);

        /// Calculates the remaining fat needed to reach the daily goal.
        double CalculateRemainingFat(DailySummary dailySummary, UserGoal userGoal);
    }
}

