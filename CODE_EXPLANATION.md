# Detailed Code Explanation - Fitness Meal Tracker

This document explains every file in the project, its purpose, and how it works.

---

## 📁 Project Structure Overview

The project follows a **layered architecture** with clear separation of concerns:

```
FitnessMealTrackerSolution/
├── FitnessMealTracker/          # Presentation Layer (WPF UI)
├── FitnessMealTracker.Business/ # Business Logic Layer
├── FitnessMealTracker.Data/      # Data Access Layer
├── FitnessMealTracker.Core/      # Domain Models Layer
└── FitnessMealTracker.Tests/     # Unit Tests
```

---

## 🎨 **PRESENTATION LAYER** (FitnessMealTracker)

### **App.xaml**
**Purpose:** WPF application entry point and configuration file.

**What it does:**
- Defines the WPF Application class
- Sets `StartupUri="MainWindow.xaml"` - tells WPF which window to show when app starts
- Contains application-wide resources (currently empty, but can hold styles, colors, etc.)

**Key Line:**
```xml
StartupUri="MainWindow.xaml"
```
This automatically opens MainWindow when the app launches.

---

### **App.xaml.cs**
**Purpose:** Code-behind for the Application class. Handles application-level events.

**What it does:**
- **OnStartup method:** Runs when the application starts
- **Error Handling:** Catches unhandled exceptions and shows error messages to the user
  - `AppDomain.CurrentDomain.UnhandledException` - catches general exceptions
  - `DispatcherUnhandledException` - catches UI thread exceptions
- **Purpose:** Prevents the app from crashing silently - shows error dialogs instead

**Why it's important:** Without this, if an error occurs, the app might just close without telling you why.

---

### **MainWindow.xaml**
**Purpose:** Defines the visual layout and UI elements of the main window.

**What it contains:**
1. **Window Resources:** Defines colors, styles, and themes
   - Primary color (green), Secondary color (blue), Accent color (orange)
   - Button styles with hover effects
   - TextBox and ListBox styling

2. **Layout Structure:**
   - **Header:** Green banner with app title
   - **Left Panel:** Meal management section
     - Meals list
     - Add/Remove meal controls
     - Food items list
     - Add/Remove food item controls
   - **Right Panel:** Daily summary section
     - Nutrition totals
     - Remaining to goal calculations
   - **Footer:** Save/Load buttons

3. **Data Binding:** Connects UI elements to ViewModel properties
   - `{Binding Meals}` - displays meals list
   - `{Binding SelectedMeal}` - tracks which meal is selected
   - `{Binding AddMealCommand}` - connects button clicks to commands

**Key Concept:** This is **declarative UI** - you describe what you want, not how to build it step-by-step.

---

### **MainWindow.xaml.cs**
**Purpose:** Code-behind for MainWindow. Sets up dependency injection and initializes the ViewModel.

**What it does:**
1. **Constructor:**
   - Calls `InitializeComponent()` - loads the XAML and creates UI elements
   - Gets the data file path (stores in AppData folder)
   - Creates repository instance (`JsonMealRepository`)
   - Creates service instance (`MealService`)
   - Creates calculator instance (`NutritionCalculator`)
   - Creates ViewModel and sets it as `DataContext` - this connects the UI to the data

