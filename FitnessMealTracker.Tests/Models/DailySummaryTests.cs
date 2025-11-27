using System;
using FitnessMealTracker.Core.Models;
using Xunit;

namespace FitnessMealTracker.Tests.Models
{
    /// <summary>
    /// Unit tests for the DailySummary class.
    /// </summary>
    public class DailySummaryTests
    {
        [Fact]
        public void AddMeal_WithValidMeal_AddsMealToSummary()
        {
            // Arrange
            DailySummary summary = new DailySummary();
            Meal meal = new Meal("Breakfast");

            // Act
            summary.AddMeal(meal);

            // Assert
            Assert.Single(summary.Meals);
            Assert.Contains(meal, summary.Meals);
        }

        [Fact]
        public void AddMeal_WithNullMeal_ThrowsArgumentNullException()
        {
            // Arrange
            DailySummary summary = new DailySummary();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => summary.AddMeal(null!));
        }

        [Fact]
        public void RemoveMeal_WithExistingMeal_ReturnsTrue()
        {
            // Arrange
            DailySummary summary = new DailySummary();
            Meal meal = new Meal("Lunch");
            summary.AddMeal(meal);

            // Act
            bool result = summary.RemoveMeal(meal);

            // Assert
            Assert.True(result);
            Assert.Empty(summary.Meals);
        }

        [Fact]
        public void RemoveMeal_WithNonExistingMeal_ReturnsFalse()
        {
            // Arrange
            DailySummary summary = new DailySummary();
            Meal meal = new Meal("Dinner");

            // Act
            bool result = summary.RemoveMeal(meal);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void RemoveMeal_WithNullMeal_ReturnsFalse()
        {
            // Arrange
            DailySummary summary = new DailySummary();

            // Act
            bool result = summary.RemoveMeal(null!);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ClearMeals_RemovesAllMeals()
        {
            // Arrange
            DailySummary summary = new DailySummary();
            summary.AddMeal(new Meal("Breakfast"));
            summary.AddMeal(new Meal("Lunch"));

            // Act
            summary.ClearMeals();

            // Assert
            Assert.Empty(summary.Meals);
        }

        [Fact]
        public void TotalCalories_WithMultipleMeals_ReturnsSum()
        {
            // Arrange
            DailySummary summary = new DailySummary();
            Meal meal1 = new Meal("Breakfast");
            meal1.AddFoodItem(new FoodItem("Egg", 70.0, 6.0, 0.5, 5.0));
            Meal meal2 = new Meal("Lunch");
            meal2.AddFoodItem(new FoodItem("Chicken", 200.0, 30.0, 0.0, 8.0));
            summary.AddMeal(meal1);
            summary.AddMeal(meal2);

            // Act
            double totalCalories = summary.TotalCalories;

            // Assert
            Assert.Equal(270.0, totalCalories);
        }

        [Fact]
        public void TotalProtein_WithMultipleMeals_ReturnsSum()
        {
            // Arrange
            DailySummary summary = new DailySummary();
            Meal meal1 = new Meal("Breakfast");
            meal1.AddFoodItem(new FoodItem("Egg", 70.0, 6.0, 0.5, 5.0));
            Meal meal2 = new Meal("Lunch");
            meal2.AddFoodItem(new FoodItem("Chicken", 200.0, 30.0, 0.0, 8.0));
            summary.AddMeal(meal1);
            summary.AddMeal(meal2);

            // Act
            double totalProtein = summary.TotalProtein;

            // Assert
            Assert.Equal(36.0, totalProtein);
        }
    }
}

