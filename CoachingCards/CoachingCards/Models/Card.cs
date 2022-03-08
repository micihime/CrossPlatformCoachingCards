using SQLite;

namespace CoachingCards.Models
{
    public class Card
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        //textual properties
        public string Heading { get; set; }
        public string Text { get; set; }
        public string Action { get; set; }

        public bool IsLeft { get; set; } //hemisphere

    }
}