2. **GetDataFilePath method:**
   - Creates a folder in `%AppData%\FitnessMealTracker\`
   - Returns path to `meals.json` file
   - Ensures the directory exists

**Why this structure:** This follows **Dependency Injection** pattern - MainWindow creates dependencies and passes them to ViewModel. This makes code testable and maintainable.

---

### **ViewModels/MainViewModel.cs**
**Purpose:** The "brain" of the UI. Connects the View (XAML) to the Model (business logic).

**What it does:**

1. **Properties (Data Binding):**
   - `Meals` - ObservableCollection that the UI watches for changes
   - `SelectedMeal` - Currently selected meal in the list
   - `SelectedMealFoodItems` - Food items in the selected meal
   - `NewMealName`, `NewFoodItemName`, etc. - Input fields for adding new items
   - `TotalCalories`, `TotalProtein`, etc. - Calculated totals (read-only)
   - `RemainingCalories`, etc. - Calculated remaining values

2. **Commands (Button Actions):**
   - `AddMealCommand` - Executes when "Add Meal" button is clicked
   - `RemoveMealCommand` - Executes when "Remove Meal" button is clicked
   - `AddFoodItemCommand` - Executes when "Add Food Item" button is clicked
   - `SaveMealsCommand`, `LoadMealsCommand` - File operations

3. **Methods:**
   - `AddMeal()` - Creates new meal and adds it to the service
   - `AddFoodItem()` - Creates new food item and adds it to selected meal
   - `RefreshMeals()` - Updates the UI when data changes
   - `UpdateSelectedMealFoodItems()` - Updates food items list when meal selection changes

4. **INotifyPropertyChanged:**
   - Implements this interface so the UI automatically updates when properties change
   - When you call `OnPropertyChanged()`, WPF refreshes the bound UI elements

**Key Pattern:** This is the **MVVM (Model-View-ViewModel)** pattern:
- **Model** = Business logic (MealService, NutritionCalculator)
- **View** = UI (MainWindow.xaml)
- **ViewModel** = This file (connects them)

---

### **ViewModels/RelayCommand.cs**
**Purpose:** Implements the `ICommand` interface to make buttons work with MVVM.

**What it does:**
- Wraps a method (like `AddMeal()`) so it can be bound to a button
- `Execute()` - runs the method when button is clicked
- `CanExecute()` - determines if button should be enabled/disabled
- `CanExecuteChanged` - event that fires when button state should update

**Why it exists:** WPF buttons need `ICommand` objects, not direct method calls. This class bridges that gap.

**Example:**
```csharp
AddMealCommand = new RelayCommand(_ => AddMeal(), _ => CanAddMeal());
```
- First parameter: What to do when clicked (call `AddMeal()`)
- Second parameter: When is it allowed (when `CanAddMeal()` returns true)

---

## 🧠 **BUSINESS LOGIC LAYER** (FitnessMealTracker.Business)

### **Services/IMealService.cs**
**Purpose:** Interface (contract) defining what meal management operations are available.

**What it defines:**
- `GetAllMeals()` - Get all meals
- `GetDailySummary()` - Get summary with totals
- `AddMeal(Meal)` - Add a meal
- `RemoveMeal(Meal)` - Remove a meal
- `SaveMeals()` - Save to storage
- `LoadMeals()` - Load from storage

**Why an interface:** Allows you to swap implementations (e.g., database instead of JSON file) without changing other code.

---

### **Services/MealService.cs**
**Purpose:** Implements business logic for meal management.

**What it does:**
1. **Constructor:** Takes a repository (for data storage) and creates a `DailySummary` object
2. **AddMeal/RemoveMeal:** Manages meals in the daily summary
3. **SaveMeals/LoadMeals:** Delegates to repository for persistence
4. **GetDailySummary:** Returns the summary object (used for calculations)

**Key Concept:** This is the **Service Layer** - it contains business rules and orchestrates data operations. It doesn't know about UI or file formats.

---

### **Services/INutritionCalculator.cs**
**Purpose:** Interface for nutrition calculation operations.

**What it defines:**
- `CalculateRemainingCalories()` - How many calories left to reach goal
- `CalculateRemainingProtein()` - How much protein left
- `CalculateRemainingCarbs()` - How many carbs left
- `CalculateRemainingFat()` - How much fat left

**Purpose:** Separates calculation logic so it can be tested and reused.

---

### **Services/NutritionCalculator.cs**
**Purpose:** Calculates remaining nutrients to reach daily goals.

**What it does:**
- Takes a `DailySummary` (what you've eaten) and `UserGoal` (your targets)
- Subtracts consumed from goals to get remaining values
- Returns negative numbers if you've exceeded goals

**Example:**
- Goal: 2000 calories
- Consumed: 500 calories
- Remaining: 1500 calories

---

## 💾 **DATA ACCESS LAYER** (FitnessMealTracker.Data)

### **Repositories/IMealRepository.cs**
**Purpose:** Interface defining how to save/load meals.

**What it defines:**
- `LoadMeals()` - Load meals from storage
- `SaveMeals(IReadOnlyList<Meal>)` - Save meals to storage

**Why an interface:** You could implement this with:
- JSON file (current)
- Database (SQL Server, SQLite)
- Cloud storage (Azure, AWS)
- Without changing other code!

---

### **Repositories/JsonMealRepository.cs**
**Purpose:** Saves and loads meals to/from a JSON file.

**What it does:**

1. **Constructor:** Takes a file path and stores it

2. **LoadMeals():**
   - Checks if file exists
   - Reads JSON text from file
   - Deserializes JSON to `List<Meal>` objects
   - Returns empty list if file doesn't exist or is empty
   - Handles errors (corrupted JSON, file access issues)

3. **SaveMeals():**
   - Creates directory if it doesn't exist
   - Serializes `List<Meal>` to JSON (formatted with indentation)
   - Writes JSON to file
   - Handles errors (disk full, permissions)

**Key Technologies:**
- **Newtonsoft.Json** - Library for JSON serialization/deserialization
- **File I/O** - Reading/writing files

**File Location:** `%AppData%\FitnessMealTracker\meals.json`

---

## 📦 **DOMAIN MODELS LAYER** (FitnessMealTracker.Core)

### **Models/FoodItem.cs**
**Purpose:** Represents a single food item with nutritional information.

**Properties:**
- `Name` - Food name (e.g., "Apple", "Chicken Breast")
- `Calories` - Calories per serving
- `Protein` - Protein in grams
- `Carbs` - Carbohydrates in grams
- `Fat` - Fat in grams

**Validation:**
- All nutrient values must be >= 0 (throws exception if negative)
- Name cannot be null (converts to empty string)

**Constructors:**
- `FoodItem(name, calories, protein, carbs, fat)` - Full constructor
- `FoodItem()` - Default constructor (for JSON deserialization)

**Why it exists:** This is a **domain model** - represents real-world concepts in code.

---

### **Models/Meal.cs**
**Purpose:** Represents a meal containing multiple food items (e.g., "Breakfast", "Lunch").

**Properties:**
- `Name` - Meal name
- `Items` - Read-only list of food items in this meal
- `TotalCalories` - Sum of all food item calories
- `TotalProtein` - Sum of all protein
- `TotalCarbs` - Sum of all carbs
- `TotalFat` - Sum of all fat

**Methods:**
- `AddFoodItem(FoodItem)` - Adds a food item to the meal
- `RemoveFoodItem(FoodItem)` - Removes a food item
- `ClearFoodItems()` - Removes all food items

**Special JSON Handling:**
- `Items` property is marked `[JsonIgnore]` - not serialized directly
- `ItemsForSerialization` is a private property marked `[JsonProperty("Items")]` - used for JSON
- This is needed because `Items` is read-only, but JSON needs to write to it

**Key Concept:** This class encapsulates data AND behavior (totals are calculated automatically).

---

### **Models/DailySummary.cs**
**Purpose:** Represents all meals consumed in a day and their combined totals.

**Properties:**
- `Meals` - Read-only list of all meals for the day
- `TotalCalories` - Sum of calories from all meals
- `TotalProtein` - Sum of protein from all meals
- `TotalCarbs` - Sum of carbs from all meals
- `TotalFat` - Sum of fat from all meals

**Methods:**
- `AddMeal(Meal)` - Adds a meal to the day
- `RemoveMeal(Meal)` - Removes a meal
- `ClearMeals()` - Removes all meals

**Purpose:** Aggregates all meals to show daily totals.

---

### **Models/UserGoal.cs**
**Purpose:** Represents the user's daily nutritional goals.

**Properties:**
- `DailyCalorieGoal` - Target calories per day
- `DailyProteinGoal` - Target protein in grams
- `DailyCarbGoal` - Target carbs in grams
- `DailyFatGoal` - Target fat in grams

**Validation:** All goals must be >= 0

**Purpose:** Stores user's targets for comparison with actual consumption.

---

### **Models/Exercise.cs**
**Purpose:** Represents an exercise activity (currently not used in UI, but available for future features).

**Properties:**
- `Name` - Exercise name (e.g., "Running", "Weight Lifting")
- `CaloriesBurned` - Calories burned during exercise

**Purpose:** Could be used to track exercise and adjust net calories.

---

### **AssemblyInfo.cs**
**Purpose:** WPF-specific assembly metadata.

**What it does:**
- Sets `ThemeInfo` attribute - tells WPF where to find theme resources
- Required for WPF applications to work correctly

**Note:** This is auto-generated but needed for WPF.

---

## 🧪 **TEST LAYER** (FitnessMealTracker.Tests)

### **Models/*Tests.cs**
**Purpose:** Unit tests for model classes.

**What they test:**
- Property validation (negative values throw exceptions)
- Methods work correctly (add, remove, clear)
- Calculations are accurate (totals sum correctly)
- Edge cases (null values, empty collections)

**Example Test:**
```csharp
[Fact]
public void AddFoodItem_WithValidItem_AddsItemToMeal()
{
    Meal meal = new Meal("Breakfast");
    FoodItem item = new FoodItem("Egg", 70.0, 6.0, 0.5, 5.0);
    meal.AddFoodItem(item);
    Assert.Single(meal.Items);
}
```

---

### **Services/*Tests.cs**
**Purpose:** Unit tests for business logic services.

**What they test:**
- Service methods work correctly
- Null parameters throw exceptions
- Calculations are accurate
- Integration with repositories

---

### **Repositories/*Tests.cs**
**Purpose:** Unit tests for data access layer.

**What they test:**
- JSON serialization/deserialization works
- File operations handle errors correctly
- Empty files return empty lists
- Directory creation works

---

## 🔄 **How Everything Works Together**

### **Application Flow:**

1. **App Starts:**
   ```
   App.xaml → App.xaml.cs → MainWindow.xaml
   ```

2. **MainWindow Initializes:**
   ```
   MainWindow.xaml.cs creates:
   - JsonMealRepository (data access)
   - MealService (business logic)
   - NutritionCalculator (calculations)
   - MainViewModel (UI logic)
   ```

3. **User Clicks "Add Meal":**
   ```
   Button → AddMealCommand → MainViewModel.AddMeal() 
   → MealService.AddMeal() → DailySummary.AddMeal()
   → UI updates automatically (data binding)
   ```

4. **User Clicks "Save Meals":**
   ```
   Button → SaveMealsCommand → MainViewModel.SaveMeals()
   → MealService.SaveMeals() → JsonMealRepository.SaveMeals()
   → JSON file written to disk
   ```

### **Data Flow:**
```
UI (XAML) 
  ↕ (Data Binding)
