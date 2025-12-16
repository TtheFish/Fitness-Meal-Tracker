using System.Collections.Generic;
using FitnessMealTracker.Core.Models;

namespace FitnessMealTracker.Data.Repositories
{
    public interface IMealRepository
    {
        /// Loads all meals from the data store.
        /// <exception cref="System.IO.IOException">Thrown when an I/O error occurs while reading.</exception>
        IReadOnlyList<Meal> LoadMeals();

        /// Saves the provided meals to the data store.
        /// <exception cref="System.ArgumentNullException">Thrown when meals is null.</exception>
        /// <exception cref="System.IO.IOException">Thrown when an I/O error occurs while writing.</exception>
        void SaveMeals(IReadOnlyList<Meal> meals);
    }
}
