using System;
using FitnessMealTracker.Core.Models;
using Xunit;

namespace FitnessMealTracker.Tests.Models
{
    /// <summary>
    /// Unit tests for the FoodItem class.
    /// </summary>
    public class FoodItemTests
    {
        [Fact]
        public void Constructor_WithValidParameters_SetsAllProperties()
        {
            // Arrange
            const string name = "Apple";
            const double calories = 95.0;
            const double protein = 0.5;
            const double carbs = 25.0;
            const double fat = 0.3;

            // Act
            FoodItem foodItem = new FoodItem(name, calories, protein, carbs, fat);

            // Assert
            Assert.Equal(name, foodItem.Name);
            Assert.Equal(calories, foodItem.Calories);
            Assert.Equal(protein, foodItem.Protein);
            Assert.Equal(carbs, foodItem.Carbs);
            Assert.Equal(fat, foodItem.Fat);
        }

        [Fact]
        public void Constructor_Default_InitializesWithDefaultValues()
        {
            // Act
            FoodItem foodItem = new FoodItem();

            // Assert
            Assert.Equal(string.Empty, foodItem.Name);
            Assert.Equal(0.0, foodItem.Calories);
            Assert.Equal(0.0, foodItem.Protein);
            Assert.Equal(0.0, foodItem.Carbs);
            Assert.Equal(0.0, foodItem.Fat);
        }

        [Fact]
        public void Name_SetToNull_ReturnsEmptyString()
        {
            // Arrange
            FoodItem foodItem = new FoodItem();

            // Act
            foodItem.Name = null!;

            // Assert
            Assert.Equal(string.Empty, foodItem.Name);
        }

        [Fact]
        public void Name_SetToWhitespace_ReturnsEmptyString()
        {
            // Arrange
            FoodItem foodItem = new FoodItem();

            // Act
            foodItem.Name = "   ";

            // Assert
            Assert.Equal(string.Empty, foodItem.Name);
        }

        [Fact]
        public void Calories_SetToNegative_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            FoodItem foodItem = new FoodItem();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => foodItem.Calories = -1.0);
        }

        [Fact]
        public void Calories_SetToZero_Succeeds()
        {
            // Arrange
            FoodItem foodItem = new FoodItem();

            // Act
            foodItem.Calories = 0.0;

            // Assert
            Assert.Equal(0.0, foodItem.Calories);
        }

        [Fact]
        public void Protein_SetToNegative_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            FoodItem foodItem = new FoodItem();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => foodItem.Protein = -1.0);
        }

        [Fact]
        public void Carbs_SetToNegative_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            FoodItem foodItem = new FoodItem();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => foodItem.Carbs = -1.0);
        }

        [Fact]
        public void Fat_SetToNegative_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            FoodItem foodItem = new FoodItem();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => foodItem.Fat = -1.0);
        }
    }
}

