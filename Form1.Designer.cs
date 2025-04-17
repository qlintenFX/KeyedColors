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
        this.profilesGroupBox = new System.Windows.Forms.GroupBox();
        this.hotkeyLabel = new System.Windows.Forms.Label();
        this.setHotkeyButton = new System.Windows.Forms.Button();
        this.deleteProfileButton = new System.Windows.Forms.Button();
        this.updateProfileButton = new System.Windows.Forms.Button();
        this.addProfileButton = new System.Windows.Forms.Button();
        
        this.settingsGroupBox = new System.Windows.Forms.GroupBox();
        this.resetButton = new System.Windows.Forms.Button();
        this.settingsLabel = new System.Windows.Forms.Label();
        this.contrastLabel = new System.Windows.Forms.Label();
        this.gammaLabel = new System.Windows.Forms.Label();
        this.contrastTrackBar = new System.Windows.Forms.TrackBar();
        this.gammaTrackBar = new System.Windows.Forms.TrackBar();
        
        this.profilesGroupBox.SuspendLayout();
        this.settingsGroupBox.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.gammaTrackBar)).BeginInit();
        this.SuspendLayout();
        
        // profileListBox
        // 
        this.profileListBox.FormattingEnabled = true;
        this.profileListBox.ItemHeight = 15;
        this.profileListBox.Location = new System.Drawing.Point(6, 22);
        this.profileListBox.Name = "profileListBox";
        this.profileListBox.Size = new System.Drawing.Size(447, 124);
        this.profileListBox.TabIndex = 0;
        this.profileListBox.SelectedIndexChanged += new System.EventHandler(this.profileListBox_SelectedIndexChanged);
        
        // profilesGroupBox
        // 
        this.profilesGroupBox.Controls.Add(this.hotkeyLabel);
        this.profilesGroupBox.Controls.Add(this.setHotkeyButton);
        this.profilesGroupBox.Controls.Add(this.deleteProfileButton);
        this.profilesGroupBox.Controls.Add(this.updateProfileButton);
        this.profilesGroupBox.Controls.Add(this.addProfileButton);
        this.profilesGroupBox.Controls.Add(this.profileListBox);
        this.profilesGroupBox.Location = new System.Drawing.Point(12, 12);
        this.profilesGroupBox.Name = "profilesGroupBox";
        this.profilesGroupBox.Size = new System.Drawing.Size(459, 195);
        this.profilesGroupBox.TabIndex = 0;
        this.profilesGroupBox.TabStop = false;
        this.profilesGroupBox.Text = "Profiles";
        
        // hotkeyLabel
        // 
        this.hotkeyLabel.AutoSize = true;
        this.hotkeyLabel.Location = new System.Drawing.Point(268, 155);
        this.hotkeyLabel.Name = "hotkeyLabel";
        this.hotkeyLabel.Size = new System.Drawing.Size(83, 15);
        this.hotkeyLabel.TabIndex = 5;
        this.hotkeyLabel.Text = "Hotkey: None";
        
        // setHotkeyButton
        // 
        this.setHotkeyButton.Location = new System.Drawing.Point(168, 152);
        this.setHotkeyButton.Name = "setHotkeyButton";
        this.setHotkeyButton.Size = new System.Drawing.Size(94, 23);
        this.setHotkeyButton.TabIndex = 4;
        this.setHotkeyButton.Text = "Set Hotkey";
        this.setHotkeyButton.UseVisualStyleBackColor = true;
        this.setHotkeyButton.Click += new System.EventHandler(this.setHotkeyButton_Click);
        
        // deleteProfileButton
        // 
        this.deleteProfileButton.Location = new System.Drawing.Point(118, 152);
        this.deleteProfileButton.Name = "deleteProfileButton";
        this.deleteProfileButton.Size = new System.Drawing.Size(44, 23);
        this.deleteProfileButton.TabIndex = 3;
        this.deleteProfileButton.Text = "Del";
        this.deleteProfileButton.UseVisualStyleBackColor = true;
        this.deleteProfileButton.Click += new System.EventHandler(this.deleteProfileButton_Click);
        
        // updateProfileButton
        // 
        this.updateProfileButton.Location = new System.Drawing.Point(56, 152);
        this.updateProfileButton.Name = "updateProfileButton";
        this.updateProfileButton.Size = new System.Drawing.Size(56, 23);
        this.updateProfileButton.TabIndex = 2;
        this.updateProfileButton.Text = "Update";
        this.updateProfileButton.UseVisualStyleBackColor = true;
        this.updateProfileButton.Click += new System.EventHandler(this.updateProfileButton_Click);
        
        // addProfileButton
        // 
        this.addProfileButton.Location = new System.Drawing.Point(6, 152);
        this.addProfileButton.Name = "addProfileButton";
        this.addProfileButton.Size = new System.Drawing.Size(44, 23);
        this.addProfileButton.TabIndex = 1;
        this.addProfileButton.Text = "Add";
        this.addProfileButton.UseVisualStyleBackColor = true;
        this.addProfileButton.Click += new System.EventHandler(this.addProfileButton_Click);
        
        // settingsGroupBox
        // 
        this.settingsGroupBox.Controls.Add(this.resetButton);
        this.settingsGroupBox.Controls.Add(this.settingsLabel);
        this.settingsGroupBox.Controls.Add(this.contrastLabel);
        this.settingsGroupBox.Controls.Add(this.gammaLabel);
        this.settingsGroupBox.Controls.Add(this.contrastTrackBar);
        this.settingsGroupBox.Controls.Add(this.gammaTrackBar);
        this.settingsGroupBox.Location = new System.Drawing.Point(12, 213);
        this.settingsGroupBox.Name = "settingsGroupBox";
        this.settingsGroupBox.Size = new System.Drawing.Size(459, 147);
        this.settingsGroupBox.TabIndex = 1;
        this.settingsGroupBox.TabStop = false;
        this.settingsGroupBox.Text = "Display Settings";
        
        // resetButton
        // 
        this.resetButton.Location = new System.Drawing.Point(378, 118);
        this.resetButton.Name = "resetButton";
        this.resetButton.Size = new System.Drawing.Size(75, 23);
        this.resetButton.TabIndex = 5;
        this.resetButton.Text = "Reset";
        this.resetButton.UseVisualStyleBackColor = true;
        this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
        
        // settingsLabel
        // 
        this.settingsLabel.AutoSize = true;
        this.settingsLabel.Location = new System.Drawing.Point(6, 118);
        this.settingsLabel.Name = "settingsLabel";
        this.settingsLabel.Size = new System.Drawing.Size(165, 15);
        this.settingsLabel.TabIndex = 4;
        this.settingsLabel.Text = "Gamma: 1.0, Contrast: 1.0";
        
        // contrastLabel
        // 
        this.contrastLabel.AutoSize = true;
        this.contrastLabel.Location = new System.Drawing.Point(6, 73);
        this.contrastLabel.Name = "contrastLabel";
        this.contrastLabel.Size = new System.Drawing.Size(55, 15);
        this.contrastLabel.TabIndex = 3;
        this.contrastLabel.Text = "Contrast:";
        
        // gammaLabel
        // 
        this.gammaLabel.AutoSize = true;
        this.gammaLabel.Location = new System.Drawing.Point(6, 22);
        this.gammaLabel.Name = "gammaLabel";
        this.gammaLabel.Size = new System.Drawing.Size(52, 15);
        this.gammaLabel.TabIndex = 2;
        this.gammaLabel.Text = "Gamma:";
        
        // contrastTrackBar
        // 
        this.contrastTrackBar.Location = new System.Drawing.Point(67, 73);
        this.contrastTrackBar.Maximum = 100;
        this.contrastTrackBar.Minimum = 0;
        this.contrastTrackBar.Name = "contrastTrackBar";
        this.contrastTrackBar.Size = new System.Drawing.Size(386, 45);
        this.contrastTrackBar.TabIndex = 1;
        this.contrastTrackBar.TickFrequency = 5;
        this.contrastTrackBar.Value = 50;
        this.contrastTrackBar.ValueChanged += new System.EventHandler(this.contrastTrackBar_ValueChanged);
        
        // gammaTrackBar
        // 
        this.gammaTrackBar.Location = new System.Drawing.Point(67, 22);
        this.gammaTrackBar.Maximum = 280;
        this.gammaTrackBar.Minimum = 30;
        this.gammaTrackBar.Name = "gammaTrackBar";
        this.gammaTrackBar.Size = new System.Drawing.Size(386, 45);
        this.gammaTrackBar.TabIndex = 0;
        this.gammaTrackBar.TickFrequency = 10;
        this.gammaTrackBar.Value = 100;
        this.gammaTrackBar.ValueChanged += new System.EventHandler(this.gammaTrackBar_ValueChanged);
        
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(483, 372);
        this.Controls.Add(this.settingsGroupBox);
        this.Controls.Add(this.profilesGroupBox);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = new System.Drawing.Icon("logo.ico");
        this.MaximizeBox = false;
        this.Name = "Form1";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "KeyedColors";
        this.profilesGroupBox.ResumeLayout(false);
        this.profilesGroupBox.PerformLayout();
        this.settingsGroupBox.ResumeLayout(false);
        this.settingsGroupBox.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.gammaTrackBar)).EndInit();
        this.ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.ListBox profileListBox;
    private System.Windows.Forms.GroupBox profilesGroupBox;
    private System.Windows.Forms.Button deleteProfileButton;
    private System.Windows.Forms.Button updateProfileButton;
    private System.Windows.Forms.Button addProfileButton;
    private System.Windows.Forms.GroupBox settingsGroupBox;
    private System.Windows.Forms.Label contrastLabel;
    private System.Windows.Forms.Label gammaLabel;
    private System.Windows.Forms.TrackBar contrastTrackBar;
    private System.Windows.Forms.TrackBar gammaTrackBar;
    private System.Windows.Forms.Label settingsLabel;
    private System.Windows.Forms.Button resetButton;
    private System.Windows.Forms.Label hotkeyLabel;
    private System.Windows.Forms.Button setHotkeyButton;
}
