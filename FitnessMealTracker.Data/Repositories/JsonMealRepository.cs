using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FitnessMealTracker.Core.Models;
using Newtonsoft.Json;

namespace FitnessMealTracker.Data.Repositories
{
    public class JsonMealRepository : IMealRepository
    {
        private readonly string _filePath;

        /// Initializes a new instance of the JsonMealRepository class.
        /// <exception cref="ArgumentNullException">Thrown when filePath is null or empty.</exception>
        public JsonMealRepository(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath), "File path cannot be null or empty.");
            }
            _filePath = filePath;
        }

        /// Loads all meals from the JSON file.
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

        /// Saves the provided meals to the JSON file.
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
