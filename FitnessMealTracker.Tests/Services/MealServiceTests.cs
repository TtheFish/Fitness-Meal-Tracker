using System;
using System.Collections.Generic;
using System.Linq;
using FitnessMealTracker.Business.Services;
using FitnessMealTracker.Core.Models;
using FitnessMealTracker.Data.Repositories;
using Xunit;

namespace FitnessMealTracker.Tests.Services
{
    /// <summary>
    /// Unit tests for the MealService class.
    /// </summary>
    public class MealServiceTests
    {
        [Fact]
        public void Constructor_WithNullRepository_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new MealService(null!));
        }

        [Fact]
        public void AddMeal_WithValidMeal_AddsMealToService()
        {
            // Arrange
            IMealRepository repository = new InMemoryMealRepository();
            MealService service = new MealService(repository);
            Meal meal = new Meal("Breakfast");

            // Act
            service.AddMeal(meal);

            // Assert
            IReadOnlyList<Meal> meals = service.GetAllMeals();
            Assert.Single(meals);
            Assert.Contains(meal, meals);
        }

        [Fact]
        public void AddMeal_WithNullMeal_ThrowsArgumentNullException()
        {
            // Arrange
            IMealRepository repository = new InMemoryMealRepository();
            MealService service = new MealService(repository);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.AddMeal(null!));
        }

        [Fact]
        public void RemoveMeal_WithExistingMeal_ReturnsTrue()
        {
            // Arrange
            IMealRepository repository = new InMemoryMealRepository();
            MealService service = new MealService(repository);
            Meal meal = new Meal("Lunch");
            service.AddMeal(meal);

            // Act
            bool result = service.RemoveMeal(meal);

            // Assert
            Assert.True(result);
            Assert.Empty(service.GetAllMeals());
        }

        [Fact]
        public void GetDailySummary_ReturnsSummaryWithAllMeals()
        {
            // Arrange
            IMealRepository repository = new InMemoryMealRepository();
            MealService service = new MealService(repository);
            Meal meal1 = new Meal("Breakfast");
            Meal meal2 = new Meal("Lunch");
            service.AddMeal(meal1);
            service.AddMeal(meal2);

            // Act
            DailySummary summary = service.GetDailySummary();

            // Assert
            Assert.Equal(2, summary.Meals.Count);
            Assert.Contains(meal1, summary.Meals);
            Assert.Contains(meal2, summary.Meals);
        }

        [Fact]
        public void SaveMeals_SavesMealsToRepository()
        {
            // Arrange
            InMemoryMealRepository repository = new InMemoryMealRepository();
            MealService service = new MealService(repository);
            Meal meal = new Meal("Dinner");
            service.AddMeal(meal);

            // Act
            service.SaveMeals();

            // Assert
            IReadOnlyList<Meal> savedMeals = repository.LoadMeals();
            Assert.Single(savedMeals);
        }

        [Fact]
        public void LoadMeals_LoadsMealsFromRepository()
        {
            // Arrange
            InMemoryMealRepository repository = new InMemoryMealRepository();
            Meal meal = new Meal("Breakfast");
            repository.SaveMeals(new List<Meal> { meal });
            MealService service = new MealService(repository);

            // Act
            service.LoadMeals();

            // Assert
            IReadOnlyList<Meal> meals = service.GetAllMeals();
            Assert.Single(meals);
            Assert.Equal(meal.Name, meals.First().Name);
        }

        /// <summary>
        /// In-memory implementation of IMealRepository for testing purposes.
        /// </summary>
        private class InMemoryMealRepository : IMealRepository
        {
            private List<Meal> _meals = new List<Meal>();

            public IReadOnlyList<Meal> LoadMeals()
            {
                return _meals.ToList();
            }

            public void SaveMeals(IReadOnlyList<Meal> meals)
            {
                _meals = meals.ToList();
            }
        }
    }
}

