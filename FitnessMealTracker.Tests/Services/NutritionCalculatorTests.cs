using System;
using FitnessMealTracker.Business.Services;
using FitnessMealTracker.Core.Models;
using Xunit;

namespace FitnessMealTracker.Tests.Services
{
    /// <summary>
    /// Unit tests for the NutritionCalculator class.
    /// </summary>
    public class NutritionCalculatorTests
    {
        [Fact]
        public void CalculateRemainingCalories_WithValidInputs_ReturnsCorrectValue()
        {
            // Arrange
            NutritionCalculator calculator = new NutritionCalculator();
            DailySummary summary = new DailySummary();
            Meal meal = new Meal("Breakfast");
            meal.AddFoodItem(new FoodItem("Egg", 200.0, 15.0, 1.0, 14.0));
            summary.AddMeal(meal);
            
            UserGoal goal = new UserGoal
            {
                DailyCalorieGoal = 2000.0
            };

            // Act
            double remaining = calculator.CalculateRemainingCalories(summary, goal);

            // Assert
            Assert.Equal(1800.0, remaining);
        }

        [Fact]
        public void CalculateRemainingCalories_WithNullSummary_ThrowsArgumentNullException()
        {
            // Arrange
            NutritionCalculator calculator = new NutritionCalculator();
            UserGoal goal = new UserGoal();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => calculator.CalculateRemainingCalories(null!, goal));
        }

        [Fact]
        public void CalculateRemainingCalories_WithNullGoal_ThrowsArgumentNullException()
        {
            // Arrange
            NutritionCalculator calculator = new NutritionCalculator();
            DailySummary summary = new DailySummary();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => calculator.CalculateRemainingCalories(summary, null!));
        }

        [Fact]
        public void CalculateRemainingProtein_WithValidInputs_ReturnsCorrectValue()
        {
            // Arrange
            NutritionCalculator calculator = new NutritionCalculator();
            DailySummary summary = new DailySummary();
            Meal meal = new Meal("Lunch");
            meal.AddFoodItem(new FoodItem("Chicken", 200.0, 30.0, 0.0, 8.0));
            summary.AddMeal(meal);
            
            UserGoal goal = new UserGoal
            {
                DailyProteinGoal = 150.0
            };

            // Act
            double remaining = calculator.CalculateRemainingProtein(summary, goal);

            // Assert
            Assert.Equal(120.0, remaining);
        }

        [Fact]
        public void CalculateRemainingCarbs_WithValidInputs_ReturnsCorrectValue()
        {
            // Arrange
            NutritionCalculator calculator = new NutritionCalculator();
            DailySummary summary = new DailySummary();
            Meal meal = new Meal("Dinner");
            meal.AddFoodItem(new FoodItem("Rice", 200.0, 4.0, 45.0, 0.5));
            summary.AddMeal(meal);
            
            UserGoal goal = new UserGoal
            {
                DailyCarbGoal = 250.0
            };

            // Act
            double remaining = calculator.CalculateRemainingCarbs(summary, goal);

            // Assert
            Assert.Equal(205.0, remaining);
        }

        [Fact]
        public void CalculateRemainingFat_WithValidInputs_ReturnsCorrectValue()
        {
            // Arrange
            NutritionCalculator calculator = new NutritionCalculator();
            DailySummary summary = new DailySummary();
            Meal meal = new Meal("Snack");
            meal.AddFoodItem(new FoodItem("Avocado", 160.0, 2.0, 9.0, 15.0));
            summary.AddMeal(meal);
            
            UserGoal goal = new UserGoal
            {
                DailyFatGoal = 65.0
            };

            // Act
            double remaining = calculator.CalculateRemainingFat(summary, goal);

            // Assert
            Assert.Equal(50.0, remaining);
        }

        [Fact]
        public void CalculateRemainingCalories_WhenOverGoal_ReturnsNegativeValue()
        {
            // Arrange
            NutritionCalculator calculator = new NutritionCalculator();
            DailySummary summary = new DailySummary();
            Meal meal = new Meal("Meal");
            meal.AddFoodItem(new FoodItem("Food", 2500.0, 50.0, 100.0, 30.0));
            summary.AddMeal(meal);
            
            UserGoal goal = new UserGoal
            {
                DailyCalorieGoal = 2000.0
            };

            // Act
            double remaining = calculator.CalculateRemainingCalories(summary, goal);

            // Assert
            Assert.True(remaining < 0);
            Assert.Equal(-500.0, remaining);
        }
    }
}

