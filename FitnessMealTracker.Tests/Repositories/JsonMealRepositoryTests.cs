using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FitnessMealTracker.Core.Models;
using FitnessMealTracker.Data.Repositories;
using Xunit;

namespace FitnessMealTracker.Tests.Repositories
{
    /// <summary>
    /// Unit tests for the JsonMealRepository class.
    /// </summary>
    public class JsonMealRepositoryTests
    {
        [Fact]
        public void Constructor_WithNullFilePath_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new JsonMealRepository(null!));
        }

        [Fact]
        public void Constructor_WithEmptyFilePath_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new JsonMealRepository(string.Empty));
        }

        [Fact]
        public void LoadMeals_WhenFileDoesNotExist_ReturnsEmptyList()
        {
            // Arrange
            string filePath = Path.GetTempFileName();
            File.Delete(filePath);
            JsonMealRepository repository = new JsonMealRepository(filePath);

            // Act
            var result = repository.LoadMeals();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void SaveMeals_ThenLoadMeals_ReturnsSameMeals()
        {
            // Arrange
            string filePath = Path.GetTempFileName();
            JsonMealRepository repository = new JsonMealRepository(filePath);
            Meal meal1 = new Meal("Breakfast");
            meal1.AddFoodItem(new FoodItem("Egg", 70.0, 6.0, 0.5, 5.0));
            Meal meal2 = new Meal("Lunch");
            meal2.AddFoodItem(new FoodItem("Chicken", 200.0, 30.0, 0.0, 8.0));
            var meals = new List<Meal> { meal1, meal2 };

            try
            {
                // Act
                repository.SaveMeals(meals);
                var loadedMeals = repository.LoadMeals();

                // Assert
                Assert.Equal(2, loadedMeals.Count);
                Assert.Equal(meal1.Name, loadedMeals[0].Name);
                Assert.Equal(meal2.Name, loadedMeals[1].Name);
                Assert.Single(loadedMeals[0].Items);
                Assert.Single(loadedMeals[1].Items);
            }
            finally
            {
                // Cleanup
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }

        [Fact]
        public void SaveMeals_WithNullMeals_ThrowsArgumentNullException()
        {
            // Arrange
            string filePath = Path.GetTempFileName();
            JsonMealRepository repository = new JsonMealRepository(filePath);

            try
            {
                // Act & Assert
                Assert.Throws<ArgumentNullException>(() => repository.SaveMeals(null!));
            }
            finally
            {
                // Cleanup
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }

        [Fact]
        public void SaveMeals_CreatesDirectoryIfNotExists()
        {
            // Arrange
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            string filePath = Path.Combine(tempDir, "meals.json");
            JsonMealRepository repository = new JsonMealRepository(filePath);
            var meals = new List<Meal> { new Meal("Test") };

            try
            {
                // Act
                repository.SaveMeals(meals);

                // Assert
                Assert.True(Directory.Exists(tempDir));
                Assert.True(File.Exists(filePath));
            }
            finally
            {
                // Cleanup
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
            }
        }

        [Fact]
        public void LoadMeals_WithEmptyFile_ReturnsEmptyList()
        {
            // Arrange
            string filePath = Path.GetTempFileName();
            File.WriteAllText(filePath, string.Empty);
            JsonMealRepository repository = new JsonMealRepository(filePath);

            try
            {
                // Act
                var result = repository.LoadMeals();

                // Assert
                Assert.NotNull(result);
                Assert.Empty(result);
            }
            finally
            {
                // Cleanup
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }
    }
}

