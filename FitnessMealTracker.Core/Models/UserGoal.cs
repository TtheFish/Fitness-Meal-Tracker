using System;

namespace FitnessMealTracker.Core.Models
{
    /// <summary>
    /// Represents user's daily nutritional goals.
    /// </summary>
    public class UserGoal
    {
        private const double MinimumGoalValue = 0.0;

        private double _dailyCalorieGoal;
        private double _dailyProteinGoal;
        private double _dailyCarbGoal;
        private double _dailyFatGoal;

        /// <summary>
        /// Gets or sets the daily calorie goal.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when value is negative.</exception>
        public double DailyCalorieGoal
        {
            get => _dailyCalorieGoal;
            set
            {
                if (value < MinimumGoalValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Daily calorie goal cannot be negative.");
                }
                _dailyCalorieGoal = value;
            }
        }

        /// <summary>
        /// Gets or sets the daily protein goal in grams.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when value is negative.</exception>
        public double DailyProteinGoal
        {
            get => _dailyProteinGoal;
            set
            {
                if (value < MinimumGoalValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Daily protein goal cannot be negative.");
                }
                _dailyProteinGoal = value;
            }
        }

        /// <summary>
        /// Gets or sets the daily carbohydrate goal in grams.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when value is negative.</exception>
        public double DailyCarbGoal
        {
            get => _dailyCarbGoal;
            set
            {
                if (value < MinimumGoalValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Daily carb goal cannot be negative.");
                }
                _dailyCarbGoal = value;
            }
        }

        /// <summary>
        /// Gets or sets the daily fat goal in grams.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when value is negative.</exception>
        public double DailyFatGoal
        {
            get => _dailyFatGoal;
            set
            {
                if (value < MinimumGoalValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Daily fat goal cannot be negative.");
                }
                _dailyFatGoal = value;
            }
        }
    }
}
