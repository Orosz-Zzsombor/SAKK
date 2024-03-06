using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSakk
{
    public class Jatszma
    {
        List<String> lepesek;
        //todo Állapottér reprezentáció kialakítása V2.0

        /// <summary>
        /// Üres játék létrehozása
        /// </summary>
        public Jatszma()
        {
            lepesek = new List<String>();
        }
        public Jatszma(String fajlSor)
        {
            lepesek = new List<String>();
            foreach (var item in fajlSor.Trim().Split('\t'))
            {
                lepesek.Add(item);
            }
        }

        public int LepesekSzama => lepesek.Count();

        public char Nyertes => LepesekSzama % 2 == 0 ? 's' : 'v';

        //public int HuszarokLepesszama => lepesek.Count(lepes => lepes[0] == 'H');
        public int HuszarokLepesszama => TisztLepesszama('H');

        public int TisztLepesszama(char tisztJele)
        {
            return lepesek.Count(lepes => lepes[0] == tisztJele);
        }

        /// <summary>
        /// todo: Keresse meg mindkét vezér (királynő) utolsó pozícióját és nézze meg, hogy ott ütötték-e ezt a pozíviót? (vmi x poz)
        /// </summary>
        /// 
        public int Gabor => VezertUttotek();

        public int VezertUttotek() 
        {

            int lepesSzamlalo = 0;
            List<String> Vezerek = new List<String>() {"d1","d8"};
            foreach (var lepes in lepesek)
            {
                if (lepes.Contains('V'))
                {
                    if (lepesSzamlalo%2==0) Vezerek[1] =lepes[-2] +Convert.ToString(lepes[-1]);
                    else Vezerek[0] = Convert.ToString(lepes[-2]) + lepes[-1];
                }
                if (lepes.Contains($"x{Vezerek[0]}") || lepes.Contains($"x{Vezerek[1]}"))
                {
                    return lepesSzamlalo;
                }
            }
            return -1;

        }
        public int vezerlepes => Vezerlepes();
        public int Vezerlepes()
        {
            int lepesSzamlalo = 0;
            int lepettMezok = 0;
            List<String> Vezerek = new List<String>() { "d1", "d8" };
            List<char> oszlop = new List<char>() {'a','b','c','d','e','f','g','h'};
            List<int> sor = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
            foreach (var lepes in lepesek)
            {
                if (lepes.Contains('V'))
                {

                    if (lepesSzamlalo % 2 == 0)
                    {
                        int oszlopertek = Math.Abs(oszlop.IndexOf(Vezerek[1][0]) - oszlop.IndexOf(lepes[-2]));
                        int sorertek = Math.Abs(sor.IndexOf(Vezerek[1][1]) - sor.IndexOf(lepes[-2]));
                        var seged = oszlop.IndexOf(Vezerek[1][1]);
                        lepettMezok += oszlopertek;
                        lepettMezok += sorertek;
                        if (oszlopertek !=0 && sorertek!=0) lepettMezok -= oszlopertek;
                        Vezerek[1] = lepes[-2] +Convert.ToString(lepes[-1]);
                    }
                    {
                        var seged1 = oszlop.IndexOf(Vezerek[0][1]);
                        int oszlopertek = Math.Abs(oszlop.IndexOf(Vezerek[0][0]) - oszlop.IndexOf(lepes[-2]));
                        int sorertek = Math.Abs(sor.IndexOf(Vezerek[0][1]) - sor.IndexOf(lepes[-2]));
                        
                        lepettMezok += oszlopertek;
                        lepettMezok += sorertek;
                        if (oszlopertek != 0 && sorertek != 0) lepettMezok -= oszlopertek;
                        Vezerek[0] = lepes[-2] + Convert.ToString(lepes[-1]);
                    } 
                }
                lepesSzamlalo++;

            }
            return lepettMezok;
        }


        public bool kiralymozog() 
        {
            int lepesSzamlalo = 0;
            foreach (var lepes in lepesek)
            {
                if ( (lepesSzamlalo % 2 == 1 && lepes.Contains("K")) || lepesSzamlalo %2==1&& (lepes.Contains("O-O")||lepes.Contains("O-O-O")))
                {
                    return true;
                }

            }
            return false;
        }
        //f)
        public int utesekSzama => lepesek.Count(lepes => lepes.Contains('x'));
        public bool tobbMint =>32-utesekSzama>20?true:false;
    }
}
