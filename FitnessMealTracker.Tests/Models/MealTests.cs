using System;
using FitnessMealTracker.Core.Models;
using Xunit;

namespace FitnessMealTracker.Tests.Models
{
    /// <summary>
    /// Unit tests for the Meal class.
    /// </summary>
    public class MealTests
    {
        [Fact]
        public void Constructor_WithName_SetsName()
        {
            // Arrange
            const string mealName = "Breakfast";

            // Act
            Meal meal = new Meal(mealName);

            // Assert
            Assert.Equal(mealName, meal.Name);
        }

        [Fact]
        public void Constructor_Default_InitializesWithEmptyName()
        {
            // Act
            Meal meal = new Meal();

            // Assert
            Assert.Equal(string.Empty, meal.Name);
        }

        [Fact]
        public void AddFoodItem_WithValidItem_AddsItemToMeal()
        {
            // Arrange
            Meal meal = new Meal("Lunch");
            FoodItem foodItem = new FoodItem("Chicken", 200.0, 30.0, 0.0, 5.0);

            // Act
            meal.AddFoodItem(foodItem);

            // Assert
            Assert.Single(meal.Items);
            Assert.Contains(foodItem, meal.Items);
        }

        [Fact]
        public void AddFoodItem_WithNullItem_ThrowsArgumentNullException()
        {
            // Arrange
            Meal meal = new Meal("Dinner");

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => meal.AddFoodItem(null!));
        }

        [Fact]
        public void RemoveFoodItem_WithExistingItem_ReturnsTrue()
        {
            // Arrange
            Meal meal = new Meal("Snack");
            FoodItem foodItem = new FoodItem("Banana", 100.0, 1.0, 25.0, 0.0);
            meal.AddFoodItem(foodItem);

            // Act
            bool result = meal.RemoveFoodItem(foodItem);

            // Assert
            Assert.True(result);
            Assert.Empty(meal.Items);
        }

        [Fact]
        public void RemoveFoodItem_WithNonExistingItem_ReturnsFalse()
        {
            // Arrange
            Meal meal = new Meal("Snack");
            FoodItem foodItem = new FoodItem("Banana", 100.0, 1.0, 25.0, 0.0);

            // Act
            bool result = meal.RemoveFoodItem(foodItem);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void RemoveFoodItem_WithNullItem_ReturnsFalse()
        {
            // Arrange
            Meal meal = new Meal("Snack");

            // Act
            bool result = meal.RemoveFoodItem(null!);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ClearFoodItems_RemovesAllItems()
        {
            // Arrange
            Meal meal = new Meal("Meal");
            meal.AddFoodItem(new FoodItem("Item1", 100.0, 10.0, 20.0, 5.0));
            meal.AddFoodItem(new FoodItem("Item2", 150.0, 15.0, 25.0, 6.0));

            // Act
            meal.ClearFoodItems();

            // Assert
            Assert.Empty(meal.Items);
        }

        [Fact]
        public void TotalCalories_WithMultipleItems_ReturnsSum()
        {
            // Arrange
            Meal meal = new Meal("Meal");
            meal.AddFoodItem(new FoodItem("Item1", 100.0, 10.0, 20.0, 5.0));
            meal.AddFoodItem(new FoodItem("Item2", 150.0, 15.0, 25.0, 6.0));

            // Act
            double totalCalories = meal.TotalCalories;

            // Assert
            Assert.Equal(250.0, totalCalories);
        }

        [Fact]
        public void TotalProtein_WithMultipleItems_ReturnsSum()
        {
            // Arrange
            Meal meal = new Meal("Meal");
            meal.AddFoodItem(new FoodItem("Item1", 100.0, 10.0, 20.0, 5.0));
            meal.AddFoodItem(new FoodItem("Item2", 150.0, 15.0, 25.0, 6.0));

            // Act
            double totalProtein = meal.TotalProtein;

            // Assert
            Assert.Equal(25.0, totalProtein);
        }

        [Fact]
        public void TotalCarbs_WithMultipleItems_ReturnsSum()
        {
            // Arrange
            Meal meal = new Meal("Meal");
            meal.AddFoodItem(new FoodItem("Item1", 100.0, 10.0, 20.0, 5.0));
            meal.AddFoodItem(new FoodItem("Item2", 150.0, 15.0, 25.0, 6.0));

            // Act
            double totalCarbs = meal.TotalCarbs;

            // Assert
            Assert.Equal(45.0, totalCarbs);
        }

        [Fact]
        public void TotalFat_WithMultipleItems_ReturnsSum()
        {
            // Arrange
            Meal meal = new Meal("Meal");
            meal.AddFoodItem(new FoodItem("Item1", 100.0, 10.0, 20.0, 5.0));
            meal.AddFoodItem(new FoodItem("Item2", 150.0, 15.0, 25.0, 6.0));

            // Act
            double totalFat = meal.TotalFat;

            // Assert
            Assert.Equal(11.0, totalFat);
        }
    }
}

