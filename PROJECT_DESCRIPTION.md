# Fitness Meal Tracker - Project Description

## 📋 Project Overview

**Fitness Meal Tracker** is a Windows desktop application that allows users to track their daily meals, calculate nutritional values, and see how much remains to reach their daily goals. The application is developed using **WPF (Windows Presentation Foundation)** technology with C# .NET 8.0.

---

## 🏗️ Architecture

The project is designed according to the **3-Layer Architecture** principle:

```
┌─────────────────────────────────────┐
│   FitnessMealTracker (UI Layer)     │  ← User Interface
├─────────────────────────────────────┤
│   FitnessMealTracker.Business       │  ← Business Logic Layer
├─────────────────────────────────────┤
│   FitnessMealTracker.Data           │  ← Data Access Layer
├─────────────────────────────────────┤
│   FitnessMealTracker.Core           │  ← Shared Models
└─────────────────────────────────────┘
```

### Layer Responsibilities:

1. **UI Layer (FitnessMealTracker)**: User interface, ViewModels, and WPF components
2. **Business Layer (FitnessMealTracker.Business)**: Business logic, calculations, and services
3. **Data Layer (FitnessMealTracker.Data)**: Data storage and loading operations
4. **Core Layer (FitnessMealTracker.Core)**: Shared models used by all layers

---

## 📁 File Structure and Responsibilities

### 🎨 **UI Layer (FitnessMealTracker)**

#### `MainWindow.xaml`
- **Purpose**: Contains the visual design of the application's main window
- **Content**: 
  - Left panel: Meal list and food item addition section
  - Right panel: Daily summary and goal tracking
  - Modern, colorful, and user-friendly interface design
- **Features**: Data binding connected to ViewModel, UI enriched with emojis

#### `MainWindow.xaml.cs`
- **Purpose**: Code-behind file for the main window
- **Functions**:
  - Creates all services and repositories when the application starts
  - Establishes a Dependency Injection-like structure
  - Determines the data file path (`GetDataFilePath()`)
  - **IMPORTANT**: Data is saved to `%AppData%\FitnessMealTracker\meals.json` file

#### `App.xaml.cs`
- **Purpose**: Application-level error handling
- **Functions**: Catches unexpected errors and displays them to the user

#### `ViewModels/MainViewModel.cs`
- **Purpose**: ViewModel layer of the MVVM pattern
- **Functions**:
  - Acts as a bridge between UI and Business Layer
  - Manages all commands (AddMeal, RemoveMeal, AddFoodItem, etc.)
  - Updates UI through PropertyChanged events
  - Calculates total calories, protein, carbohydrates, and fat
  - Calculates remaining goal values

#### `ViewModels/RelayCommand.cs`
- **Purpose**: Command implementation for MVVM pattern
- **Functions**: Executes commands bound to buttons

---

### 💼 **Business Layer (FitnessMealTracker.Business)**

#### `Services/MealService.cs`
- **Purpose**: Business logic for meal management
- **Functions**:
  - Meal addition and removal operations
  - Manages daily summary (`DailySummary`)
  - Coordinates save/load operations to repository

#### `Services/IMealService.cs`
- **Purpose**: Interface (abstract class) for MealService
- **Benefit**: Different implementations can be added in the future (e.g., database version)

#### `Services/NutritionCalculator.cs`
- **Purpose**: Nutrition calculations
- **Functions**:
  - Calculates remaining calories
  - Calculates remaining protein, carbohydrates, and fat
  - Formula: `Remaining = Goal - Consumed`

#### `Services/INutritionCalculator.cs`
- **Purpose**: Interface for NutritionCalculator

---

### 💾 **Data Layer (FitnessMealTracker.Data)**

#### `Repositories/JsonMealRepository.cs`
- **Purpose**: Saves and loads data to/from JSON file
- **Functions**:
  - `SaveMeals()`: Writes all meals to file in JSON format
  - `LoadMeals()`: Reads meals from JSON file and returns them
  - **Library Used**: Newtonsoft.Json
  - **Error Handling**: Returns empty list if file doesn't exist, throws exception on JSON error

#### `Repositories/IMealRepository.cs`
- **Purpose**: Interface for Repository
- **Benefit**: Different storage methods can be added in the future (e.g., XML, database)

---

### 📦 **Core Layer (FitnessMealTracker.Core)**

#### `Models/Meal.cs`
- **Purpose**: Meal model
- **Properties**:
  - `Name`: Meal name (e.g., "Breakfast", "Lunch")
  - `Items`: List of food items in the meal
  - `TotalCalories`, `TotalProtein`, `TotalCarbs`, `TotalFat`: Total nutritional values
