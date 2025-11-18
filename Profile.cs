using System;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace KeyedColors
{
    [Serializable]
    public class Profile
    {
        public string Name { get; set; } = string.Empty;
        public double Gamma { get; set; }
        public double Contrast { get; set; }
        public int Vibrance { get; set; } = 50;
        
        // Store hotkey as integers for JSON serialization
        [JsonIgnore]
        public Keys HotKey { get; set; }
        
        [JsonIgnore]
        public Keys HotKeyModifier { get; set; }
        
        // These properties are used for JSON serialization
        public int HotKeyValue 
        { 
            get => (int)HotKey;
            set => HotKey = (Keys)value;
        }
        
        public int HotKeyModifierValue
        {
            get => (int)HotKeyModifier;
            set => HotKeyModifier = (Keys)value;
        }
        
        public int HotkeyId { get; set; }

        public Profile()
        {
            // Default values
            Name = "New Profile";
            Gamma = 1.0;
            Contrast = 0.5;  // 50% is normal contrast
            Vibrance = 50;   // 50% aligns with NVIDIA default
            HotKey = Keys.None;
            HotKeyModifier = Keys.None;
            HotkeyId = -1;
        }

        public Profile(string name, double gamma, double contrast, int vibrance = 50)
        {
            Name = name;
            Gamma = gamma;
            Contrast = contrast;
            Vibrance = vibrance;
            HotKey = Keys.None;
            HotKeyModifier = Keys.None;
            HotkeyId = -1;
        }

        public override string ToString()
        {
            string hotkeyText = HotKey != Keys.None ? 
                $"{HotKeyModifier}+{HotKey}" : 
                "No hotkey";
                
            return $"{Name} (Gamma: {Gamma:F2}, Contrast: {Contrast*100:F0}%, Vibrance: {Vibrance}%, Hotkey: {hotkeyText})";
        }
    }
} 