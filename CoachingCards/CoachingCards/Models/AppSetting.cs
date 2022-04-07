using SQLite;

namespace CoachingCards.Models
{
    public class AppSetting
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Key { get; set; }
        public int Value { get; set; }
    }
}
