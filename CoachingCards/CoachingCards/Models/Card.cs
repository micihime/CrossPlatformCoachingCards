namespace CoachingCards.Models
{
    public class Card
    {
        #region PROPERTIES

        public int ID { get; set; }

        //textual properties
        public string Heading { get; set; }
        public string Text { get; set; }
        public string Action { get; set; }

        public bool IsLeft { get; set; } //hemisphere
        #endregion

        #region CONSTRUCTOR

        public Card(int id, string heading, string text, string action, bool isLeft)
        {
            ID = id;
            Heading = heading;
            Text = text;
            Action = action;
            IsLeft = isLeft;
        }
        #endregion
    }
}
