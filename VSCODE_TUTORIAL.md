# VS Code Tutorial for Fitness Meal Tracker Project

This guide will help you set up, build, run, test, and debug your C# WPF application in VS Code (or Cursor).

## Table of Contents

1. [Prerequisites](#prerequisites)
2. [Initial Setup](#initial-setup)
3. [Installing Required Extensions](#installing-required-extensions)
4. [Building the Project](#building-the-project)
5. [Running the Application](#running-the-application)
6. [Debugging](#debugging)
7. [Testing](#testing)
8. [Common Tasks](#common-tasks)
9. [Troubleshooting](#troubleshooting)

---

## Prerequisites

Before you begin, ensure you have:

1. **.NET 8.0 SDK** installed
   - Check if installed: Open terminal and run `dotnet --version`
   - If not installed, download from: https://dotnet.microsoft.com/download/dotnet/8.0
   - You should see version `8.0.x` or higher

2. **VS Code or Cursor** installed
   - VS Code: https://code.visualstudio.com/
   - Cursor: https://cursor.sh/

---

## Initial Setup

1. **Open the project folder** in VS Code/Cursor:
   - File → Open Folder
   - Navigate to `FitnessMealTrackerSolution` folder
   - Click "Select Folder"

2. **Restore NuGet packages** (if needed):
   ```bash
   dotnet restore
   ```

---

## Installing Required Extensions

VS Code will prompt you to install recommended extensions when you open the project. If not, follow these steps:

### Method 1: Automatic Installation (Recommended)
1. When you open the project, VS Code should show a notification: "This workspace has extension recommendations"
2. Click "Install All" or "Show Recommendations"
3. Install the following extensions:
   - **C# Dev Kit** (`ms-dotnettools.csdevkit`)
   - **C#** (`ms-dotnettools.csharp`)
   - **.NET Install Tool** (`ms-dotnettools.vscode-dotnet-runtime`)

### Method 2: Manual Installation
1. Press `Ctrl+Shift+X` (or `Cmd+Shift+X` on Mac) to open Extensions view
2. Search for and install:
   - `ms-dotnettools.csdevkit`
   - `ms-dotnettools.csharp`
   - `ms-dotnettools.vscode-dotnet-runtime`

### After Installation
1. **Reload VS Code**: Press `Ctrl+Shift+P` → Type "Reload Window" → Enter
2. **Wait for OmniSharp to initialize**: Look at the bottom-right status bar for "Loading..." or "OmniSharp"
3. The language server will analyze your project (this may take a minute)

---

## Building the Project

### Method 1: Using Terminal
Open the integrated terminal (`Ctrl+`` ` or `View → Terminal`) and run:

```bash
# Build the entire solution
dotnet build FitnessMealTrackerSolution.sln

# Build a specific project
dotnet build FitnessMealTracker/FitnessMealTracker.csproj
```

### Method 2: Using Tasks
1. Press `Ctrl+Shift+P` (Command Palette)
2. Type "Tasks: Run Task"
3. Select "build"

### Method 3: Using Keyboard Shortcut
- The build task is automatically run when you start debugging (F5)

### Build Output
- Success: You'll see "Build succeeded" in the terminal
- Errors: Check the "Problems" panel (`Ctrl+Shift+M`) for compilation errors

---

## Running the Application

### Method 1: Using Terminal
```bash
# Run the WPF application
dotnet run --project FitnessMealTracker/FitnessMealTracker.csproj
```

### Method 2: Using Debugger (Recommended)
1. Press `F5` or go to `Run → Start Debugging`
2. Select ".NET Core Launch (WPF)" from the debug configuration
3. The application will build and launch automatically

### Method 3: Using Watch Mode (Auto-reload on changes)
1. Press `Ctrl+Shift+P`
2. Type "Tasks: Run Task"
3. Select "watch"
4. The app will automatically restart when you save changes

---

## Debugging

### Setting Breakpoints
1. Click in the left margin (gutter) next to a line number
2. A red dot appears indicating a breakpoint
3. You can also press `F9` on the current line

### Start Debugging
1. **Press `F5`** or click the "Run and Debug" icon in the sidebar
2. Select ".NET Core Launch (WPF)" configuration
3. The application will start in debug mode

### Debug Controls
- **Continue** (`F5`): Resume execution
- **Step Over** (`F10`): Execute current line, don't enter functions
- **Step Into** (`F11`): Step into function calls
- **Step Out** (`Shift+F11`): Step out of current function
- **Restart** (`Ctrl+Shift+F5`): Restart debugging session
- **Stop** (`Shift+F5`): Stop debugging

### Debugging Features
- **Variables Panel**: View local variables and their values
- **Watch Panel**: Add expressions to watch
- **Call Stack**: See the call hierarchy
- **Breakpoints Panel**: Manage all breakpoints

### Debug Console
- Access via `View → Debug Console` or the bottom panel
- You can evaluate expressions and run commands here

---

## Testing

### Creating a Test Project

Currently, your solution doesn't have a test project. Here's how to add one:

#### Step 1: Create Test Project
```bash
# Navigate to solution root
cd FitnessMealTrackerSolution

# Create a new xUnit test project
dotnet new xunit -n FitnessMealTracker.Tests

# Add the test project to the solution
dotnet sln add FitnessMealTracker.Tests/FitnessMealTracker.Tests.csproj
```

#### Step 2: Add Project References
```bash
# Add reference to Core project
dotnet add FitnessMealTracker.Tests/FitnessMealTracker.Tests.csproj reference FitnessMealTracker.Core/FitnessMealTracker.Core.csproj

# Add reference to Data project (if needed)
dotnet add FitnessMealTracker.Tests/FitnessMealTracker.Tests.csproj reference FitnessMealTracker.Data/FitnessMealTracker.Data.csproj
```

#### Step 3: Example Test File
Create `FitnessMealTracker.Tests/Repositories/JsonMealRepositoryTests.cs`:

```csharp
using Xunit;
using FitnessMealTracker.Core.Models;
using FitnessMealTracker.Data.Repositories;
using System.IO;

namespace FitnessMealTracker.Tests.Repositories
{
    public class JsonMealRepositoryTests
    {
        [Fact]
        public void LoadMeals_WhenFileDoesNotExist_ReturnsEmptyList()
        {
            // Arrange
            var filePath = "nonexistent.json";
            var repository = new JsonMealRepository(filePath);

            // Act
            var result = repository.LoadMeals();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void SaveMeals_ThenLoadMeals_ReturnsSameMeals()
        {
            // Arrange
            var filePath = Path.GetTempFileName();
            var repository = new JsonMealRepository(filePath);
            var meals = new List<Meal>
            {
                new Meal { /* initialize with test data */ }
            };

            try
            {
                // Act
                repository.SaveMeals(meals);
                var loadedMeals = repository.LoadMeals();

                // Assert
                Assert.Equal(meals.Count, loadedMeals.Count);
            }
            finally
            {
                // Cleanup
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
        }
    }
}
```

### Running Tests

#### Method 1: Using Terminal
```bash
# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --verbosity normal

# Run tests for a specific project
dotnet test FitnessMealTracker.Tests/FitnessMealTracker.Tests.csproj

# Run a specific test
dotnet test --filter "FullyQualifiedName~JsonMealRepositoryTests"
```

#### Method 2: Using Test Explorer
1. Install the **.NET Core Test Explorer** extension (optional but helpful)
2. Open the Test Explorer view (`Ctrl+Shift+T` or `View → Testing`)
3. Click the play button next to individual tests or test classes

#### Method 3: Using CodeLens
- Tests will show "Run Test" and "Debug Test" links above test methods
- Click these links to run individual tests

### Test Output
- Green checkmark: Test passed
- Red X: Test failed
- Yellow circle: Test skipped
- Check the "Test Results" panel for detailed output

---

## Common Tasks

### Restore NuGet Packages
```bash
dotnet restore
```

### Clean Build Artifacts
```bash
# Using terminal
dotnet clean

# Using task
Ctrl+Shift+P → "Tasks: Run Task" → "clean"
```

### Add a NuGet Package
```bash
# Add to a specific project
dotnet add FitnessMealTracker.Data/FitnessMealTracker.Data.csproj package PackageName

# Example: Add Entity Framework
dotnet add FitnessMealTracker.Data/FitnessMealTracker.Data.csproj package Microsoft.EntityFrameworkCore
```

### Format Code
- **Auto-format on save**: Enabled by default (configured in `.vscode/settings.json`)
- **Manual format**: `Shift+Alt+F` (or `Shift+Option+F` on Mac)
- **Format selection**: Select code → `Ctrl+K Ctrl+F`

### Organize Imports
- **On save**: Automatically organized (configured in settings)
- **Manual**: `Ctrl+Shift+P` → "Organize Imports"

### Navigate Code
- **Go to Definition**: `F12` or `Ctrl+Click`
- **Go to Declaration**: `Ctrl+F12`
- **Find References**: `Shift+F12`
- **Go to Symbol**: `Ctrl+T`
- **Go to File**: `Ctrl+P`

### Refactoring
- **Rename Symbol**: `F2`
- **Extract Method**: Select code → `Ctrl+Shift+P` → "Extract Method"
- **Quick Fix**: `Ctrl+.` on errors/warnings

---

## Troubleshooting

### Issue: No IntelliSense or Error Detection

**Solutions:**
1. **Check if extensions are installed**:
   - Press `Ctrl+Shift+X`
   - Search for "C# Dev Kit" and "C#" - ensure they're installed

2. **Restart OmniSharp**:
   - Press `Ctrl+Shift+P`
   - Type "OmniSharp: Restart OmniSharp"
   - Wait for it to reload

3. **Check OmniSharp output**:
   - View → Output
   - Select "OmniSharp Log" from the dropdown
   - Look for errors

4. **Verify .NET SDK**:
   ```bash
   dotnet --version
   ```
   Should show `8.0.x` or higher

### Issue: Build Errors

**Solutions:**
1. **Clean and rebuild**:
   ```bash
   dotnet clean
   dotnet restore
   dotnet build
   ```

2. **Check for missing references**:
   - Open the Problems panel (`Ctrl+Shift+M`)
   - Look for "The type or namespace name 'X' could not be found"
   - Add missing project references or NuGet packages

3. **Verify solution file**:
   - Ensure all projects are included in `FitnessMealTrackerSolution.sln`

### Issue: Application Won't Run

**Solutions:**
1. **Check if it's a WPF app**:
   - WPF apps require Windows
   - Ensure `UseWPF` is set to `true` in `.csproj`

2. **Check build output**:
   ```bash
   dotnet build --verbosity normal
   ```
   Look for errors or warnings

3. **Verify target framework**:
   - Check that `net8.0-windows` is installed:
   ```bash
   dotnet --list-sdks
   ```

### Issue: Tests Not Discovered

**Solutions:**
1. **Ensure test project references test framework**:
   ```bash
   dotnet add FitnessMealTracker.Tests/FitnessMealTracker.Tests.csproj package xunit
   dotnet add FitnessMealTracker.Tests/FitnessMealTracker.Tests.csproj package xunit.runner.visualstudio
   ```

2. **Rebuild test project**:
   ```bash
   dotnet build FitnessMealTracker.Tests/FitnessMealTracker.Tests.csproj
   ```

3. **Check test output**:
   ```bash
   dotnet test --verbosity detailed
   ```

### Issue: Slow Performance

**Solutions:**
1. **Exclude build folders from file watcher** (already configured in `.vscode/settings.json`):
   - `bin/` and `obj/` folders are excluded

2. **Reduce Roslyn analyzers** (if too many):
   - Edit `.vscode/settings.json`
   - Set `"omnisharp.enableRoslynAnalyzers": false` (not recommended)

3. **Close unused files** to reduce memory usage

---

## Useful Keyboard Shortcuts

| Action | Windows/Linux | Mac |
|--------|---------------|-----|
| Open Command Palette | `Ctrl+Shift+P` | `Cmd+Shift+P` |
| Open Terminal | `` Ctrl+` `` | `` Cmd+` `` |
| Go to Definition | `F12` | `F12` |
| Find References | `Shift+F12` | `Shift+F12` |
| Format Document | `Shift+Alt+F` | `Shift+Option+F` |
| Start Debugging | `F5` | `F5` |
| Toggle Breakpoint | `F9` | `F9` |
| Step Over | `F10` | `F10` |
| Step Into | `F11` | `F11` |
| Problems Panel | `Ctrl+Shift+M` | `Cmd+Shift+M` |
| Quick Fix | `Ctrl+.` | `Cmd+.` |
| Rename Symbol | `F2` | `F2` |

---

## Additional Resources

- [.NET Documentation](https://docs.microsoft.com/dotnet/)
- [C# Dev Kit Documentation](https://code.visualstudio.com/docs/languages/csharp)
- [xUnit Documentation](https://xunit.net/)
- [WPF Documentation](https://docs.microsoft.com/dotnet/desktop/wpf/)

---

## Quick Reference Commands

```bash
# Build
dotnet build FitnessMealTrackerSolution.sln

# Run
dotnet run --project FitnessMealTracker/FitnessMealTracker.csproj

# Test
dotnet test

# Clean
dotnet clean

# Restore packages
dotnet restore

# Add new project
dotnet new classlib -n ProjectName

# Add project to solution
dotnet sln add ProjectName/ProjectName.csproj

# Add package
dotnet add ProjectName/ProjectName.csproj package PackageName
```

---

Happy coding! 🚀

