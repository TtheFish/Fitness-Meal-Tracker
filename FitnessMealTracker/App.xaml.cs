using System;
using System.Windows;

namespace FitnessMealTracker
{
    public partial class App : Application
    {
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
            
            // Ensure window is shown even if there are initialization issues
            try
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to start application: {ex.Message}\n\n{ex.StackTrace}",
                    "Startup Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Shutdown();
            }
        }
    }
}
