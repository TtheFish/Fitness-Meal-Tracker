using System;

namespace FitnessMealTracker.Core.Models
{
    public class UserGoal
    {
        private const double MinimumGoalValue = 0.0;

        private double _dailyCalorieGoal;
        private double _dailyProteinGoal;
        private double _dailyCarbGoal;
        private double _dailyFatGoal;

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