- **Methods**: `AddFoodItem()`, `RemoveFoodItem()`, `ClearFoodItems()`

#### `Models/FoodItem.cs`
- **Purpose**: Food item model
- **Properties**:
  - `Name`: Food name (e.g., "Egg", "Chicken Breast")
  - `Calories`: Calorie value
  - `Protein`: Protein (grams)
  - `Carbs`: Carbohydrates (grams)
  - `Fat`: Fat (grams)
- **Validation**: Negative values are not accepted

#### `Models/DailySummary.cs`
- **Purpose**: Daily summary model
- **Functions**:
  - Holds all meals
  - Calculates daily total calories, protein, carbohydrates, and fat
  - Methods: `AddMeal()`, `RemoveMeal()`, `ClearMeals()`

#### `Models/UserGoal.cs`
- **Purpose**: User goals model
- **Properties**:
  - `DailyCalorieGoal`: Daily calorie goal (default: 2000)
  - `DailyProteinGoal`: Daily protein goal (default: 150g)
  - `DailyCarbGoal`: Daily carbohydrate goal (default: 250g)
  - `DailyFatGoal`: Daily fat goal (default: 65g)

---

## 🔄 Application Flow

### 1️⃣ **Application Startup**

```
App.xaml.cs (OnStartup)
    ↓
MainWindow.xaml.cs (Constructor)
    ↓
1. GetDataFilePath() → %AppData%\FitnessMealTracker\meals.json
2. JsonMealRepository is created
3. MealService is created (with repository)
4. NutritionCalculator is created
5. MainViewModel is created (with service and calculator)
6. DataContext = MainViewModel (connected to UI)
```

### 2️⃣ **Add Meal Operation**

```
User clicks "Add Meal" button
    ↓
MainViewModel.AddMealCommand executes
    ↓
MainViewModel.AddMeal() method
    ↓
MealService.AddMeal(newMeal)
    ↓
DailySummary.AddMeal(meal)
    ↓
MainViewModel.RefreshMeals() → UI is updated
```

### 3️⃣ **Add Food Item Operation**

```
User selects a meal and enters food information
    ↓
Clicks "Add Food Item" button
    ↓
MainViewModel.AddFoodItemCommand executes
    ↓
MainViewModel.AddFoodItem() method
    ↓
FoodItem is created
    ↓
SelectedMeal.AddFoodItem(newFoodItem)
    ↓
MainViewModel.RefreshMeals() → Totals are recalculated
    ↓
UI is automatically updated (thanks to PropertyChanged events)
```

### 4️⃣ **Save Data Operation**

```
User clicks "Save Meals" button
    ↓
MainViewModel.SaveMealsCommand executes
    ↓
MainViewModel.SaveMeals() method
    ↓
MealService.SaveMeals()
    ↓
DailySummary.Meals list is retrieved
    ↓
JsonMealRepository.SaveMeals(meals)
    ↓
Serialized to JSON format
    ↓
Written to %AppData%\FitnessMealTracker\meals.json file
```

### 5️⃣ **Load Data Operation**

```
User clicks "Load Meals" button
    ↓
MainViewModel.LoadMealsCommand executes
    ↓
MainViewModel.LoadMeals() method
    ↓
MealService.LoadMeals()
    ↓
JsonMealRepository.LoadMeals()
    ↓
meals.json file is read
    ↓
Deserialized from JSON → Meal list
    ↓
DailySummary.ClearMeals() → Old data is cleared
    ↓
New data is added to DailySummary
    ↓
MainViewModel.RefreshMeals() → UI is updated
```

### 6️⃣ **Nutritional Value Calculation**

```
When user adds food
    ↓
Meal.TotalCalories (sum of FoodItems)
    ↓
DailySummary.TotalCalories (sum of Meals)
    ↓
MainViewModel.TotalCalories (retrieved from DailySummary)
    ↓
Displayed in UI
```

### 7️⃣ **Remaining Goal Calculation**

```
MainViewModel.RemainingCalories property is called
    ↓
NutritionCalculator.CalculateRemainingCalories()
    ↓
Formula: UserGoal.DailyCalorieGoal - DailySummary.TotalCalories
    ↓
Result is displayed in UI
```

---

## 💾 Data Storage

### 📍 **File Location**

Data is stored at:
```
Windows: %AppData%\FitnessMealTracker\meals.json
Full Path: C:\Users\[Username]\AppData\Roaming\FitnessMealTracker\meals.json
```

### 📄 **File Format**

Data is stored in **JSON (JavaScript Object Notation)** format:

