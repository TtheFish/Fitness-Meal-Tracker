# 🍎 Fitness Meal Tracker

A modern Windows desktop application built with WPF (.NET 8.0) for tracking daily meals, food items, and nutritional goals. This application helps you monitor your calorie intake, macronutrients (protein, carbs, fat), and stay on track with your fitness goals.

## ✨ Features

- **Meal Management**: Create, view, and manage multiple meals throughout the day
- **Food Item Tracking**: Add food items to meals with detailed nutritional information
- **Nutritional Calculations**: 
  - Track total calories, protein, carbohydrates, and fat
  - Calculate remaining nutrients based on daily goals
  - View meal-level and daily summaries
- **Goal Setting**: Set and monitor daily nutritional goals (calories, protein, carbs, fat)
- **Data Persistence**: Automatically saves meal data to JSON file in AppData folder
- **Modern UI**: Clean and intuitive WPF interface with gradient styling

## 🏗️ Architecture

This project follows a clean architecture pattern with separation of concerns:

- **FitnessMealTracker.Core**: Domain models and business entities
  - `Meal`, `FoodItem`, `UserGoal`, `DailySummary`, `Exercise`
  
- **FitnessMealTracker.Business**: Business logic and services
  - `MealService`: Manages meal operations
  - `NutritionCalculator`: Calculates nutritional values and remaining goals
  
- **FitnessMealTracker.Data**: Data access layer
  - `JsonMealRepository`: JSON-based persistence implementation
  
- **FitnessMealTracker**: WPF presentation layer
  - MVVM pattern with `MainViewModel`
  - Modern WPF UI with XAML styling

- **FitnessMealTracker.Tests**: Unit tests for models, services, and repositories

## 🚀 Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Windows OS (WPF requirement)
- Visual Studio 2022 or Visual Studio Code with C# extension

### Installation

1. Clone the repository:
```bash
git clone https://github.com/TtheFish/Fitness-Meal-Tracker.git
cd Fitness-Meal-Tracker/FitnessMealTrackerSolution
```

2. Restore NuGet packages:
```bash
dotnet restore
```

3. Build the solution:
```bash
dotnet build
```

4. Run the application:
```bash
dotnet run --project FitnessMealTracker/FitnessMealTracker.csproj
```

Or open `FitnessMealTrackerSolution.sln` in Visual Studio and press F5.

## 📖 Usage

1. **Add a Meal**: Enter a meal name (e.g., "Breakfast", "Lunch") and click "Add Meal"
2. **Add Food Items**: 
   - Select a meal from the list
   - Enter food item details (name, calories, protein, carbs, fat)
   - Click "Add Food Item"
3. **View Summary**: The right panel displays:
   - Total daily calories and macronutrients
   - Remaining values based on your goals
   - Progress tracking
4. **Save Data**: Click "Save Meals" to persist your data (auto-saves to `%AppData%\FitnessMealTracker\meals.json`)

## 🎯 Default Goals

The application comes with default daily nutritional goals:
- **Calories**: 2000 kcal
- **Protein**: 150 g
- **Carbohydrates**: 250 g
- **Fat**: 65 g

You can modify these goals in the code or extend the UI to allow user customization.

## 🧪 Testing

Run the unit tests:
```bash
dotnet test
```

## 📁 Project Structure

```
FitnessMealTrackerSolution/
├── FitnessMealTracker/              # WPF UI application
│   ├── MainWindow.xaml              # Main UI
│   ├── ViewModels/                  # MVVM view models
│   └── ...
├── FitnessMealTracker.Core/         # Domain models
│   └── Models/
├── FitnessMealTracker.Business/     # Business logic
│   └── Services/
├── FitnessMealTracker.Data/         # Data access
│   └── Repositories/
└── FitnessMealTracker.Tests/        # Unit tests
```

## 🛠️ Technologies Used

- **.NET 8.0**: Latest .NET framework
- **WPF (Windows Presentation Foundation)**: UI framework
- **MVVM Pattern**: Model-View-ViewModel architecture
- **Newtonsoft.Json**: JSON serialization
- **xUnit**: Unit testing framework

## 📝 Data Storage

Meal data is automatically saved to:
```
%AppData%\FitnessMealTracker\meals.json
```

The application creates this directory automatically on first run.

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## 📄 License

This project is open source and available under the MIT License.

## 👤 Author

**TtheFish**

- GitHub: [@TtheFish](https://github.com/TtheFish)

## 🔮 Future Enhancements

Potential features for future versions:
- User profile management
- Customizable daily goals through UI
- Exercise tracking integration
- Meal templates and favorites
- Export/import functionality
- Charts and progress visualization
- Barcode scanning for food items
- Integration with nutrition databases

---

⭐ If you find this project useful, please consider giving it a star!

