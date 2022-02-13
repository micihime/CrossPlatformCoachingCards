namespace CoachingCards.Models
{
    public class Intro
    {
        public int ID { get; set; }

        //textual properties
        public string Heading { get; set; }
        public string Paragraph1 { get; set; }
        public string Paragraph2 { get; set; }
        public string Paragraph3 { get; set; }

        #region CONSTRUCTOR

        public Intro(int id, string heading, string p1, string p2, string p3)
        {
            ID = id;
            Heading = heading;
            Paragraph1 = p1;
            Paragraph2 = p2;
            Paragraph3 = p3;
        }
        #endregion
    }
}
