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

        /// Initializes a new instance of the MainViewModel class.
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

        public ObservableCollection<Meal> Meals { get; }

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

        public ObservableCollection<FoodItem>? SelectedMealFoodItems
        {
            get => _selectedMealFoodItems;
            private set
            {
                _selectedMealFoodItems = value;
                OnPropertyChanged();
            }
        }

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

        public string NewFoodItemProtein
        {
            get => _newFoodItemProtein;
            set
            {
                _newFoodItemProtein = value;
                OnPropertyChanged();
            }
        }

        public string NewFoodItemCarbs
        {
            get => _newFoodItemCarbs;
            set
            {
                _newFoodItemCarbs = value;
                OnPropertyChanged();
            }
        }

        public string NewFoodItemFat
        {
            get => _newFoodItemFat;
            set
            {
                _newFoodItemFat = value;
                OnPropertyChanged();
            }
        }

        public double TotalCalories => _mealService.GetDailySummary().TotalCalories;

        public double TotalProtein => _mealService.GetDailySummary().TotalProtein;

        public double TotalCarbs => _mealService.GetDailySummary().TotalCarbs;

        public double TotalFat => _mealService.GetDailySummary().TotalFat;

        public double RemainingCalories => _nutritionCalculator.CalculateRemainingCalories(_mealService.GetDailySummary(), _userGoal);

        public double RemainingProtein => _nutritionCalculator.CalculateRemainingProtein(_mealService.GetDailySummary(), _userGoal);

        public double RemainingCarbs => _nutritionCalculator.CalculateRemainingCarbs(_mealService.GetDailySummary(), _userGoal);

        public double RemainingFat => _nutritionCalculator.CalculateRemainingFat(_mealService.GetDailySummary(), _userGoal);

        public ICommand LoadMealsCommand { get; }

        public ICommand SaveMealsCommand { get; }

        public ICommand AddMealCommand { get; }

        public ICommand RemoveMealCommand { get; }

        public ICommand AddFoodItemCommand { get; }

        public ICommand RemoveFoodItemCommand { get; }

        /// Loads meals from persistent storage.
        private void LoadMeals()
        {
            _mealService.LoadMeals();
            RefreshMeals();
        }

        /// Saves meals to persistent storage.
        private void SaveMeals()
        {
            _mealService.SaveMeals();
        }

        /// Adds a new meal with the specified name.
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

        /// Determines whether a meal can be added.
        private bool CanAddMeal()
        {
            return !string.IsNullOrWhiteSpace(NewMealName);
        }

        /// Removes the currently selected meal.
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

        /// Determines whether a meal can be removed.
        private bool CanRemoveMeal()
        {
            return SelectedMeal != null;
        }

        /// Adds a food item to the selected meal.
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

        /// Determines whether a food item can be added.
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

        /// Removes the currently selected food item from the selected meal.
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

        /// Determines whether a food item can be removed.
        private bool CanRemoveFoodItem()
        {
            return SelectedMeal != null && SelectedFoodItem != null;
        }

        /// Updates the selected meal's food items collection.
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

        /// Refreshes the meals collection and updates all calculated properties.
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
