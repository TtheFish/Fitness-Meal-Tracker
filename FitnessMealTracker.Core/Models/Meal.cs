using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace FitnessMealTracker.Core.Models
{
    public class Meal
    {
        private string _name = string.Empty;
        private readonly List<FoodItem> _items = new List<FoodItem>();

        public string Name
        {
            get => _name;
            set => _name = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
        }

        [JsonIgnore]
        public IReadOnlyList<FoodItem> Items => _items.AsReadOnly();

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

        public double TotalCalories => _items.Sum(item => item.Calories);

        public double TotalProtein => _items.Sum(item => item.Protein);

        public double TotalCarbs => _items.Sum(item => item.Carbs);

        public double TotalFat => _items.Sum(item => item.Fat);

        public Meal(string name)
        {
            Name = name;
        }

        public Meal()
        {
        }

        /// Adds a food item to the meal.
        /// <exception cref="ArgumentNullException">Thrown when item is null.</exception>
        public void AddFoodItem(FoodItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Food item cannot be null.");
            }
            _items.Add(item);
        }

        /// Removes a food item from the meal.
        public bool RemoveFoodItem(FoodItem item)
        {
            return item != null && _items.Remove(item);
        }

        /// Clears all food items from the meal.
        public void ClearFoodItems()
        {
            _items.Clear();
        }
    }
}
