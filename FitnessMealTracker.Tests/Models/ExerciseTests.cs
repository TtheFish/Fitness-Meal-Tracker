using System;
using FitnessMealTracker.Core.Models;
using Xunit;

namespace FitnessMealTracker.Tests.Models
{
    /// <summary>
    /// Unit tests for the Exercise class.
    /// </summary>
    public class ExerciseTests
    {
        [Fact]
        public void Constructor_WithValidParameters_SetsProperties()
        {
            // Arrange
            const string name = "Running";
            const double caloriesBurned = 300.0;

            // Act
            Exercise exercise = new Exercise(name, caloriesBurned);

            // Assert
            Assert.Equal(name, exercise.Name);
            Assert.Equal(caloriesBurned, exercise.CaloriesBurned);
        }

        [Fact]
        public void Constructor_Default_InitializesWithDefaultValues()
        {
            // Act
            Exercise exercise = new Exercise();

            // Assert
            Assert.Equal(string.Empty, exercise.Name);
            Assert.Equal(0.0, exercise.CaloriesBurned);
        }

        [Fact]
        public void Name_SetToNull_ReturnsEmptyString()
        {
            // Arrange
            Exercise exercise = new Exercise();

            // Act
            exercise.Name = null!;

            // Assert
            Assert.Equal(string.Empty, exercise.Name);
        }

        [Fact]
        public void CaloriesBurned_SetToNegative_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            Exercise exercise = new Exercise();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => exercise.CaloriesBurned = -1.0);
        }

        [Fact]
        public void CaloriesBurned_SetToZero_Succeeds()
        {
            // Arrange
            Exercise exercise = new Exercise();

            // Act
            exercise.CaloriesBurned = 0.0;

            // Assert
            Assert.Equal(0.0, exercise.CaloriesBurned);
        }
    }
}

