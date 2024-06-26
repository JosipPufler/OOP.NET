using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Utilities
{
    public static class FileUtils
    {
        private static string playerPicturesPath = @"./playerPictures.json";

        public static T ReadJson<T>(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string json = reader.ReadToEnd();

                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
            else
            {
                throw new FileNotFoundException("File not found", filePath);
            }
        }

        public static void SavePlayerPicture(Dictionary<string, string> fileDictionary)
        {
            var jsonData = JsonConvert.SerializeObject(fileDictionary);
            File.WriteAllText(playerPicturesPath, jsonData);
        }

        public static Dictionary<string, string> GetPlayerPictures()
        {
            if (!File.Exists(playerPicturesPath))
            {
                return new Dictionary<string, string>();
            }
            else
            {
                return ReadJson<Dictionary<string, string>>(playerPicturesPath);
            }

        }
    }
}
