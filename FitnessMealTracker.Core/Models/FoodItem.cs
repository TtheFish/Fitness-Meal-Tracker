namespace FitnessMealTracker.Core.Models
{
    public class FoodItem
    {
        private const double MinimumNutrientValue = 0.0;

        private string _name = string.Empty;
        private double _calories;
        private double _protein;
        private double _carbs;
        private double _fat;

        public string Name
        {
            get => _name;
            set => _name = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
        }

        /// <exception cref="ArgumentOutOfRangeException">Thrown when value is negative.</exception>
        public double Calories
        {
            get => _calories;
            set
            {
                if (value < MinimumNutrientValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Calories cannot be negative.");
                }
                _calories = value;
            }
        }

        /// <exception cref="ArgumentOutOfRangeException">Thrown when value is negative.</exception>
        public double Protein
        {
            get => _protein;
            set
            {
                if (value < MinimumNutrientValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Protein cannot be negative.");
                }
                _protein = value;
            }
        }

        /// <exception cref="ArgumentOutOfRangeException">Thrown when value is negative.</exception>
        public double Carbs
        {
            get => _carbs;
            set
            {
                if (value < MinimumNutrientValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Carbs cannot be negative.");
                }
                _carbs = value;
            }
        }

        /// <exception cref="ArgumentOutOfRangeException">Thrown when value is negative.</exception>
        public double Fat
        {
            get => _fat;
            set
            {
                if (value < MinimumNutrientValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Fat cannot be negative.");
                }
                _fat = value;
            }
        }

        public FoodItem(string name, double calories, double protein, double carbs, double fat)
        {
            Name = name;
            Calories = calories;
            Protein = protein;
            Carbs = carbs;
            Fat = fat;
        }

        public FoodItem()
        {
        }
    }
}
