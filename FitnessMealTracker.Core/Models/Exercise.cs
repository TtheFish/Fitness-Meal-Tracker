using System;

namespace FitnessMealTracker.Core.Models
{
    /// <summary>
    /// Represents an exercise activity with calories burned.
    /// </summary>
    public class Exercise
    {
        private const double MinimumCaloriesBurned = 0.0;

        private string _name = string.Empty;
        private double _caloriesBurned;

        /// <summary>
        /// Gets or sets the name of the exercise.
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
        }

        /// <summary>
        /// Gets or sets the calories burned during the exercise.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when value is negative.</exception>
        public double CaloriesBurned
        {
            get => _caloriesBurned;
            set
            {
                if (value < MinimumCaloriesBurned)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Calories burned cannot be negative.");
                }
                _caloriesBurned = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the Exercise class with default values.
        /// </summary>
        public Exercise()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Exercise class with specified values.
        /// </summary>
        /// <param name="name">The name of the exercise.</param>
        /// <param name="caloriesBurned">The calories burned during the exercise.</param>
        public Exercise(string name, double caloriesBurned)
        {
            Name = name;
            CaloriesBurned = caloriesBurned;
        }
    }
}
