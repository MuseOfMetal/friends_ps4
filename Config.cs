using System.IO;

namespace Shop
{
    public class Config
    {
        private static Config config;

        public static Config Get()
        {
            if (config != null)
                return config;
            Load();
            return config;
        }
        public string BotToken { get; set; }
        public long QuestionsGroupId { get; set; }
        public long OrdersGroupId { get; set; }
        public int SuperAdminId { get; set; }

        public static void Load()
        {
            using (StreamReader reader = new StreamReader("config.json"))
            {
                config = Newtonsoft.Json.JsonConvert.DeserializeObject<Config>(reader.ReadToEnd());
            }
        }

        public static void Save()
        {
            using (StreamWriter writer = new StreamWriter("config.json"))
            {
                writer.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(config));
            }
        }
    }
}
