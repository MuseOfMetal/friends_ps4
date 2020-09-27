namespace Shop
{
    class Texts
    {
        private static Texts texts;


        public static void Load()
        {
            if (System.IO.File.Exists("texts.json"))
                using (System.IO.StreamReader reader = new System.IO.StreamReader(@"texts.json"))
                {
                    texts = Newtonsoft.Json.JsonConvert.DeserializeObject<Texts>(reader.ReadToEnd());
                }
        }

        public static void Save()
        {
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(@"texts.json"))
            {
                writer.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(texts));
            }
        }

        public static Texts Get()
        {
            if (texts != null)
                return texts;
            if (System.IO.File.Exists("texts.json"))
            {
                Load();
                return texts;
            }
            texts = new Texts();
            return texts;
        }

        protected Texts()
        {

        }

        public string TextStocks = "null";
        public string TextReviews = "null";
        public string TextHowGoingOrder = "null";
        public string TextP2 = "null";
        public string TextP3 = "null";
        public string TextHowEndOrder = "null";

    }
}
