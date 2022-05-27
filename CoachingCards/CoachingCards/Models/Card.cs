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

    public class CardExtended : Card
    {
        public bool IsBacksideUp { get; set; } //is turned backside up
        public string Background { get; set; }
        public string Separator { get; set; }
    }
}