using System;
using FitnessMealTracker.Core.Models;
using Xunit;

namespace FitnessMealTracker.Tests.Models
{
    /// <summary>
    /// Unit tests for the UserGoal class.
    /// </summary>
    public class UserGoalTests
    {
        [Fact]
        public void DailyCalorieGoal_SetToNegative_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            UserGoal goal = new UserGoal();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => goal.DailyCalorieGoal = -1.0);
        }

        [Fact]
        public void DailyProteinGoal_SetToNegative_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            UserGoal goal = new UserGoal();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => goal.DailyProteinGoal = -1.0);
        }

        [Fact]
        public void DailyCarbGoal_SetToNegative_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            UserGoal goal = new UserGoal();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => goal.DailyCarbGoal = -1.0);
        }

        [Fact]
        public void DailyFatGoal_SetToNegative_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            UserGoal goal = new UserGoal();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => goal.DailyFatGoal = -1.0);
        }

        [Fact]
        public void DailyCalorieGoal_SetToValidValue_Succeeds()
        {
            // Arrange
            UserGoal goal = new UserGoal();
            const double expectedValue = 2000.0;

            // Act
            goal.DailyCalorieGoal = expectedValue;

            // Assert
            Assert.Equal(expectedValue, goal.DailyCalorieGoal);
        }
    }
}

