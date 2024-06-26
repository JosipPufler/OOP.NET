using DataLayer.Models;
using Newtonsoft.Json;

namespace DataLayer.Utilities
{
    public static class ConfigService
    {
        public static string configPath = "..\\..\\..\\..\\config.json";

        private static Config config = GetConfig();

        public static readonly string type = config.type;

        public static readonly string filePath = config.filePath;

        public static bool HasUserSettings;

        public static UserSettings UserSettings
        {
            get
            {
                return config.userSettings;
            }
            set
            {
                config.userSettings = value;
            }
        }

        private static Config GetConfig()
        {
            if (File.Exists(configPath))
            {
                var config = FileUtils.ReadJson<Config>(configPath);
                if (config.type == null)
                {
                    throw new Exception("No defined repository type");
                }

                if (config.userSettings == null)
                {
                    config.userSettings = new UserSettings();
                    HasUserSettings = false;
                }
                else
                {
                    HasUserSettings = true;
                }
                return config;
            }
            else
            {
                throw new FileNotFoundException("Nema kongif datoteke. Panika.");
            }
        }

        public static bool CheckConfigFile()
        {
            Config readConfig = FileUtils.ReadJson<Config>(configPath);
            if (readConfig.userSettings == null)
            {
                return false;
            }
            string categorySettingsId = readConfig.userSettings.category.ToString();
            string languageSettingsId = readConfig.userSettings.language.ToString();
            if (readConfig.userSettings.categoryName == categorySettingsId &&
                readConfig.userSettings.languageName == languageSettingsId)
            {

                return true;
            }
            return false;
        }

        public static void SaveConfig()
        {
            var jsonData = JsonConvert.SerializeObject(config);
            File.WriteAllText(configPath, jsonData);
        }
    }

    internal class Config
    {
        public string type;
        public string filePath;
        public UserSettings userSettings;
    }
}

