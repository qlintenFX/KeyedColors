namespace KeyedColors;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        
        this.profileListBox = new System.Windows.Forms.ListBox();
        this.tabControl = new System.Windows.Forms.TabControl();
        this.profilesTab = new System.Windows.Forms.TabPage();
        this.dynamicControlsTab = new System.Windows.Forms.TabPage();
        this.settingsTab = new System.Windows.Forms.TabPage();
        
        this.resetButton = new System.Windows.Forms.Button();
        this.settingsLabel = new System.Windows.Forms.Label();
        this.contrastLabel = new System.Windows.Forms.Label();
        this.gammaLabel = new System.Windows.Forms.Label();
        this.contrastTrackBar = new System.Windows.Forms.TrackBar();
        this.gammaTrackBar = new System.Windows.Forms.TrackBar();
        
        this.hotkeyLabel = new System.Windows.Forms.Label();
        this.setHotkeyButton = new System.Windows.Forms.Button();
        this.deleteProfileButton = new System.Windows.Forms.Button();
        this.updateProfileButton = new System.Windows.Forms.Button();
        this.addProfileButton = new System.Windows.Forms.Button();
        
        this.startWithWindowsCheckBox = new System.Windows.Forms.CheckBox();
        this.minimizeToTrayCheckBox = new System.Windows.Forms.CheckBox();
        
        // Dynamic Controls tab controls
        this.dynamicControlsToggle = new System.Windows.Forms.CheckBox();
        this.dynamicGammaLabel = new System.Windows.Forms.Label();
        this.dynamicContrastLabel = new System.Windows.Forms.Label();
        this.dynamicSettingsLabel = new System.Windows.Forms.Label();
        this.dynamicGammaTrackBar = new System.Windows.Forms.TrackBar();
        this.dynamicContrastTrackBar = new System.Windows.Forms.TrackBar();
        this.dynamicControlsResetButton = new System.Windows.Forms.Button();
        this.dynamicControlsHelpLabel = new System.Windows.Forms.Label();
        this.dynamicSaveToProfileButton = new System.Windows.Forms.Button();
        
        this.tabControl.SuspendLayout();
        this.profilesTab.SuspendLayout();
        this.dynamicControlsTab.SuspendLayout();
        this.settingsTab.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.gammaTrackBar)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dynamicGammaTrackBar)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dynamicContrastTrackBar)).BeginInit();
        this.SuspendLayout();
        
        // tabControl
        // 
        this.tabControl.Controls.Add(this.profilesTab);
        this.tabControl.Controls.Add(this.dynamicControlsTab);
        this.tabControl.Controls.Add(this.settingsTab);
        this.tabControl.Location = new System.Drawing.Point(12, 12);
        this.tabControl.Name = "tabControl";
        this.tabControl.SelectedIndex = 0;
        this.tabControl.Size = new System.Drawing.Size(459, 458);
        this.tabControl.TabIndex = 0;
        
        // profilesTab
        // 
        this.profilesTab.Controls.Add(this.resetButton);
        this.profilesTab.Controls.Add(this.settingsLabel);
        this.profilesTab.Controls.Add(this.contrastLabel);
        this.profilesTab.Controls.Add(this.gammaLabel);
        this.profilesTab.Controls.Add(this.contrastTrackBar);
        this.profilesTab.Controls.Add(this.gammaTrackBar);
        this.profilesTab.Controls.Add(this.hotkeyLabel);
        this.profilesTab.Controls.Add(this.setHotkeyButton);
        this.profilesTab.Controls.Add(this.deleteProfileButton);
        this.profilesTab.Controls.Add(this.updateProfileButton);
        this.profilesTab.Controls.Add(this.addProfileButton);
        this.profilesTab.Controls.Add(this.profileListBox);
        this.profilesTab.Location = new System.Drawing.Point(4, 24);
        this.profilesTab.Name = "profilesTab";
        this.profilesTab.Padding = new System.Windows.Forms.Padding(3);
        this.profilesTab.Size = new System.Drawing.Size(451, 430);
        this.profilesTab.TabIndex = 0;
        this.profilesTab.Text = "Profiles";
        this.profilesTab.UseVisualStyleBackColor = true;
        
        // Dynamic Controls Tab
        // 
        this.dynamicControlsTab.Controls.Add(this.dynamicControlsToggle);
        this.dynamicControlsTab.Controls.Add(this.dynamicGammaLabel);
        this.dynamicControlsTab.Controls.Add(this.dynamicContrastLabel);
        this.dynamicControlsTab.Controls.Add(this.dynamicSettingsLabel);
        this.dynamicControlsTab.Controls.Add(this.dynamicGammaTrackBar);
        this.dynamicControlsTab.Controls.Add(this.dynamicContrastTrackBar);
        this.dynamicControlsTab.Controls.Add(this.dynamicControlsResetButton);
        this.dynamicControlsTab.Controls.Add(this.dynamicControlsHelpLabel);
        this.dynamicControlsTab.Controls.Add(this.dynamicSaveToProfileButton);
        this.dynamicControlsTab.Location = new System.Drawing.Point(4, 24);
        this.dynamicControlsTab.Name = "dynamicControlsTab";
        this.dynamicControlsTab.Padding = new System.Windows.Forms.Padding(3);
        this.dynamicControlsTab.Size = new System.Drawing.Size(451, 430);
        this.dynamicControlsTab.TabIndex = 1;
        this.dynamicControlsTab.Text = "Dynamic Controls";
        this.dynamicControlsTab.UseVisualStyleBackColor = true;
        
        // dynamicControlsToggle
        // 
        this.dynamicControlsToggle.AutoSize = true;
        this.dynamicControlsToggle.Location = new System.Drawing.Point(15, 20);
        this.dynamicControlsToggle.Name = "dynamicControlsToggle";
        this.dynamicControlsToggle.Size = new System.Drawing.Size(184, 19);
        this.dynamicControlsToggle.TabIndex = 0;
        this.dynamicControlsToggle.Text = "Enable Dynamic Controls";
        this.dynamicControlsToggle.UseVisualStyleBackColor = true;
        this.dynamicControlsToggle.CheckedChanged += new System.EventHandler(this.dynamicControlsToggle_CheckedChanged);
        
        // dynamicGammaLabel
        // 
        this.dynamicGammaLabel.AutoSize = true;
        this.dynamicGammaLabel.Location = new System.Drawing.Point(15, 100);
        this.dynamicGammaLabel.Name = "dynamicGammaLabel";
        this.dynamicGammaLabel.Size = new System.Drawing.Size(52, 15);
        this.dynamicGammaLabel.TabIndex = 1;
        this.dynamicGammaLabel.Text = "Gamma:";
        
        // dynamicContrastLabel
        // 
        this.dynamicContrastLabel.AutoSize = true;
        this.dynamicContrastLabel.Location = new System.Drawing.Point(15, 160);
        this.dynamicContrastLabel.Name = "dynamicContrastLabel";
        this.dynamicContrastLabel.Size = new System.Drawing.Size(55, 15);
        this.dynamicContrastLabel.TabIndex = 2;
        this.dynamicContrastLabel.Text = "Contrast:";
        
        // dynamicSettingsLabel
        // 
        this.dynamicSettingsLabel.AutoSize = true;
        this.dynamicSettingsLabel.Location = new System.Drawing.Point(15, 220);
        this.dynamicSettingsLabel.Name = "dynamicSettingsLabel";
        this.dynamicSettingsLabel.Size = new System.Drawing.Size(165, 15);
        this.dynamicSettingsLabel.TabIndex = 3;
        this.dynamicSettingsLabel.Text = "Gamma: 1.00, Contrast: 50%";
        
        // dynamicGammaTrackBar
        // 
        this.dynamicGammaTrackBar.Location = new System.Drawing.Point(75, 100);
        this.dynamicGammaTrackBar.Maximum = 280;
        this.dynamicGammaTrackBar.Minimum = 30;
        this.dynamicGammaTrackBar.Name = "dynamicGammaTrackBar";
        this.dynamicGammaTrackBar.Size = new System.Drawing.Size(350, 45);
        this.dynamicGammaTrackBar.TabIndex = 4;
        this.dynamicGammaTrackBar.TickFrequency = 10;
        this.dynamicGammaTrackBar.Value = 100;
        this.dynamicGammaTrackBar.ValueChanged += new System.EventHandler(this.dynamicGammaTrackBar_ValueChanged);
        
        // dynamicContrastTrackBar
        // 
        this.dynamicContrastTrackBar.Location = new System.Drawing.Point(75, 160);
        this.dynamicContrastTrackBar.Maximum = 100;
        this.dynamicContrastTrackBar.Minimum = 0;
        this.dynamicContrastTrackBar.Name = "dynamicContrastTrackBar";
        this.dynamicContrastTrackBar.Size = new System.Drawing.Size(350, 45);
        this.dynamicContrastTrackBar.TabIndex = 5;
        this.dynamicContrastTrackBar.TickFrequency = 5;
        this.dynamicContrastTrackBar.Value = 50;
        this.dynamicContrastTrackBar.ValueChanged += new System.EventHandler(this.dynamicContrastTrackBar_ValueChanged);
        
        // dynamicControlsResetButton
        // 
        this.dynamicControlsResetButton.Location = new System.Drawing.Point(350, 220);
        this.dynamicControlsResetButton.Name = "dynamicControlsResetButton";
        this.dynamicControlsResetButton.Size = new System.Drawing.Size(75, 23);
        this.dynamicControlsResetButton.TabIndex = 6;
        this.dynamicControlsResetButton.Text = "Reset";
        this.dynamicControlsResetButton.UseVisualStyleBackColor = true;
        this.dynamicControlsResetButton.Click += new System.EventHandler(this.dynamicControlsResetButton_Click);
        
        // dynamicControlsHelpLabel
        // 
        this.dynamicControlsHelpLabel.Location = new System.Drawing.Point(15, 260);
        this.dynamicControlsHelpLabel.Name = "dynamicControlsHelpLabel";
        this.dynamicControlsHelpLabel.Size = new System.Drawing.Size(410, 100);
        this.dynamicControlsHelpLabel.TabIndex = 7;
        this.dynamicControlsHelpLabel.Text = "Use the following hotkeys to adjust settings:\r\n\r\nShift+Up/Down: Adjust Gamma\r\nShift+Left/Right: Adjust Contrast\r\n\r\nThe adjustments will be applied in real-time.";
        
        // dynamicSaveToProfileButton
        // 
        this.dynamicSaveToProfileButton.Location = new System.Drawing.Point(15, 385);
        this.dynamicSaveToProfileButton.Name = "dynamicSaveToProfileButton";
        this.dynamicSaveToProfileButton.Size = new System.Drawing.Size(140, 23);
        this.dynamicSaveToProfileButton.TabIndex = 8;
        this.dynamicSaveToProfileButton.Text = "Save as New Profile";
        this.dynamicSaveToProfileButton.UseVisualStyleBackColor = true;
        this.dynamicSaveToProfileButton.Click += new System.EventHandler(this.dynamicSaveToProfileButton_Click);
        
        // profileListBox
        // 
        this.profileListBox.FormattingEnabled = true;
        this.profileListBox.ItemHeight = 15;
        this.profileListBox.Location = new System.Drawing.Point(6, 6);
        this.profileListBox.Name = "profileListBox";
        this.profileListBox.Size = new System.Drawing.Size(439, 154);
        this.profileListBox.TabIndex = 0;
        this.profileListBox.SelectedIndexChanged += new System.EventHandler(this.profileListBox_SelectedIndexChanged);
        
        // hotkeyLabel
        // 
        this.hotkeyLabel.AutoSize = true;
        this.hotkeyLabel.Location = new System.Drawing.Point(268, 175);
        this.hotkeyLabel.Name = "hotkeyLabel";
        this.hotkeyLabel.Size = new System.Drawing.Size(83, 15);
        this.hotkeyLabel.TabIndex = 5;
        this.hotkeyLabel.Text = "Hotkey: None";
        
        // setHotkeyButton
        // 
        this.setHotkeyButton.Location = new System.Drawing.Point(168, 172);
        this.setHotkeyButton.Name = "setHotkeyButton";
        this.setHotkeyButton.Size = new System.Drawing.Size(94, 23);
        this.setHotkeyButton.TabIndex = 4;
        this.setHotkeyButton.Text = "Set Hotkey";
        this.setHotkeyButton.UseVisualStyleBackColor = true;
        this.setHotkeyButton.Click += new System.EventHandler(this.setHotkeyButton_Click);
        
        // deleteProfileButton
        // 
        this.deleteProfileButton.Location = new System.Drawing.Point(118, 172);
        this.deleteProfileButton.Name = "deleteProfileButton";
        this.deleteProfileButton.Size = new System.Drawing.Size(44, 23);
        this.deleteProfileButton.TabIndex = 3;
        this.deleteProfileButton.Text = "Del";
        this.deleteProfileButton.UseVisualStyleBackColor = true;
        this.deleteProfileButton.Click += new System.EventHandler(this.deleteProfileButton_Click);
        
        // updateProfileButton
        // 
        this.updateProfileButton.Location = new System.Drawing.Point(56, 172);
        this.updateProfileButton.Name = "updateProfileButton";
        this.updateProfileButton.Size = new System.Drawing.Size(56, 23);
        this.updateProfileButton.TabIndex = 2;
        this.updateProfileButton.Text = "Update";
        this.updateProfileButton.UseVisualStyleBackColor = true;
        this.updateProfileButton.Click += new System.EventHandler(this.updateProfileButton_Click);
        
        // addProfileButton
        // 
        this.addProfileButton.Location = new System.Drawing.Point(6, 172);
        this.addProfileButton.Name = "addProfileButton";
        this.addProfileButton.Size = new System.Drawing.Size(44, 23);
        this.addProfileButton.TabIndex = 1;
        this.addProfileButton.Text = "Add";
        this.addProfileButton.UseVisualStyleBackColor = true;
        this.addProfileButton.Click += new System.EventHandler(this.addProfileButton_Click);
        
        // resetButton
        // 
        this.resetButton.Location = new System.Drawing.Point(368, 350);
        this.resetButton.Name = "resetButton";
        this.resetButton.Size = new System.Drawing.Size(75, 23);
        this.resetButton.TabIndex = 11;
        this.resetButton.Text = "Reset";
        this.resetButton.UseVisualStyleBackColor = true;
        this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
        
        // settingsLabel
        // 
        this.settingsLabel.AutoSize = true;
        this.settingsLabel.Location = new System.Drawing.Point(6, 350);
        this.settingsLabel.Name = "settingsLabel";
        this.settingsLabel.Size = new System.Drawing.Size(165, 15);
        this.settingsLabel.TabIndex = 10;
        this.settingsLabel.Text = "Gamma: 1.0, Contrast: 1.0";
        
        // contrastLabel
        // 
        this.contrastLabel.AutoSize = true;
        this.contrastLabel.Location = new System.Drawing.Point(6, 305);
        this.contrastLabel.Name = "contrastLabel";
        this.contrastLabel.Size = new System.Drawing.Size(55, 15);
        this.contrastLabel.TabIndex = 9;
        this.contrastLabel.Text = "Contrast:";
        
        // gammaLabel
        // 
        this.gammaLabel.AutoSize = true;
        this.gammaLabel.Location = new System.Drawing.Point(6, 254);
        this.gammaLabel.Name = "gammaLabel";
        this.gammaLabel.Size = new System.Drawing.Size(52, 15);
        this.gammaLabel.TabIndex = 8;
        this.gammaLabel.Text = "Gamma:";
        
        // contrastTrackBar
        // 
        this.contrastTrackBar.Location = new System.Drawing.Point(67, 305);
        this.contrastTrackBar.Maximum = 100;
        this.contrastTrackBar.Minimum = 0;
        this.contrastTrackBar.Name = "contrastTrackBar";
        this.contrastTrackBar.Size = new System.Drawing.Size(376, 45);
        this.contrastTrackBar.TabIndex = 7;
        this.contrastTrackBar.TickFrequency = 5;
        this.contrastTrackBar.Value = 50;
        this.contrastTrackBar.ValueChanged += new System.EventHandler(this.contrastTrackBar_ValueChanged);
        
        // gammaTrackBar
        // 
        this.gammaTrackBar.Location = new System.Drawing.Point(67, 254);
        this.gammaTrackBar.Maximum = 280;
        this.gammaTrackBar.Minimum = 30;
        this.gammaTrackBar.Name = "gammaTrackBar";
        this.gammaTrackBar.Size = new System.Drawing.Size(376, 45);
        this.gammaTrackBar.TabIndex = 6;
        this.gammaTrackBar.TickFrequency = 10;
        this.gammaTrackBar.Value = 100;
        this.gammaTrackBar.ValueChanged += new System.EventHandler(this.gammaTrackBar_ValueChanged);
        
        // settingsTab
        // 
        this.settingsTab.Controls.Add(this.minimizeToTrayCheckBox);
        this.settingsTab.Controls.Add(this.startWithWindowsCheckBox);
        this.settingsTab.Location = new System.Drawing.Point(4, 24);
        this.settingsTab.Name = "settingsTab";
        this.settingsTab.Padding = new System.Windows.Forms.Padding(3);
        this.settingsTab.Size = new System.Drawing.Size(451, 430);
        this.settingsTab.TabIndex = 2;
        this.settingsTab.Text = "Settings";
        this.settingsTab.UseVisualStyleBackColor = true;
        
        // startWithWindowsCheckBox
        // 
        this.startWithWindowsCheckBox.AutoSize = true;
        this.startWithWindowsCheckBox.Location = new System.Drawing.Point(15, 25);
        this.startWithWindowsCheckBox.Name = "startWithWindowsCheckBox";
        this.startWithWindowsCheckBox.Size = new System.Drawing.Size(129, 19);
        this.startWithWindowsCheckBox.TabIndex = 0;
        this.startWithWindowsCheckBox.Text = "Start with Windows";
        this.startWithWindowsCheckBox.UseVisualStyleBackColor = true;
        this.startWithWindowsCheckBox.CheckedChanged += new System.EventHandler(this.startWithWindowsCheckBox_CheckedChanged);
        
        // minimizeToTrayCheckBox
        // 
        this.minimizeToTrayCheckBox.AutoSize = true;
        this.minimizeToTrayCheckBox.Location = new System.Drawing.Point(15, 50);
        this.minimizeToTrayCheckBox.Name = "minimizeToTrayCheckBox";
        this.minimizeToTrayCheckBox.Size = new System.Drawing.Size(165, 19);
        this.minimizeToTrayCheckBox.TabIndex = 1;
        this.minimizeToTrayCheckBox.Text = "Minimize to tray when closed";
        this.minimizeToTrayCheckBox.UseVisualStyleBackColor = true;
        this.minimizeToTrayCheckBox.CheckedChanged += new System.EventHandler(this.minimizeToTrayCheckBox_CheckedChanged);
        
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(483, 482);
        this.Controls.Add(this.tabControl);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = Properties.Resources.AppIcon;
        this.MaximizeBox = false;
        this.Name = "Form1";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "KeyedColors";
        this.tabControl.ResumeLayout(false);
        this.profilesTab.ResumeLayout(false);
        this.profilesTab.PerformLayout();
        this.dynamicControlsTab.ResumeLayout(false);
        this.settingsTab.ResumeLayout(false);
        this.settingsTab.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.gammaTrackBar)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dynamicGammaTrackBar)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dynamicContrastTrackBar)).EndInit();
        this.ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.ListBox profileListBox;
    private System.Windows.Forms.TabControl tabControl;
    private System.Windows.Forms.TabPage profilesTab;
    private System.Windows.Forms.TabPage dynamicControlsTab;
    private System.Windows.Forms.TabPage settingsTab;
    private System.Windows.Forms.Button deleteProfileButton;
    private System.Windows.Forms.Button updateProfileButton;
    private System.Windows.Forms.Button addProfileButton;
    private System.Windows.Forms.Label contrastLabel;
    private System.Windows.Forms.Label gammaLabel;
    private System.Windows.Forms.TrackBar contrastTrackBar;
    private System.Windows.Forms.TrackBar gammaTrackBar;
    private System.Windows.Forms.Label settingsLabel;
    private System.Windows.Forms.Button resetButton;
    private System.Windows.Forms.Label hotkeyLabel;
    private System.Windows.Forms.Button setHotkeyButton;
    private System.Windows.Forms.CheckBox startWithWindowsCheckBox;
    private System.Windows.Forms.CheckBox minimizeToTrayCheckBox;
    private System.Windows.Forms.CheckBox dynamicControlsToggle;
    private System.Windows.Forms.Label dynamicGammaLabel;
    private System.Windows.Forms.Label dynamicContrastLabel;
    private System.Windows.Forms.Label dynamicSettingsLabel;
    private System.Windows.Forms.TrackBar dynamicGammaTrackBar;
    private System.Windows.Forms.TrackBar dynamicContrastTrackBar;
    private System.Windows.Forms.Button dynamicControlsResetButton;
    private System.Windows.Forms.Label dynamicControlsHelpLabel;
    private System.Windows.Forms.Button dynamicSaveToProfileButton;
}
