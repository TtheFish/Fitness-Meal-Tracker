using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace FitnessMealTracker.Core.Models
{
    /// <summary>
    /// Represents a meal containing multiple food items.
    /// </summary>
    public class Meal
    {
        private string _name = string.Empty;
        private readonly List<FoodItem> _items = new List<FoodItem>();

        /// <summary>
        /// Gets or sets the name of the meal.
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
        }

        /// <summary>
        /// Gets a read-only collection of food items in this meal.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<FoodItem> Items => _items.AsReadOnly();

        /// <summary>
        /// Gets or sets the food items for JSON serialization/deserialization.
        /// </summary>
        [JsonProperty("Items")]
        private List<FoodItem> ItemsForSerialization
        {
            get => _items;
            set
            {
                _items.Clear();
                if (value != null)
                {
                    _items.AddRange(value);
                }
            }
        }

        /// <summary>
        /// Gets the total calories of all food items in the meal.
        /// </summary>
        public double TotalCalories => _items.Sum(item => item.Calories);

        /// <summary>
        /// Gets the total protein content of all food items in the meal.
        /// </summary>
        public double TotalProtein => _items.Sum(item => item.Protein);

        /// <summary>
        /// Gets the total carbohydrates content of all food items in the meal.
        /// </summary>
        public double TotalCarbs => _items.Sum(item => item.Carbs);

        /// <summary>
        /// Gets the total fat content of all food items in the meal.
        /// </summary>
        public double TotalFat => _items.Sum(item => item.Fat);

        /// <summary>
        /// Initializes a new instance of the Meal class with a specified name.
        /// </summary>
        /// <param name="name">The name of the meal.</param>
        public Meal(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the Meal class with default values.
        /// </summary>
        public Meal()
        {
        }

        /// <summary>
        /// Adds a food item to the meal.
        /// </summary>
        /// <param name="item">The food item to add.</param>
        /// <exception cref="ArgumentNullException">Thrown when item is null.</exception>
        public void AddFoodItem(FoodItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Food item cannot be null.");
            }
            _items.Add(item);
        }

        /// <summary>
        /// Removes a food item from the meal.
        /// </summary>
        /// <param name="item">The food item to remove.</param>
        /// <returns>True if the item was removed; otherwise, false.</returns>
        public bool RemoveFoodItem(FoodItem item)
        {
            return item != null && _items.Remove(item);
        }

        /// <summary>
        /// Clears all food items from the meal.
        /// </summary>
        public void ClearFoodItems()
        {
            _items.Clear();
        }
    }
}
