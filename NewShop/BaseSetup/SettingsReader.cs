using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NewShop.BaseSetup;

namespace NewShop.BaseSetup
{

    public class SettingsReader
    {
        public static Settings ReadSettings()
        {
            var setupSettings = new Settings();

            // read JSON directly from a file
            using (StreamReader file = File.OpenText(@"Config\\Settings.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject json = (JObject) JToken.ReadFrom(reader);
                setupSettings = JsonConvert.DeserializeObject<Settings>(json["settings"].ToString());
            }

            return setupSettings;
        }

        public static UserInfo ReadInfo()
        {
            var userSettings = new UserInfo();

            // read JSON directly from a file
            using (StreamReader file = File.OpenText(@"Config\\Settings.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject json = (JObject) JToken.ReadFrom(reader);
                userSettings = JsonConvert.DeserializeObject<UserInfo>(json["userInfo"].ToString());
            }

            return userSettings;
        }
    }
}