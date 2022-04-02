using SQLite;

namespace CoachingCards.Models
{
    public class Deck
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int CardID { get; set; }
    }
}