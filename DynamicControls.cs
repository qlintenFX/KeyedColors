using System;
using System.Windows.Forms;

namespace KeyedColors
{
    public class DynamicControls
    {
        // Constants for adjustment steps
        public const double GAMMA_STEP = 0.05;
        public const double CONTRAST_STEP = 0.05;
        
        // Properties for current values
        public double Gamma { get; private set; }
        public double Contrast { get; private set; }
        public bool IsEnabled { get; set; }
        
        // Event for when values change
        public event EventHandler? ValuesChanged;
        
        // Reference to the display manager for applying changes
        private DisplayManager displayManager;
        
        // Hotkey IDs
        public int HotkeyIdGammaUp { get; private set; } = -1;
        public int HotkeyIdGammaDown { get; private set; } = -1;
        public int HotkeyIdContrastUp { get; private set; } = -1;
        public int HotkeyIdContrastDown { get; private set; } = -1;
        
        // Registry key for saving settings
        private const string SettingsRegistryKey = @"SOFTWARE\KeyedColors";
        private const string DynamicEnabledValue = "DynamicControlsEnabled";
        
        public DynamicControls(DisplayManager displayManager)
        {
            this.displayManager = displayManager;
            
            // Initialize with default values
            Gamma = 1.0;
            Contrast = 0.5;
            IsEnabled = false;
        }
        
        // Adjust gamma value
        public void AdjustGamma(double delta)
        {
            if (!IsEnabled) return;
            
            double newGamma = Gamma + delta;
            // Clamp between 0.3 and 2.8 (application limits)
            newGamma = Math.Max(0.3, Math.Min(2.8, newGamma));
            
            if (newGamma != Gamma)
            {
                Gamma = newGamma;
                ApplySettings();
                ValuesChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        
        // Adjust contrast value
        public void AdjustContrast(double delta)
        {
            if (!IsEnabled) return;
            
            double newContrast = Contrast + delta;
            // Clamp between 0 and 1
            newContrast = Math.Max(0, Math.Min(1, newContrast));
            
            if (newContrast != Contrast)
            {
                Contrast = newContrast;
                ApplySettings();
                ValuesChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        
        // Apply the current settings
        private void ApplySettings()
        {
            if (IsEnabled)
            {
                displayManager.ApplySettings(Gamma, Contrast);
            }
        }
        
        // Set initial values (e.g., from a profile)
        public void SetValues(double gamma, double contrast)
        {
            Gamma = Math.Max(0.3, Math.Min(2.8, gamma));
            Contrast = Math.Max(0, Math.Min(1, contrast));
            
            if (IsEnabled)
            {
                ApplySettings();
                ValuesChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        
        // Register the directional hotkeys
        public void RegisterHotkeys(HotkeyManager hotkeyManager, IntPtr formHandle)
        {
            // Unregister existing hotkeys first
            UnregisterHotkeys(hotkeyManager);
            
            // Shift + Arrow keys
            uint modifiers = HotkeyManager.MOD_SHIFT;
            
            // Register the hotkeys
            HotkeyIdGammaUp = RegisterDynamicHotkey(hotkeyManager, formHandle, Keys.Up, modifiers);
            HotkeyIdGammaDown = RegisterDynamicHotkey(hotkeyManager, formHandle, Keys.Down, modifiers);
            HotkeyIdContrastUp = RegisterDynamicHotkey(hotkeyManager, formHandle, Keys.Right, modifiers);
            HotkeyIdContrastDown = RegisterDynamicHotkey(hotkeyManager, formHandle, Keys.Left, modifiers);
        }
        
        private int RegisterDynamicHotkey(HotkeyManager hotkeyManager, IntPtr formHandle, Keys key, uint modifiers)
        {
            // We use the Windows API directly since our HotkeyManager is designed for profiles
            int id = hotkeyManager.NextHotkeyId;
            if (HotkeyManager.RegisterHotKey(formHandle, id, modifiers, (uint)key))
            {
                hotkeyManager.IncrementHotkeyId();
                return id;
            }
            return -1;
        }
        
        // Unregister all dynamic hotkeys
        public void UnregisterHotkeys(HotkeyManager hotkeyManager)
        {
            if (HotkeyIdGammaUp > 0)
            {
                HotkeyManager.UnregisterHotKey(hotkeyManager.FormHandle, HotkeyIdGammaUp);
                HotkeyIdGammaUp = -1;
            }
            
            if (HotkeyIdGammaDown > 0) 
            {
                HotkeyManager.UnregisterHotKey(hotkeyManager.FormHandle, HotkeyIdGammaDown);
                HotkeyIdGammaDown = -1;
            }
            
            if (HotkeyIdContrastUp > 0)
            {
                HotkeyManager.UnregisterHotKey(hotkeyManager.FormHandle, HotkeyIdContrastUp);
                HotkeyIdContrastUp = -1;
            }
            
            if (HotkeyIdContrastDown > 0)
            {
                HotkeyManager.UnregisterHotKey(hotkeyManager.FormHandle, HotkeyIdContrastDown);
                HotkeyIdContrastDown = -1;
            }
        }
        
        // Process a hotkey message
        public bool ProcessHotkey(IntPtr wParam)
        {
            if (!IsEnabled) return false;
            
            int id = wParam.ToInt32();
            
            if (id == HotkeyIdGammaUp)
            {
                AdjustGamma(GAMMA_STEP);
                return true;
            }
            else if (id == HotkeyIdGammaDown)
            {
                AdjustGamma(-GAMMA_STEP);
                return true;
            }
            else if (id == HotkeyIdContrastUp)
            {
                AdjustContrast(CONTRAST_STEP);
                return true;
            }
            else if (id == HotkeyIdContrastDown)
            {
                AdjustContrast(-CONTRAST_STEP);
                return true;
            }
            
            return false;
        }
    }
} 