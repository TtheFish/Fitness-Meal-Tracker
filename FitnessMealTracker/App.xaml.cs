using System;
using System.Windows;

namespace FitnessMealTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Handles unhandled exceptions in the application.
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            // Handle unhandled exceptions
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                MessageBox.Show(
                    $"An error occurred: {args.ExceptionObject}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            };
            
            // Handle dispatcher unhandled exceptions
            DispatcherUnhandledException += (sender, args) =>
            {
                MessageBox.Show(
                    $"An error occurred: {args.Exception.Message}\n\n{args.Exception.StackTrace}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                args.Handled = true;
            };
        }
    }
}
