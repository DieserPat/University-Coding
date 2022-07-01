using System;

namespace Praktikumsaufgabe_2
{
    class Program
    {
        static void Main(string[] args)
        {
            PrioListe pl = new PrioListe();
            pl.Einfuegen(" 9 Auto waschen", Prioritaet.niedrig);
            pl.Einfuegen(" 6 Milch kaufen");
            pl.Einfuegen(" 2 Brot nicht vergessen", Prioritaet.hoch);
            pl.Einfuegen(" 3 Brief einwerfen", Prioritaet.hoch);
            pl.Einfuegen(" 1 Praktikumsaufgabe hochladen", Prioritaet.top);
            pl.Einfuegen(" 7 Praktikumsergebnisse anschauen");
            pl.Einfuegen(" 4 Muttertagsgeschenk 9.5.", Prioritaet.hoch);
            pl.Einfuegen("10 Weihnachtsgeschenke überlegen", Prioritaet.niedrig);
            pl.Einfuegen(" 8 Listenstrukturen noch einmal durcharbeiten");
            pl.Einfuegen(" 5 Steuererklärung bis 2.8.2021", Prioritaet.hoch);
            pl.Ausgeben();
        }
        class PrioListe
        {
            public LElem head;

            //Methode für mit den angegebenen Params
            public void Einfuegen(string neueAufgabe, Prioritaet prio = Prioritaet.normal)
            {
                /* -direkt neues aufgaben objekt erstellen 
                 * -hangler als pointer und durchgehen der liste
                 * -vorgänger damit das neue objekt vom vorgänger verwiesen wird (als nachfolger)
                 * -bool für die schleife
                 */
                LElem neu = new LElem(neueAufgabe, prio);
                LElem hangler = head;
                LElem vorgaenger;
                bool gefunden = false;

                //wenn es noch keinen head gibt
                if (head == null)
                {
                    head = neu;
                }
                //wenn es nur EIN element gibt und das einzige Element NIEDRIGERE prio hat als das neue --> hinten anfügen
                else if (head.nachfolger == null && head.prioAufgabe < neu.prioAufgabe)
                {
                    head.nachfolger = neu;
                }
                //wenn es nur EIN element gibt und es HÖHERE prio hat als das NEUE --> vorne anfügen
                else if (head.nachfolger == null && head.prioAufgabe > neu.prioAufgabe)
                {
                    neu.nachfolger = head;
                    head = neu;
                }
                //wenn die prio der neuen aufgabe "top" ist oder die neue priorität schon HÖHER als die prio des HEADS --> vorne 
                else if (neu.prioAufgabe == Prioritaet.top || head.prioAufgabe > neu.prioAufgabe)
                {
                    head = neu;
                    neu.nachfolger = hangler;
                }

                //schleife die alle aufgaben durchgeht und sie and der stelle einfügt an der sie vorgesehen bis bool = false
                else
                {
                    while (!gefunden)
                    {
                        vorgaenger = hangler;
                        hangler = hangler.nachfolger;

                        if ((int)hangler.prioAufgabe > (int)neu.prioAufgabe)
                        {
                            vorgaenger.nachfolger = neu;
                            neu.nachfolger = hangler;
                            gefunden = true;
                        }
                        else if (hangler.nachfolger == null)
                        {
                            hangler.nachfolger = neu;
                            gefunden = true;
                        }
                    }
                }
            }

            //ausgabe der anstehenden Aufgabe und löschen(abgearbeitet) der Aufgabe
            public void NaechsteAufgabe()
            {
                if (head != null)
                {
                    Console.WriteLine($"Die nächste Aufgabe ist: {head.aufgabe}");
                    head = head.nachfolger;
                }
            }

            //ausgabe aller aufgaben auf die console
            public void Ausgeben()
            {
                LElem hangler = head;
                while (hangler != null)
                {
                    Console.WriteLine($"{hangler.prioAufgabe,8}: {hangler.aufgabe}");
                    hangler = hangler.nachfolger;
                }
            }

            //class Listenelement
            public class LElem
            {
                //objectverweis "nachfolger"
                public string aufgabe;
                public LElem nachfolger;
                public Prioritaet prioAufgabe;

                //normaler Konstruktor für Listenelement
                public LElem(string aufgabe, Prioritaet prio)
                {
                    this.aufgabe = aufgabe;
                    nachfolger = null;
                    prioAufgabe = prio;
                }
            }
        }

       

        // enum priorität. Hier arbeite ich mit den index wobei top für "0" steht und niedrig für "3"
        enum Prioritaet { top, hoch, normal, niedrig }
    }

}
