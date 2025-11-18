using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace KeyedColors
{
    public class ProfileManager
    {
        private List<Profile> profiles;
        private string profilesFilePath;
        
        public List<Profile> Profiles => profiles;

        public ProfileManager()
        {
            profiles = new List<Profile>();
            
            // Set up profiles save path in AppData
            string appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "KeyedColors");
                
            // Create directory if it doesn't exist
            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }
            
            profilesFilePath = Path.Combine(appDataPath, "profiles.json");
            
            // Create default presets if no profiles exist
            if (!File.Exists(profilesFilePath))
            {
                CreateDefaultProfile();
            }
            else
            {
                LoadProfiles();
            }
        }

        public void AddProfile(Profile profile)
        {
            profiles.Add(profile);
            SaveProfiles();
        }

        public void UpdateProfile(int index, Profile profile)
        {
            if (index >= 0 && index < profiles.Count)
            {
                profiles[index] = profile;
                SaveProfiles();
            }
        }

        public void RemoveProfile(int index)
        {
            if (index >= 0 && index < profiles.Count)
            {
                profiles.RemoveAt(index);
                SaveProfiles();
            }
        }

        public void SaveProfiles()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                
                string jsonString = JsonSerializer.Serialize(profiles, options);
                File.WriteAllText(profilesFilePath, jsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving profiles: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProfiles()
        {
            try
            {
                if (File.Exists(profilesFilePath))
                {
                    string jsonString = File.ReadAllText(profilesFilePath);
                    var loadedProfiles = JsonSerializer.Deserialize<List<Profile>>(jsonString);
                    
                    if (loadedProfiles != null)
                    {
                        profiles = loadedProfiles;
                    }
                    else
                    {
                        CreateDefaultProfile();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading profiles: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                CreateDefaultProfile();
            }
        }
        
        private void CreateDefaultProfile()
        {
            // Create default presets
            profiles = new List<Profile>
            {
                new Profile("Default", 1.0, 0.5, 50),
                new Profile("Dark", 0.8, 0.5, 50),
                new Profile("Night Vision", 2.8, 0.6, 65)
            };
            
            SaveProfiles();
        }
    }
} 