using System.Collections.Generic;
using FitnessMealTracker.Core.Models;

namespace FitnessMealTracker.Data.Repositories
{
    /// <summary>
    /// Defines the contract for meal data persistence operations.
    /// </summary>
    public interface IMealRepository
    {
        /// <summary>
        /// Loads all meals from the data store.
        /// </summary>
        /// <returns>A list of meals. Returns an empty list if no meals are found.</returns>
        /// <exception cref="System.IO.IOException">Thrown when an I/O error occurs while reading.</exception>
        IReadOnlyList<Meal> LoadMeals();

        /// <summary>
        /// Saves the provided meals to the data store.
        /// </summary>
        /// <param name="meals">The list of meals to save.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when meals is null.</exception>
        /// <exception cref="System.IO.IOException">Thrown when an I/O error occurs while writing.</exception>
        void SaveMeals(IReadOnlyList<Meal> meals);
    }
}
