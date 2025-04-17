namespace KeyedColors;

using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        try
        {
            // Try several possible logging locations
            string logPath = GetWritableLogPath("keyedcolors_log.txt");
            
            // Log startup
            SafeWriteToLog(logPath, $"[{DateTime.Now}] Application starting...\r\n");
            SafeWriteToLog(logPath, $"[{DateTime.Now}] Base directory: {AppDomain.CurrentDomain.BaseDirectory}\r\n");
            SafeWriteToLog(logPath, $"[{DateTime.Now}] Executable path: {Application.ExecutablePath}\r\n");
            
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            
            SafeWriteToLog(logPath, $"[{DateTime.Now}] Configuration initialized\r\n");
            
            Application.Run(new Form1());
            
            SafeWriteToLog(logPath, $"[{DateTime.Now}] Application closed normally\r\n");
        }
        catch (Exception ex)
        {
            // Try to log the exception to various locations
            string logPath = GetWritableLogPath("keyedcolors_error.txt");
            string errorMsg = $"[{DateTime.Now}] ERROR: {ex.Message}\r\n{ex.StackTrace}\r\n";
            if (ex.InnerException != null)
            {
                errorMsg += $"Inner Exception: {ex.InnerException.Message}\r\n{ex.InnerException.StackTrace}\r\n";
            }
            
            SafeWriteToLog(logPath, errorMsg);
            
            // Show error message to the user
            MessageBox.Show($"The application encountered an error and needs to close.\r\nError details have been saved to:\r\n{logPath}", 
                "KeyedColors Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    
    /// <summary>
    /// Gets a path where we can write log files, trying multiple locations
    /// </summary>
    private static string GetWritableLogPath(string filename)
    {
        // List of potential log locations in order of preference
        string[] potentialPaths = new string[]
        {
            // 1. Application directory
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename),
            
            // 2. Executable directory
            Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) ?? "", filename),
            
            // 3. User's temp directory
            Path.Combine(Path.GetTempPath(), filename),
            
            // 4. User's documents folder
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "KeyedColors", filename),
            
            // 5. Desktop as last resort
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), filename)
        };
        
        foreach (string path in potentialPaths)
        {
            try
            {
                // Ensure directory exists
                string? directory = Path.GetDirectoryName(path);
                if (directory != null && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
                // Test if we can write to this location
                File.AppendAllText(path, "");
                return path;
            }
            catch
            {
                // Try next location if this one fails
                continue;
            }
        }
        
        // If all else fails, return the first path and hope for the best
        return potentialPaths[0];
    }
    
    /// <summary>
    /// Safely writes to a log file, suppressing any exceptions
    /// </summary>
    private static void SafeWriteToLog(string path, string content)
    {
        try
        {
            File.AppendAllText(path, content);
        }
        catch
        {
            // Silently fail - logging should never crash the application
        }
    }
}