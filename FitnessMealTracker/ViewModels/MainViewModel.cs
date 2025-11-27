using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FitnessMealTracker.Business.Services;
using FitnessMealTracker.Core.Models;

namespace FitnessMealTracker.ViewModels
{
    /// <summary>
    /// ViewModel for the main window, providing data binding and command handling.
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IMealService _mealService;
        private readonly INutritionCalculator _nutritionCalculator;
        private UserGoal _userGoal;
        private Meal? _selectedMeal;
        private FoodItem? _selectedFoodItem;
        private string _newMealName = string.Empty;
        private string _newFoodItemName = string.Empty;
        private string _newFoodItemCalories = string.Empty;
        private string _newFoodItemProtein = string.Empty;
        private string _newFoodItemCarbs = string.Empty;
        private string _newFoodItemFat = string.Empty;
        private ObservableCollection<FoodItem>? _selectedMealFoodItems;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// <param name="mealService">The meal service for business logic operations.</param>
        /// <param name="nutritionCalculator">The nutrition calculator for goal calculations.</param>
        /// <exception cref="ArgumentNullException">Thrown when mealService or nutritionCalculator is null.</exception>
        public MainViewModel(IMealService mealService, INutritionCalculator nutritionCalculator)
        {
            _mealService = mealService ?? throw new ArgumentNullException(nameof(mealService));
            _nutritionCalculator = nutritionCalculator ?? throw new ArgumentNullException(nameof(nutritionCalculator));
            
            _userGoal = new UserGoal
            {
                DailyCalorieGoal = 2000.0,
                DailyProteinGoal = 150.0,
                DailyCarbGoal = 250.0,
                DailyFatGoal = 65.0
            };

            Meals = new ObservableCollection<Meal>();
            LoadMealsCommand = new RelayCommand(_ => LoadMeals());
            SaveMealsCommand = new RelayCommand(_ => SaveMeals());
            AddMealCommand = new RelayCommand(_ => AddMeal(), _ => CanAddMeal());
            RemoveMealCommand = new RelayCommand(_ => RemoveMeal(), _ => CanRemoveMeal());
            AddFoodItemCommand = new RelayCommand(_ => AddFoodItem(), _ => CanAddFoodItem());
            RemoveFoodItemCommand = new RelayCommand(_ => RemoveFoodItem(), _ => CanRemoveFoodItem());
            
            RefreshMeals();
        }

        /// <summary>
        /// Gets the collection of meals for the current day.
        /// </summary>
        public ObservableCollection<Meal> Meals { get; }

