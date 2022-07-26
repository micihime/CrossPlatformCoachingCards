using SQLite;

namespace CoachingCards.Models
{
    public class Intro
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        //textual properties
        public string Heading { get; set; }
        public string Paragraph1 { get; set; }
        public string Paragraph2 { get; set; }
        public string Paragraph3 { get; set; }
        public string Paragraph4 { get; set; }
        public string Paragraph5 { get; set; }
    }
}
