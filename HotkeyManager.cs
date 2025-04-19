using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KeyedColors
{
    public class HotkeyManager
    {
        // Windows API for hotkeys
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        // Modifier keys
        public const uint MOD_ALT = 0x0001;
        public const uint MOD_CONTROL = 0x0002;
        public const uint MOD_SHIFT = 0x0004;
        public const uint MOD_WIN = 0x0008;
        
        // Next hotkey ID
        private int nextHotkeyId = 1;
        
        // Store registered hotkeys
        private Dictionary<int, Profile> registeredHotkeys = new Dictionary<int, Profile>();
        
        // Handle to the form for registering hotkeys
        private IntPtr formHandle;
        
        // Event for when a hotkey is pressed
        public event EventHandler<HotkeyEventArgs>? HotkeyPressed;

        // Properties for DynamicControls
        public int NextHotkeyId => nextHotkeyId;
        public IntPtr FormHandle => formHandle;
        
        public void IncrementHotkeyId()
        {
            nextHotkeyId++;
        }

        public HotkeyManager(IntPtr formHandle)
        {
            this.formHandle = formHandle;
        }

        public int RegisterHotkey(Profile profile)
        {
            if (profile.HotKey == Keys.None)
                return -1;

            // Convert the Keys modifiers to the Windows API modifiers
            uint modifiers = 0;
            if ((profile.HotKeyModifier & Keys.Alt) == Keys.Alt)
                modifiers |= MOD_ALT;
            if ((profile.HotKeyModifier & Keys.Control) == Keys.Control)
                modifiers |= MOD_CONTROL;
            if ((profile.HotKeyModifier & Keys.Shift) == Keys.Shift)
                modifiers |= MOD_SHIFT;

            // Get key code
            uint key = (uint)profile.HotKey;
            
            // Register the hotkey
            int id = nextHotkeyId++;
            if (RegisterHotKey(formHandle, id, modifiers, key))
            {
                registeredHotkeys[id] = profile;
                profile.HotkeyId = id;
                return id;
            }
            
            return -1;
        }

        public bool UnregisterHotkey(int id)
        {
            if (id <= 0)
                return false;
                
            bool result = UnregisterHotKey(formHandle, id);
            if (result && registeredHotkeys.ContainsKey(id))
            {
                registeredHotkeys.Remove(id);
            }
            return result;
        }

        public void UnregisterAllHotkeys()
        {
            foreach (int id in registeredHotkeys.Keys)
            {
                UnregisterHotKey(formHandle, id);
            }
            registeredHotkeys.Clear();
        }

        // Call this method from the form when a hotkey message is received
        public void ProcessHotkey(IntPtr wParam)
        {
            int id = wParam.ToInt32();
            if (registeredHotkeys.TryGetValue(id, out Profile? profile) && profile != null)
            {
                HotkeyPressed?.Invoke(this, new HotkeyEventArgs(profile));
            }
        }
    }

    // Event args for hotkey events
    public class HotkeyEventArgs : EventArgs
    {
        public Profile Profile { get; }

        public HotkeyEventArgs(Profile profile)
        {
            Profile = profile;
        }
    }
} 