        /// <summary>
        /// Gets or sets the currently selected meal.
        /// </summary>
        public Meal? SelectedMeal
        {
            get => _selectedMeal;
            set
            {
                _selectedMeal = value;
                UpdateSelectedMealFoodItems();
                OnPropertyChanged();
                ((RelayCommand)RemoveMealCommand).RaiseCanExecuteChanged();
                ((RelayCommand)AddFoodItemCommand).RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets the food items of the selected meal.
        /// </summary>
        public ObservableCollection<FoodItem>? SelectedMealFoodItems
        {
            get => _selectedMealFoodItems;
            private set
            {
                _selectedMealFoodItems = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the currently selected food item.
        /// </summary>
        public FoodItem? SelectedFoodItem
        {
            get => _selectedFoodItem;
            set
            {
                _selectedFoodItem = value;
                OnPropertyChanged();
                ((RelayCommand)RemoveFoodItemCommand).RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets the name for a new meal.
        /// </summary>
        public string NewMealName
        {
            get => _newMealName;
            set
            {
                _newMealName = value;
                OnPropertyChanged();
                ((RelayCommand)AddMealCommand).RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets the name for a new food item.
        /// </summary>
        public string NewFoodItemName
        {
            get => _newFoodItemName;
            set
            {
                _newFoodItemName = value;
                OnPropertyChanged();
                ((RelayCommand)AddFoodItemCommand).RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets the calories for a new food item.
        /// </summary>
        public string NewFoodItemCalories
        {
            get => _newFoodItemCalories;
            set
            {
                _newFoodItemCalories = value;
                OnPropertyChanged();
                ((RelayCommand)AddFoodItemCommand).RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets the protein for a new food item.
        /// </summary>
        public string NewFoodItemProtein
        {
            get => _newFoodItemProtein;
            set
            {
                _newFoodItemProtein = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the carbohydrates for a new food item.
        /// </summary>
        public string NewFoodItemCarbs
        {
            get => _newFoodItemCarbs;
            set
            {
                _newFoodItemCarbs = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the fat for a new food item.
        /// </summary>
        public string NewFoodItemFat
        {
            get => _newFoodItemFat;
            set
            {
                _newFoodItemFat = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the total calories consumed today.
        /// </summary>
        public double TotalCalories => _mealService.GetDailySummary().TotalCalories;

        /// <summary>
        /// Gets the total protein consumed today.
        /// </summary>
        public double TotalProtein => _mealService.GetDailySummary().TotalProtein;

        /// <summary>
        /// Gets the total carbohydrates consumed today.
        /// </summary>
        public double TotalCarbs => _mealService.GetDailySummary().TotalCarbs;

        /// <summary>
        /// Gets the total fat consumed today.
        /// </summary>
        public double TotalFat => _mealService.GetDailySummary().TotalFat;

        /// <summary>
        /// Gets the remaining calories to reach the daily goal.
        /// </summary>
        public double RemainingCalories => _nutritionCalculator.CalculateRemainingCalories(_mealService.GetDailySummary(), _userGoal);

        /// <summary>
        /// Gets the remaining protein to reach the daily goal.
        /// </summary>
        public double RemainingProtein => _nutritionCalculator.CalculateRemainingProtein(_mealService.GetDailySummary(), _userGoal);

        /// <summary>
        /// Gets the remaining carbohydrates to reach the daily goal.
        /// </summary>
        public double RemainingCarbs => _nutritionCalculator.CalculateRemainingCarbs(_mealService.GetDailySummary(), _userGoal);

        /// <summary>
        /// Gets the remaining fat to reach the daily goal.
        /// </summary>
        public double RemainingFat => _nutritionCalculator.CalculateRemainingFat(_mealService.GetDailySummary(), _userGoal);

        /// <summary>
        /// Gets the command to load meals from storage.
        /// </summary>
        public ICommand LoadMealsCommand { get; }

        /// <summary>
        /// Gets the command to save meals to storage.
        /// </summary>
        public ICommand SaveMealsCommand { get; }

        /// <summary>
        /// Gets the command to add a new meal.
        /// </summary>
        public ICommand AddMealCommand { get; }

        /// <summary>
        /// Gets the command to remove the selected meal.
        /// </summary>
        public ICommand RemoveMealCommand { get; }

        /// <summary>
        /// Gets the command to add a food item to the selected meal.
        /// </summary>
        public ICommand AddFoodItemCommand { get; }

        /// <summary>
        /// Gets the command to remove the selected food item.
        /// </summary>
        public ICommand RemoveFoodItemCommand { get; }

        /// <summary>
        /// Loads meals from persistent storage.
        /// </summary>
        private void LoadMeals()
        {
            _mealService.LoadMeals();
            RefreshMeals();
        }

        /// <summary>
        /// Saves meals to persistent storage.
        /// </summary>
        private void SaveMeals()
        {
            _mealService.SaveMeals();
        }

        /// <summary>
        /// Adds a new meal with the specified name.
        /// </summary>
        private void AddMeal()
        {
            if (!CanAddMeal())
            {
                return;
            }

            Meal newMeal = new Meal(NewMealName);
            _mealService.AddMeal(newMeal);
            NewMealName = string.Empty;
            RefreshMeals();
            SelectedMeal = newMeal;
        }

        /// <summary>
        /// Determines whether a meal can be added.
        /// </summary>
        private bool CanAddMeal()
        {
            return !string.IsNullOrWhiteSpace(NewMealName);
        }

        /// <summary>
        /// Removes the currently selected meal.
        /// </summary>
        private void RemoveMeal()
        {
            if (SelectedMeal == null)
            {
                return;
            }

            _mealService.RemoveMeal(SelectedMeal);
            RefreshMeals();
            SelectedMeal = null;
        }

        /// <summary>
        /// Determines whether a meal can be removed.
        /// </summary>
        private bool CanRemoveMeal()
        {
            return SelectedMeal != null;
        }

        /// <summary>
        /// Adds a food item to the selected meal.
        /// </summary>
        private void AddFoodItem()
        {
            if (SelectedMeal == null || !CanAddFoodItem())
            {
                return;
            }

            if (!double.TryParse(NewFoodItemCalories, out double calories) ||
                !double.TryParse(NewFoodItemProtein, out double protein) ||
                !double.TryParse(NewFoodItemCarbs, out double carbs) ||
                !double.TryParse(NewFoodItemFat, out double fat))
            {
                return;
            }

            FoodItem newFoodItem = new FoodItem(NewFoodItemName, calories, protein, carbs, fat);
            SelectedMeal.AddFoodItem(newFoodItem);
            
            NewFoodItemName = string.Empty;
            NewFoodItemCalories = string.Empty;
            NewFoodItemProtein = string.Empty;
            NewFoodItemCarbs = string.Empty;
            NewFoodItemFat = string.Empty;
            
            RefreshMeals();
            UpdateSelectedMealFoodItems();
        }

        /// <summary>
        /// Determines whether a food item can be added.
        /// </summary>
        private bool CanAddFoodItem()
        {
            if (SelectedMeal == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(NewFoodItemName))
            {
                return false;
            }

            return double.TryParse(NewFoodItemCalories, out _) &&
                   double.TryParse(NewFoodItemProtein, out _) &&
                   double.TryParse(NewFoodItemCarbs, out _) &&
                   double.TryParse(NewFoodItemFat, out _);
        }

        /// <summary>
        /// Removes the currently selected food item from the selected meal.
        /// </summary>
        private void RemoveFoodItem()
        {
            if (SelectedMeal == null || SelectedFoodItem == null)
            {
                return;
            }

            SelectedMeal.RemoveFoodItem(SelectedFoodItem);
            RefreshMeals();
            UpdateSelectedMealFoodItems();
            SelectedFoodItem = null;
        }

        /// <summary>
        /// Determines whether a food item can be removed.
        /// </summary>
        private bool CanRemoveFoodItem()
        {
            return SelectedMeal != null && SelectedFoodItem != null;
        }

        /// <summary>
        /// Updates the selected meal's food items collection.
        /// </summary>
        private void UpdateSelectedMealFoodItems()
        {
            if (SelectedMeal == null)
            {
                SelectedMealFoodItems = null;
            }
            else
            {
                SelectedMealFoodItems = new ObservableCollection<FoodItem>(SelectedMeal.Items);
            }
        }

        /// <summary>
        /// Refreshes the meals collection and updates all calculated properties.
        /// </summary>
        private void RefreshMeals()
        {
            Meals.Clear();
            foreach (Meal meal in _mealService.GetAllMeals())
            {
                Meals.Add(meal);
            }

            UpdateSelectedMealFoodItems();

            OnPropertyChanged(nameof(TotalCalories));
            OnPropertyChanged(nameof(TotalProtein));
            OnPropertyChanged(nameof(TotalCarbs));
            OnPropertyChanged(nameof(TotalFat));
            OnPropertyChanged(nameof(RemainingCalories));
            OnPropertyChanged(nameof(RemainingProtein));
            OnPropertyChanged(nameof(RemainingCarbs));
            OnPropertyChanged(nameof(RemainingFat));
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
