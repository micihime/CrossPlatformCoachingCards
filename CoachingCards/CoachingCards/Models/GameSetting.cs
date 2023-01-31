using SQLite;

namespace CoachingCards.Models
{
    //game settings - deck
    public class GameSetting
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Key { get; set; }
        public int Value { get; set; }
    }
}
