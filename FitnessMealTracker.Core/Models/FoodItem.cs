namespace FitnessMealTracker.Core.Models
{
    /// <summary>
    /// Represents a food item with nutritional information.
    /// </summary>
    public class FoodItem
    {
        private const double MinimumNutrientValue = 0.0;

        private string _name = string.Empty;
        private double _calories;
        private double _protein;
        private double _carbs;
        private double _fat;

        /// <summary>
        /// Gets or sets the name of the food item.
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
        }

        /// <summary>
        /// Gets or sets the calories per serving.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the protein content in grams.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the carbohydrates content in grams.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the fat content in grams.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the FoodItem class with specified nutritional values.
        /// </summary>
        /// <param name="name">The name of the food item.</param>
        /// <param name="calories">The calories per serving.</param>
        /// <param name="protein">The protein content in grams.</param>
        /// <param name="carbs">The carbohydrates content in grams.</param>
        /// <param name="fat">The fat content in grams.</param>
        public FoodItem(string name, double calories, double protein, double carbs, double fat)
        {
            Name = name;
            Calories = calories;
            Protein = protein;
            Carbs = carbs;
            Fat = fat;
        }

        /// <summary>
        /// Initializes a new instance of the FoodItem class with default values.
        /// </summary>
        public FoodItem()
        {
        }
    }
}