```json
[
  {
    "Name": "Breakfast",
    "Items": [
      {
        "Name": "Egg",
        "Calories": 70.0,
        "Protein": 6.0,
        "Carbs": 0.6,
        "Fat": 5.0
      },
      {
        "Name": "Bread",
        "Calories": 80.0,
        "Protein": 3.0,
        "Carbs": 15.0,
        "Fat": 1.0
      }
    ]
  },
  {
    "Name": "Lunch",
    "Items": [
      {
        "Name": "Chicken Breast",
        "Calories": 165.0,
        "Protein": 31.0,
        "Carbs": 0.0,
        "Fat": 3.6
      }
    ]
  }
]
```

### 🔧 **File Operations**

- **Saving**: `JsonMealRepository.SaveMeals()` → `File.WriteAllText()`
- **Loading**: `JsonMealRepository.LoadMeals()` → `File.ReadAllText()`
- **Serialization**: Newtonsoft.Json library is used
- **Error Cases**: 
  - If file doesn't exist → Returns empty list
  - JSON error → Throws exception
  - Access error → Throws exception

---

## 🎯 MVVM Pattern (Model-View-ViewModel)

This project uses the **MVVM (Model-View-ViewModel)** design pattern:

- **Model**: `Meal`, `FoodItem`, `DailySummary`, `UserGoal` (in Core layer)
- **View**: `MainWindow.xaml` (UI definitions)
- **ViewModel**: `MainViewModel` (Business logic and data binding)

### Benefits of MVVM:
- ✅ UI and business logic are separated (separation of concerns)
- ✅ Testability increases
- ✅ Code duplication decreases
- ✅ Automatic UI updates through data binding

---

## 🔗 Dependency Injection

The project uses a simple DI structure:

```csharp
// In MainWindow.xaml.cs:
IMealRepository mealRepository = new JsonMealRepository(dataFilePath);
IMealService mealService = new MealService(mealRepository);
INutritionCalculator nutritionCalculator = new NutritionCalculator();
DataContext = new MainViewModel(mealService, nutritionCalculator);
```

### Benefits:
- ✅ Easily replaceable implementations thanks to interfaces
- ✅ Testability (mock objects can be used)
- ✅ Loose coupling

---

## 📊 Data Flow Diagram

```
┌─────────────┐
│   UI (XAML) │
└──────┬──────┘
       │ Data Binding
       ↓
┌─────────────┐
│ MainViewModel│
└──────┬──────┘
       │
       ├──→ MealService ──→ DailySummary ──→ Meal ──→ FoodItem
       │
       └──→ NutritionCalculator ──→ UserGoal
       │
       └──→ MealService ──→ JsonMealRepository ──→ meals.json
```

---

## 🧪 Test Project

The `FitnessMealTracker.Tests` project contains unit tests:
- Model tests (FoodItem, Meal, DailySummary, etc.)
- Repository tests (JsonMealRepository)
- Service tests (MealService, NutritionCalculator)

---

## 🚀 Usage Scenario

1. **Application Opens**: Existing data is loaded (if available)
2. **Add Meal**: New meal is added with "Add Meal" (e.g., "Breakfast")
3. **Add Food**: Food items are added to selected meal (e.g., "Egg", 70 calories)
4. **View Totals**: Daily totals and remaining goals are displayed in the right panel
5. **Save**: Data is saved to JSON file with "Save Meals"
6. **Close and Open**: When application is closed and reopened, data is loaded with "Load Meals"

---

## 🎨 UI Features

- **Modern Design**: Colorful, emoji-rich, user-friendly interface
- **Responsive**: Flexible structure with Grid layout
- **Data Binding**: Automatic UI updates
- **Command Pattern**: Commands for buttons
- **Validation**: Empty fields and negative values are checked

---

## 📝 Summary

This project, using modern software development principles:
- ✅ Organized with **3-Layer Architecture**
- ✅ UI and logic separated with **MVVM Pattern**
- ✅ Flexible structure with **Interfaces**
- ✅ Data storage with **JSON file**
- ✅ Reactive UI created with **PropertyChanged**
- ✅ User interactions managed with **Command Pattern**

**Data Storage Location**: `%AppData%\FitnessMealTracker\meals.json`

---

## 🔍 Important Notes

1. **Data Persistence**: Data is only saved when "Save Meals" button is clicked
2. **Auto-Load**: There is no automatic loading when application opens, manual "Load Meals" is required
3. **Daily Summary**: All meals are aggregated in a single daily summary
4. **Goals**: Default goals are hardcoded (2000 calories, 150g protein, etc.)

---

**Prepared by**: AI Assistant  
**Date**: 2024  
**Project**: Fitness Meal Tracker - Final Project

