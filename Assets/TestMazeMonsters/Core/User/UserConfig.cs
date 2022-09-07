using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;
using Zenject;

namespace TestMazeMonsters.Core.User
{
    public class UserConfig : IInitializable
    {
        private string CONFIG_FILE_NAME = "userconfig.json";
        private string DEFAULT_CONFIG_DIR_NAME = "DefaultConfigs";

        private UserData _userData;
        private string _persistentPath;

        private string PersistentPath
        {
            get
            {
                if (string.IsNullOrEmpty(_persistentPath))
                    _persistentPath = Path.Combine(Application.persistentDataPath, CONFIG_FILE_NAME);
                return _persistentPath;
            }
        }

        public UserData UserData
        {
            get
            {
                if (_userData == null)
                    Load();
                return _userData;
            }
        }

        public void Initialize()
        {
            Load();
        }

        public void Save()
        {
            JsonConvert.DefaultSettings = () =>
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
                return settings;
            };
            File.WriteAllText(PersistentPath,JsonConvert.SerializeObject(_userData));
        }

        private void Load()
        {
            if (_userData != null)
            {
                return;
            }
            if (File.Exists(PersistentPath))
            {
                _userData = JsonConvert.DeserializeObject<UserData>(File.ReadAllText(PersistentPath));
            }
            else
            {
                LoadDefault();
            }
        }

        private void LoadDefault()
        {
            string defaultPath =
                Path.Combine(DEFAULT_CONFIG_DIR_NAME, Path.GetFileNameWithoutExtension(CONFIG_FILE_NAME));
            _userData = JsonConvert.DeserializeObject<UserData>(Resources.Load<TextAsset>(defaultPath).text);
            Save();
        }
    }
}
