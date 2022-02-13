using System.Collections.Generic;

namespace CoachingCards.Models
{
    public static class IntroList
    {
        private static readonly List<Intro> introTexts = new List<Intro>()
        {
            new Intro(1, "Vítejte!", "Držíte v rukách první koučovací karty svého druhu vytvořené podle MindArt Conceptu. Koučovací karty pro ty, kteří chtějí osobnostní růst, ale nevědí, jak začít.", "Klepnutím nebo zatresením si vytáhněte si jednu kartu denně, splňte úkol, je-li na ní nějaký.", "Nejlepších výsledků dosáhnete, pokud si budete zapisovat vytaženou kartu a k ní i samotný průběh vlastní akce. Na co jste díky její realizaci přišli? Co nového jste se díky ní o sobě dozvěděli?"),
            new Intro(2, "O kartách", "Karty jsou rozděleny na dva druhy podle toho, kterou hemisféru podporují. Cílem je aktivitu obou hemisfér dostat do rovnováhy.", "Levá hemisféra je zaměřena na: analytické myšlení, logiku, detaily, fakta, pravidla.", "Pravá hemisféra je zaměřena na: tvořivost, intuici, souvislosti, symboly, významy."),
            new Intro(3, "Poděkování", "Rádi bychom poděkovali všem našim klientům, kteří nás úžasně inspirovali k tématům na kartách. Přejeme hodně zábavy, aha momentů a užitečných zážitků.", "Veronika a Petr Pavelkovi, Institut osobnostního tréninku", "Karty nenahradí služby kouče a terapeuta.")
        };

        public static List<Intro> GetIntro()
        {
            return introTexts;
        }
    }
}
