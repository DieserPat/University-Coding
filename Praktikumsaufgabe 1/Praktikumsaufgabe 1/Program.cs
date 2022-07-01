using System;

namespace Praktikumsaufgabe_1
{
    class Program
    {
        static void Main(string[] args)
        {
            CMySentence sentence = new CMySentence("Dies ist ein Test!");
            CMyWord s = new CMyWord(sentence);
            CMyWord t = new CMyWord(sentence[2] + " ");
            Console.WriteLine(s);
            Console.WriteLine(s + t);
            Console.WriteLine(s - t);
            Console.WriteLine(s[" :eraepsekahS"] + "S" + t | !("s" + t + "?"));
            if (s) Console.WriteLine("s ist länger als 20 Zeichen.");
            else Console.WriteLine("s ist höchstens 20 Zeichen lang.");
            s *= 3;
            Console.WriteLine(s);
            if (s) Console.Write("s ist länger als 20 Zeichen ");
            else Console.Write("s ist höchstens 20 Zeichen lang ");
            Console.WriteLine("und enthält " + (int)sentence + " Wörter.");
            if (new CMyWord("ein ") == t) Console.WriteLine("So soll es s" + t);
        }
        class CMyWord
        {
            private string zeichenkette;
            public string Zeichenkette
            {
                get { return zeichenkette; }
                set { zeichenkette = value; }
            }
            public CMyWord()
            {
                zeichenkette = "";
            }
            public CMyWord(String s)
            {
                zeichenkette = s;
            }
            // hier erweitern

            public override string ToString()    //überladen des ToString() operanten
            {
                return Zeichenkette;
            }
            public CMyWord(CMySentence sentence)
            {
                zeichenkette = sentence.ToString();
            }


            // überladen der + operanten die objekte jeweils zusammenrechnen wollen mit verschiedenen parametern
            // return jeweils mit der richtigen seite des strings eingefügt
            public static CMyWord operator +(CMyWord satz1, CMyWord satz2)
            {
                return new CMyWord(satz1.Zeichenkette + satz2.Zeichenkette);
            }
            public static CMyWord operator +(string links, CMyWord satz2)
            {
                return new CMyWord(links + satz2.Zeichenkette);
            }
            public static CMyWord operator +(CMyWord satz2, string rechts)
            {
                return new CMyWord(satz2.Zeichenkette + rechts);
            }



            // "oder" operanten überladen sodass ein string eingefügt wird zwischen den zeichenketten
            public static CMyWord operator |(CMyWord objekt1, CMyWord objekt2)
            {
                return new CMyWord(objekt1.Zeichenkette + "oder " + objekt2.Zeichenkette);
            }


            // 3x den string ausgeben
            public static CMyWord operator *(CMyWord objekt, int zahl)
            {
                CMyWord text = new CMyWord();
                for (int i = 0; i < zahl; i++)
                {
                    text += objekt + " ";
                }
                return text;
            }

            // operator der ein wort abzieht
            public static CMyWord operator -(CMyWord satz1, CMyWord satz2)
            {
                return new CMyWord(satz1.Zeichenkette.Replace(satz2.Zeichenkette, ""));
            }

            //zeichenketten miteinander vergleichen
            public static bool operator ==(CMyWord objekt1, CMyWord objekt2)
            {
                if (objekt1.Zeichenkette == objekt2.Zeichenkette)
                    return true;
                return false;
            }

            //vollständigkeitshalber muss der gegen operator angegeben werden
            public static bool operator !=(CMyWord objekt1, CMyWord objekt2)
            {
                if (objekt1.Zeichenkette != objekt2.Zeichenkette)
                    return true;
                return false;
            }


            //True und False operator der die zeichenkette vergleicht mit der länge 20 (beide müssen dabei sein)
            public static bool operator true(CMyWord w)
            {
                if (w.Zeichenkette.Length > 20)
                {
                    return true;
                }
                return false;
            }
            public static bool operator false(CMyWord w)
            {
                if (w.Zeichenkette.Length <= 20)
                {
                    return true;
                }
                return false;
            }

            // wie bei "oder" bloß als nicht operator wird das wort nicht eingefügt
            public static CMyWord operator !(CMyWord objekt)
            {
                return new CMyWord("nicht " + objekt.Zeichenkette);
            }

            //indexer der den inhalt rückwärts ausgibt 
            public CMyWord this[string text]
            {
                get
                {
                    string textrichtig = "";
                    for (int i = text.Length - 1; i >= 0; i--)
                    {
                        textrichtig += text[i];
                    }
                    return new CMyWord(textrichtig);
                }
            }
        }
        class CMySentence
        {
            CMyWord[] myWords;
            // hier erweitern

            // sentence splitten und in CMyWord speichern
            public CMySentence(string sentence)
            {
                string[] words = sentence.Split(' ');
                myWords = new CMyWord[words.Length];

                for (int i = 0; i < words.Length; i++)
                {
                    myWords[i] = new CMyWord(words[i]);
                }
            }

            // expliziet operator zum zählen der wörter
            public static explicit operator int(CMySentence objekt)
            {
                return objekt.myWords.Length;
            }


            //überladen ToString() der aus dem string array ein normalen string macht
            public override string ToString()
            {
                string ausgabe = "";
                for (int i = 0; i < myWords.Length; i++)
                {
                    ausgabe += myWords[i].Zeichenkette;
                    ausgabe += " ";
                }
                return ausgabe;

            }

            //indexer zum konvertieren eines array indexinhalts in ein string
            public string this[int i]
            {
                get { return myWords[i].ToString(); }
            }

            //ende danke :)
        }
    }
}
