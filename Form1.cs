using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;
using Microsoft.Win32;

namespace KeyedColors;

public partial class Form1 : Form
{
    private ProfileManager? profileManager;
    private DisplayManager? displayManager;
    private HotkeyManager? hotkeyManager;
    private NotifyIcon? trayIcon;
    private Profile? currentProfile;
    private bool isMinimized = false;
    private string logPath;
    private const string StartupRegistryKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
    private const string ApplicationName = "KeyedColors";
    private const string SettingsRegistryKey = @"SOFTWARE\KeyedColors";
    private const string MinimizeToTrayValue = "MinimizeToTray";
    private bool minimizeToTray = true; // Default to true for backward compatibility

    // For handling WM_HOTKEY messages
    private const int WM_HOTKEY = 0x0312;

    public Form1()
    {
        // Set up logging
        logPath = GetWritableLogPath("keyedcolors_form_log.txt");
        LogMessage("Form1 constructor starting");
        
        try
        {
            LogMessage("Calling InitializeComponent");
            InitializeComponent();
            LogMessage("InitializeComponent completed");

            // Initialize managers
            LogMessage("Creating ProfileManager");
            profileManager = new ProfileManager();
            LogMessage("ProfileManager created");
            
            LogMessage("Creating DisplayManager");
            displayManager = new DisplayManager();
            LogMessage("DisplayManager created");
            
            // Setup UI after load
            LogMessage("Setting up Form events");
            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;
            LogMessage("Form1 constructor completed successfully");
        }
        catch (Exception ex)
        {
            LogMessage($"ERROR in Form1 constructor: {ex.Message}");
            LogMessage(ex.StackTrace);
            if (ex.InnerException != null)
            {
                LogMessage($"Inner exception: {ex.InnerException.Message}");
                LogMessage(ex.InnerException.StackTrace);
            }
            MessageBox.Show($"Error initializing the application: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void Form1_Load(object? sender, EventArgs e)
    {
        LogMessage("Form1_Load started");
        try
        {
            // Initialize the hotkey manager after the form is loaded (handle is available)
            LogMessage("Creating HotkeyManager");
            hotkeyManager = new HotkeyManager(this.Handle);
            hotkeyManager.HotkeyPressed += HotkeyManager_HotkeyPressed;
            LogMessage("HotkeyManager created");

            // Set up tray icon
            LogMessage("Setting up tray icon");
            SetupTrayIcon();
            LogMessage("Tray icon setup completed");

            // Load profiles and register hotkeys
            LogMessage("Loading profiles to UI");
            LoadProfilesToUI();
            LogMessage("Profiles loaded");
            
            LogMessage("Registering hotkeys");
            RegisterAllHotkeys();
            LogMessage("Hotkeys registered");
            
            // Load startup setting
            UpdateStartWithWindowsCheckbox();
            
            // Load minimize to tray setting
            LoadMinimizeToTraySetting();
            
            LogMessage("Form1_Load completed successfully");
        }
        catch (Exception ex)
        {
            LogMessage($"ERROR in Form1_Load: {ex.Message}");
            LogMessage(ex.StackTrace);
            if (ex.InnerException != null)
            {
                LogMessage($"Inner exception: {ex.InnerException.Message}");
                LogMessage(ex.InnerException.StackTrace);
            }
            MessageBox.Show($"Error loading the application: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LogMessage(string? message)
    {
        if (string.IsNullOrEmpty(message))
            return;
            
        try
        {
            File.AppendAllText(logPath, $"[{DateTime.Now}] {message}\r\n");
        }
        catch
        {
            // Try once more with a different path if the first attempt fails
            try
            {
                string altPath = Path.Combine(Path.GetTempPath(), "keyedcolors_form_log.txt");
                File.AppendAllText(altPath, $"[{DateTime.Now}] {message}\r\n");
                
                // Update log path for future writes
                logPath = altPath;
            }
            catch
            {
                // Ignore logging failures - don't want to cause cascading errors
            }
        }
    }

    private string GetWritableLogPath(string filename)
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

    private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
    {
        // Unregister all hotkeys
        hotkeyManager?.UnregisterAllHotkeys();
        
        // Reset display settings to original
        displayManager?.ResetToDefault();
        
        // Clean up tray icon
        if (trayIcon != null)
        {
            trayIcon.Visible = false;
            trayIcon.Dispose();
        }
    }

    protected override void WndProc(ref Message m)
    {
        // Listen for hotkey messages
        if (m.Msg == WM_HOTKEY && hotkeyManager != null)
        {
            hotkeyManager.ProcessHotkey(m.WParam);
        }
        
        base.WndProc(ref m);
    }

    private void HotkeyManager_HotkeyPressed(object? sender, HotkeyEventArgs e)
    {
        // Apply the profile settings when hotkey is pressed
        ApplyProfile(e.Profile);
    }

    private void SetupTrayIcon()
    {
        LogMessage("SetupTrayIcon started");
        try
        {
            // Use the application icon
            Icon appIcon;
            
            try
            {
                // First try to use the AppIcon property from Resources
                appIcon = Properties.Resources.AppIcon;
                LogMessage("Loaded icon from Properties.Resources.AppIcon");
            }
            catch (Exception ex)
            {
                LogMessage($"Failed to get icon from Properties.Resources.AppIcon: {ex.Message}");
                
                // Try loading from embedded resources
                try
                {
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    using (Stream? iconStream = assembly.GetManifestResourceStream("KeyedColors.logo.ico"))
                    {
                        if (iconStream != null)
                        {
                            appIcon = new Icon(iconStream);
                            LogMessage("Loaded icon from embedded resource");
                        }
                        else
                        {
                            // Try the file system as a last resort
                            string iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logo.ico");
                            LogMessage($"Trying to load icon from: {iconPath}");
                            
                            if (File.Exists(iconPath))
                            {
                                appIcon = new Icon(iconPath);
                                LogMessage("Loaded icon from file system");
                            }
                            else
                            {
                                // Fall back to executable icon
                                appIcon = Icon.ExtractAssociatedIcon(Application.ExecutablePath) ?? SystemIcons.Application;
                                LogMessage("Using icon from executable");
                            }
                        }
                    }
                }
                catch (Exception exInner)
                {
                    LogMessage($"Failed to get icon from resources: {exInner.Message}");
                    try
                    {
                        appIcon = Icon.ExtractAssociatedIcon(Application.ExecutablePath) ?? SystemIcons.Application;
                        LogMessage("Using icon from executable");
                    }
                    catch
                    {
                        // Fallback to system icon
                        appIcon = SystemIcons.Application;
                        LogMessage("Using system application icon");
                    }
                }
            }
            
            trayIcon = new NotifyIcon
            {
                Icon = appIcon,
                Text = "KeyedColors",
                Visible = true
            };
            LogMessage("NotifyIcon created successfully");

            // Create context menu for tray icon
            ContextMenuStrip menu = new ContextMenuStrip();
            
            // Add profiles submenu
            ToolStripMenuItem profilesMenu = new ToolStripMenuItem("Profiles");
            menu.Items.Add(profilesMenu);
            UpdateTrayProfilesMenu(profilesMenu);

            // Add other menu items
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add("Show", null, ShowForm_Click);
            menu.Items.Add("Exit", null, Exit_Click);

            trayIcon.ContextMenuStrip = menu;
            trayIcon.MouseDoubleClick += TrayIcon_MouseDoubleClick;
            LogMessage("Tray icon setup complete");
        }
        catch (Exception ex)
        {
            LogMessage($"ERROR in SetupTrayIcon: {ex.Message}");
            LogMessage(ex.StackTrace);
            
            try
            {
                // Fallback to system icon if there's an error
                trayIcon = new NotifyIcon
                {
                    Icon = SystemIcons.Application, 
                    Text = "KeyedColors",
                    Visible = true
                };
                
                ContextMenuStrip menu = new ContextMenuStrip();
                menu.Items.Add("Show", null, ShowForm_Click);
                menu.Items.Add("Exit", null, Exit_Click);
                trayIcon.ContextMenuStrip = menu;
                
                LogMessage("Created fallback tray icon with system icon");
            }
            catch (Exception ex2)
            {
                LogMessage($"CRITICAL ERROR: Failed to create fallback tray icon: {ex2.Message}");
                MessageBox.Show("Error setting up system tray icon. Application may not function correctly.", 
                    "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void UpdateTrayProfilesMenu(ToolStripMenuItem profilesMenu)
    {
        if (profileManager == null)
            return;
            
        profilesMenu.DropDownItems.Clear();
        
        foreach (Profile profile in profileManager.Profiles)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(profile.Name);
            item.Tag = profile;
            item.Click += (s, e) => 
            {
                if (s is ToolStripMenuItem menuItem && menuItem.Tag is Profile selectedProfile)
                {
                    ApplyProfile(selectedProfile);
                }
            };
            profilesMenu.DropDownItems.Add(item);
        }
    }

    private void TrayIcon_MouseDoubleClick(object? sender, MouseEventArgs e)
    {
        ShowForm_Click(sender, e);
    }

    private void ShowForm_Click(object? sender, EventArgs e)
    {
        this.Show();
        this.WindowState = FormWindowState.Normal;
        this.Activate();
        isMinimized = false;
    }

    private void Exit_Click(object? sender, EventArgs e)
    {
        Application.Exit();
    }

    private void LoadProfilesToUI()
    {
        if (profileManager == null || profileListBox == null)
            return;
            
        // Clear and populate profiles listbox
        profileListBox.Items.Clear();
        foreach (Profile profile in profileManager.Profiles)
        {
            profileListBox.Items.Add(profile);
        }

        // Select first profile if available
        if (profileListBox.Items.Count > 0)
        {
            profileListBox.SelectedIndex = 0;
        }
    }

    private void RegisterAllHotkeys()
    {
        if (hotkeyManager == null || profileManager == null) return;
        
        // Unregister all existing hotkeys first
        hotkeyManager.UnregisterAllHotkeys();
        
        // Register hotkeys for each profile
        foreach (Profile profile in profileManager.Profiles)
        {
            try
            {
                if (profile.HotKey != Keys.None)
                {
                    int id = hotkeyManager.RegisterHotkey(profile);
                    if (id > 0)
                    {
                        profile.HotkeyId = id;
                    }
                    else
                    {
                        // Reset hotkey if registration failed
                        profile.HotKey = Keys.None;
                        profile.HotKeyModifier = Keys.None;
                    }
                }
            }
            catch (Exception)
            {
                // Handle any exceptions with hotkey registration
                profile.HotKey = Keys.None;
                profile.HotKeyModifier = Keys.None;
                profile.HotkeyId = -1;
            }
        }
    }

    private void ApplyProfile(Profile profile)
    {
        if (profile != null && displayManager != null)
        {
            currentProfile = profile;
            displayManager.ApplySettings(profile.Gamma, profile.Contrast);
            
            // Update UI if form is visible
            if (!isMinimized)
            {
                // Update UI to reflect the current profile
                if (profileListBox != null && profileListBox.Items.Contains(profile))
                {
                    profileListBox.SelectedItem = profile;
                }
                
                // Update sliders
                if (gammaTrackBar != null && contrastTrackBar != null)
                {
                    gammaTrackBar.Value = (int)(profile.Gamma * 100);
                    contrastTrackBar.Value = (int)(profile.Contrast * 100);
                }
                
                // Update label
                UpdateSettingsLabel();
            }
        }
    }

    private void UpdateSettingsLabel()
    {
        if (gammaTrackBar != null && contrastTrackBar != null && settingsLabel != null)
        {
            double gamma = gammaTrackBar.Value / 100.0;
            double contrast = contrastTrackBar.Value;
            
            settingsLabel.Text = $"Gamma: {gamma:F2}, Contrast: {contrast}%";
        }
    }

    private void gammaTrackBar_ValueChanged(object sender, EventArgs e)
    {
        UpdateSettingsLabel();
        
        // Apply settings immediately for preview
        if (displayManager != null && gammaTrackBar != null && contrastTrackBar != null)
        {
            double gamma = gammaTrackBar.Value / 100.0;
            double contrast = contrastTrackBar.Value / 100.0;
            displayManager.ApplySettings(gamma, contrast);
        }
    }

    private void contrastTrackBar_ValueChanged(object sender, EventArgs e)
    {
        UpdateSettingsLabel();
        
        // Apply settings immediately for preview
        if (displayManager != null && gammaTrackBar != null && contrastTrackBar != null)
        {
            double gamma = gammaTrackBar.Value / 100.0;
            double contrast = contrastTrackBar.Value / 100.0;
            displayManager.ApplySettings(gamma, contrast);
        }
    }

    private void profileListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (profileListBox.SelectedItem is Profile selectedProfile)
        {
            // Update UI with selected profile settings
            gammaTrackBar.Value = (int)(selectedProfile.Gamma * 100);
            contrastTrackBar.Value = (int)(selectedProfile.Contrast * 100);
            
            // Update hotkey display
            string hotkeyText = selectedProfile.HotKey != Keys.None ?
                $"{selectedProfile.HotKeyModifier}+{selectedProfile.HotKey}" :
                "None";
                
            hotkeyLabel.Text = $"Hotkey: {hotkeyText}";
            
            // Apply the profile
            ApplyProfile(selectedProfile);
        }
    }

    private void addProfileButton_Click(object sender, EventArgs e)
    {
        if (profileManager == null || gammaTrackBar == null || contrastTrackBar == null)
            return;
            
        // Create a new profile with current settings
        double gamma = gammaTrackBar.Value / 100.0;
        double contrast = contrastTrackBar.Value / 100.0;
        
        string name = "New Profile";
        
        // Show input dialog for profile name
        using (var form = new Form())
        {
            form.Text = "New Profile";
            form.ClientSize = new Size(350, 100);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterParent;
            form.MaximizeBox = false;
            form.MinimizeBox = false;

            Label label = new Label() { Left = 20, Top = 20, Text = "Profile Name:", AutoSize = true };
            TextBox textBox = new TextBox() { Left = 120, Top = 20, Width = 200 };
            textBox.Text = name;
            
            Button confirmButton = new Button() { Text = "OK", Left = 140, Width = 80, Top = 60, DialogResult = DialogResult.OK };
            form.AcceptButton = confirmButton;
            
            form.Controls.Add(label);
            form.Controls.Add(textBox);
            form.Controls.Add(confirmButton);

            if (form.ShowDialog() == DialogResult.OK)
            {
                name = textBox.Text.Trim();
                if (string.IsNullOrEmpty(name))
                {
                    name = "New Profile";
                }
            }
            else
            {
                return; // User canceled
            }
        }

        Profile newProfile = new Profile(name, gamma, contrast);
        profileManager.AddProfile(newProfile);
        
        // Refresh UI
        LoadProfilesToUI();
        profileListBox.SelectedItem = newProfile;
    }

    private void updateProfileButton_Click(object sender, EventArgs e)
    {
        if (profileManager == null || profileListBox == null || 
            gammaTrackBar == null || contrastTrackBar == null)
            return;
            
        if (profileListBox.SelectedItem is Profile selectedProfile)
        {
            // Update the selected profile with current settings
            selectedProfile.Gamma = gammaTrackBar.Value / 100.0;
            selectedProfile.Contrast = contrastTrackBar.Value / 100.0;
            
            profileManager.SaveProfiles();
            
            // Refresh UI
            int selectedIndex = profileListBox.SelectedIndex;
            LoadProfilesToUI();
            profileListBox.SelectedIndex = selectedIndex;
        }
    }

    private void deleteProfileButton_Click(object sender, EventArgs e)
    {
        if (profileManager == null || profileListBox == null)
            return;
            
        if (profileListBox.SelectedItem is Profile selectedProfile)
        {
            if (MessageBox.Show($"Are you sure you want to delete the profile '{selectedProfile.Name}'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Unregister hotkey if it exists
                if (selectedProfile.HotkeyId > 0 && hotkeyManager != null)
                {
                    hotkeyManager.UnregisterHotkey(selectedProfile.HotkeyId);
                }
                
                // Remove profile
                int index = profileListBox.SelectedIndex;
                profileManager.RemoveProfile(index);
                
                // Refresh UI
                LoadProfilesToUI();
                
                // Select adjacent profile
                if (index >= profileListBox.Items.Count)
                {
                    index = profileListBox.Items.Count - 1;
                }
                
                if (index >= 0)
                {
                    profileListBox.SelectedIndex = index;
                }
            }
        }
    }

    private void setHotkeyButton_Click(object sender, EventArgs e)
    {
        if (profileListBox.SelectedItem is Profile selectedProfile && hotkeyManager != null && profileManager != null && hotkeyLabel != null)
        {
            // Create a form for hotkey configuration
            using (var form = new Form())
            {
                form.Text = "Set Hotkey";
                form.ClientSize = new Size(300, 150);
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterParent;
                form.MaximizeBox = false;
                form.MinimizeBox = false;

                Label label = new Label() { Left = 20, Top = 20, Text = "Press a key combination:", AutoSize = true };
                TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 260, ReadOnly = true };
                
                // Show current hotkey
                if (selectedProfile.HotKey != Keys.None)
                {
                    textBox.Text = $"{selectedProfile.HotKeyModifier}+{selectedProfile.HotKey}";
                }
                
                Button confirmButton = new Button() { Text = "OK", Left = 70, Width = 80, Top = 100, DialogResult = DialogResult.OK };
                Button cancelButton = new Button() { Text = "Cancel", Left = 160, Width = 80, Top = 100, DialogResult = DialogResult.Cancel };
                form.AcceptButton = confirmButton;
                form.CancelButton = cancelButton;
                
                Keys modifiers = Keys.None;
                Keys key = Keys.None;
                
                textBox.KeyDown += (s, ke) => 
                {
                    // Capture the key and modifiers
                    modifiers = Keys.None;
                    if (ke.Control) modifiers |= Keys.Control;
                    if (ke.Alt) modifiers |= Keys.Alt;
                    if (ke.Shift) modifiers |= Keys.Shift;
                    
                    key = ke.KeyCode;
                    
                    // At least one modifier is required for a hotkey
                    if (modifiers != Keys.None && key != Keys.None &&
                        key != Keys.ControlKey && key != Keys.ShiftKey && key != Keys.Menu)
                    {
                        textBox.Text = $"{modifiers}+{key}";
                        ke.Handled = true;
                        ke.SuppressKeyPress = true;
                    }
                };
                
                form.Controls.Add(label);
                form.Controls.Add(textBox);
                form.Controls.Add(confirmButton);
                form.Controls.Add(cancelButton);

                if (form.ShowDialog() == DialogResult.OK && key != Keys.None && modifiers != Keys.None)
                {
                    // Unregister old hotkey if it exists
                    if (selectedProfile.HotkeyId > 0)
                    {
                        hotkeyManager.UnregisterHotkey(selectedProfile.HotkeyId);
                    }
                    
                    // Set the new hotkey
                    selectedProfile.HotKey = key;
                    selectedProfile.HotKeyModifier = modifiers;
                    
                    // Register the new hotkey
                    int id = hotkeyManager.RegisterHotkey(selectedProfile);
                    if (id > 0)
                    {
                        selectedProfile.HotkeyId = id;
                        profileManager.SaveProfiles();
                        
                        // Update UI
                        hotkeyLabel.Text = $"Hotkey: {modifiers}+{key}";
                        int selectedIndex = profileListBox.SelectedIndex;
                        LoadProfilesToUI();
                        profileListBox.SelectedIndex = selectedIndex;
                    }
                    else
                    {
                        MessageBox.Show("Failed to register the hotkey. It may be in use by another application.",
                            "Hotkey Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }

    private void resetButton_Click(object sender, EventArgs e)
    {
        // Reset display to default settings
        if (displayManager != null && gammaTrackBar != null && contrastTrackBar != null)
        {
            displayManager.ResetToDefault();
            
            // Update UI to show default values
            gammaTrackBar.Value = 100;
            contrastTrackBar.Value = 50;
            UpdateSettingsLabel();
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (e.CloseReason == CloseReason.UserClosing)
        {
            if (minimizeToTray)
            {
                // Hide instead of close when user clicks X
                e.Cancel = true;
                this.Hide();
                isMinimized = true;
            }
            // If minimizeToTray is false, allow the form to close normally
        }
        else
        {
            base.OnFormClosing(e);
        }
    }

    private void startWithWindowsCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            bool startWithWindows = startWithWindowsCheckBox.Checked;
            SetStartWithWindows(startWithWindows);
        }
        catch (Exception ex)
        {
            LogMessage($"Error changing Start with Windows setting: {ex.Message}");
            MessageBox.Show($"Failed to update startup setting: {ex.Message}", "Settings Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            // Reset checkbox to actual state without triggering event
            startWithWindowsCheckBox.CheckedChanged -= startWithWindowsCheckBox_CheckedChanged;
            UpdateStartWithWindowsCheckbox();
            startWithWindowsCheckBox.CheckedChanged += startWithWindowsCheckBox_CheckedChanged;
        }
    }
    
    private void SetStartWithWindows(bool enabled)
    {
        LogMessage($"Setting Start with Windows to: {enabled}");
        try
        {
            using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(StartupRegistryKey, true))
            {
                if (key == null)
                {
                    LogMessage("Failed to open registry key for startup");
                    return;
                }

                if (enabled)
                {
                    string appPath = Application.ExecutablePath;
                    key.SetValue(ApplicationName, appPath);
                    LogMessage($"Added application to startup: {appPath}");
                }
                else
                {
                    key.DeleteValue(ApplicationName, false);
                    LogMessage("Removed application from startup");
                }
            }
        }
        catch (Exception ex)
        {
            LogMessage($"Registry error: {ex.Message}");
            throw;
        }
    }
    
    private void UpdateStartWithWindowsCheckbox()
    {
        LogMessage("Checking if application is set to start with Windows");
        try
        {
            bool startupEnabled = false;
            
            using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(StartupRegistryKey, false))
            {
                if (key != null)
                {
                    string? value = key.GetValue(ApplicationName) as string;
                    startupEnabled = !string.IsNullOrEmpty(value);
                    LogMessage($"Start with Windows currently set to: {startupEnabled}");
                }
            }
            
            // Update checkbox without triggering event
            startWithWindowsCheckBox.CheckedChanged -= startWithWindowsCheckBox_CheckedChanged;
            startWithWindowsCheckBox.Checked = startupEnabled;
            startWithWindowsCheckBox.CheckedChanged += startWithWindowsCheckBox_CheckedChanged;
        }
        catch (Exception ex)
        {
            LogMessage($"Error checking startup setting: {ex.Message}");
        }
    }

    private void minimizeToTrayCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            minimizeToTray = minimizeToTrayCheckBox.Checked;
            SaveMinimizeToTraySetting(minimizeToTray);
        }
        catch (Exception ex)
        {
            LogMessage($"Error changing minimize to tray setting: {ex.Message}");
            MessageBox.Show($"Failed to update minimize to tray setting: {ex.Message}", "Settings Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            // Reset checkbox to actual state without triggering event
            minimizeToTrayCheckBox.CheckedChanged -= minimizeToTrayCheckBox_CheckedChanged;
            minimizeToTrayCheckBox.Checked = minimizeToTray;
            minimizeToTrayCheckBox.CheckedChanged += minimizeToTrayCheckBox_CheckedChanged;
        }
    }
    
    private void SaveMinimizeToTraySetting(bool enabled)
    {
        LogMessage($"Setting Minimize to Tray to: {enabled}");
        try
        {
            using (RegistryKey? key = Registry.CurrentUser.CreateSubKey(SettingsRegistryKey, true))
            {
                if (key == null)
                {
                    LogMessage("Failed to create/open registry key for settings");
                    return;
                }

                key.SetValue(MinimizeToTrayValue, enabled ? 1 : 0, RegistryValueKind.DWord);
                LogMessage($"Saved Minimize to Tray setting: {enabled}");
            }
        }
        catch (Exception ex)
        {
            LogMessage($"Registry error saving minimize to tray setting: {ex.Message}");
            throw;
        }
    }
    
    private void LoadMinimizeToTraySetting()
    {
        LogMessage("Loading minimize to tray setting");
        try
        {
            using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(SettingsRegistryKey, false))
            {
                if (key != null)
                {
                    object? value = key.GetValue(MinimizeToTrayValue);
                    if (value != null)
                    {
                        minimizeToTray = Convert.ToInt32(value) != 0;
                    }
                    else
                    {
                        // Default to true if setting doesn't exist (backward compatibility)
                        minimizeToTray = true;
                    }
                }
                else
                {
                    // Create the key for future use
                    using (RegistryKey? newKey = Registry.CurrentUser.CreateSubKey(SettingsRegistryKey, true))
                    {
                        if (newKey != null)
                        {
                            newKey.SetValue(MinimizeToTrayValue, minimizeToTray ? 1 : 0, RegistryValueKind.DWord);
                        }
                    }
                }
                
                LogMessage($"Minimize to tray setting loaded: {minimizeToTray}");
                
                // Update checkbox without triggering event
                minimizeToTrayCheckBox.CheckedChanged -= minimizeToTrayCheckBox_CheckedChanged;
                minimizeToTrayCheckBox.Checked = minimizeToTray;
                minimizeToTrayCheckBox.CheckedChanged += minimizeToTrayCheckBox_CheckedChanged;
            }
        }
        catch (Exception ex)
        {
            LogMessage($"Error loading minimize to tray setting: {ex.Message}");
            
            // Use default (true) in case of error
            minimizeToTray = true;
            minimizeToTrayCheckBox.Checked = true;
        }
    }
}
