using System;
using System.IO;
using System.Windows;
using FitnessMealTracker.Business.Services;
using FitnessMealTracker.Data.Repositories;
using FitnessMealTracker.ViewModels;

namespace FitnessMealTracker
{
    public partial class MainWindow : Window
    {
        private const string DefaultDataFilePath = "meals.json";

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                
                string dataFilePath = GetDataFilePath();
                IMealRepository mealRepository = new JsonMealRepository(dataFilePath);
                IMealService mealService = new MealService(mealRepository);
                INutritionCalculator nutritionCalculator = new NutritionCalculator();
                
                DataContext = new MainViewModel(mealService, nutritionCalculator);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error initializing window: {ex.Message}\n\n{ex.StackTrace}",
                    "Initialization Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                throw;
            }
        }

        private static string GetDataFilePath()
        {
            string appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "FitnessMealTracker");
            
            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }
            
            return Path.Combine(appDataPath, DefaultDataFilePath);
        }
    }
}
