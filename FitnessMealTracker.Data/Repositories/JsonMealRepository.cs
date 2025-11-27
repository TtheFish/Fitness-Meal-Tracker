using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FitnessMealTracker.Core.Models;
using Newtonsoft.Json;

namespace FitnessMealTracker.Data.Repositories
{
    /// <summary>
    /// Provides JSON file-based persistence for meals.
    /// </summary>
    public class JsonMealRepository : IMealRepository
    {
        private readonly string _filePath;

        /// <summary>
        /// Initializes a new instance of the JsonMealRepository class.
        /// </summary>
        /// <param name="filePath">The path to the JSON file for storing meals.</param>
        /// <exception cref="ArgumentNullException">Thrown when filePath is null or empty.</exception>
        public JsonMealRepository(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath), "File path cannot be null or empty.");
            }
            _filePath = filePath;
        }

        /// <summary>
        /// Loads all meals from the JSON file.
        /// </summary>
        /// <returns>A read-only list of meals. Returns an empty list if the file doesn't exist or is empty.</returns>
        /// <exception cref="IOException">Thrown when an I/O error occurs while reading the file.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when access to the file is denied.</exception>
        public IReadOnlyList<Meal> LoadMeals()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Meal>();
            }

            try
            {
                string jsonContent = File.ReadAllText(_filePath);
                
                if (string.IsNullOrWhiteSpace(jsonContent))
                {
                    return new List<Meal>();
                }

                List<Meal>? meals = JsonConvert.DeserializeObject<List<Meal>>(jsonContent);
                return meals ?? new List<Meal>();
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Failed to deserialize meals from file: {_filePath}", ex);
            }
        }

        /// <summary>
        /// Saves the provided meals to the JSON file.
        /// </summary>
        /// <param name="meals">The list of meals to save.</param>
        /// <exception cref="ArgumentNullException">Thrown when meals is null.</exception>
        /// <exception cref="IOException">Thrown when an I/O error occurs while writing the file.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when access to the file is denied.</exception>
        public void SaveMeals(IReadOnlyList<Meal> meals)
        {
            if (meals == null)
            {
                throw new ArgumentNullException(nameof(meals), "Meals list cannot be null.");
            }

            try
            {
                string directoryPath = Path.GetDirectoryName(_filePath) ?? string.Empty;
                
                if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string jsonContent = JsonConvert.SerializeObject(meals, Formatting.Indented);
                File.WriteAllText(_filePath, jsonContent);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Failed to serialize meals to file: {_filePath}", ex);
            }
        }
    }
}