ViewModel (MainViewModel)
  ↕ (Method Calls)
Business Layer (MealService, NutritionCalculator)
  ↕ (Method Calls)
Data Layer (JsonMealRepository)
  ↕ (File I/O)
JSON File (meals.json)
```

---

## 🎯 **Key Design Patterns Used**

1. **MVVM (Model-View-ViewModel):** Separates UI from business logic
2. **Repository Pattern:** Abstracts data access
3. **Dependency Injection:** Dependencies passed in constructors
4. **Interface Segregation:** Small, focused interfaces
5. **Single Responsibility:** Each class has one job
6. **Encapsulation:** Private fields with public properties

---

## 📝 **Summary by File**

| File | Purpose | Key Responsibility |
|------|---------|-------------------|
| **App.xaml** | Application entry point | Starts the app, shows MainWindow |
| **App.xaml.cs** | Error handling | Catches and displays errors |
| **MainWindow.xaml** | UI layout | Defines what the window looks like |
| **MainWindow.xaml.cs** | Window setup | Creates dependencies, connects ViewModel |
| **MainViewModel.cs** | UI logic | Handles user interactions, updates UI |
| **RelayCommand.cs** | Command pattern | Makes buttons work with MVVM |
| **MealService.cs** | Business logic | Manages meals, coordinates operations |
| **NutritionCalculator.cs** | Calculations | Computes remaining nutrients |
| **JsonMealRepository.cs** | Data persistence | Saves/loads meals to JSON file |
| **FoodItem.cs** | Domain model | Represents a food item |
| **Meal.cs** | Domain model | Represents a meal with items |
| **DailySummary.cs** | Domain model | Aggregates all meals for the day |
| **UserGoal.cs** | Domain model | Stores user's targets |

---

This architecture ensures:
- ✅ **Separation of concerns** - Each layer has a clear purpose
- ✅ **Testability** - Each component can be tested independently
- ✅ **Maintainability** - Changes in one layer don't break others
- ✅ **Extensibility** - Easy to add new features (e.g., database storage)

