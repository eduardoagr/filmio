namespace filmio.Model {
    public class Config {
        public bool enable_advanced_chat { get; set; }
        public bool enable_chat { get; set; }
        public bool enable_hand_raising { get; set; }
    }

    public class Room {
        public string id { get; set; }
        public string name { get; set; }
        public bool api_created { get; set; }
        public string privacy { get; set; }
        public string url { get; set; }
        public DateTime created_at { get; set; }
        public Config config { get; set; }
    }
}
