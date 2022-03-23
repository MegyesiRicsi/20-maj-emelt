using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace metjelentes
{
    class C
    {
        public string telepules, szel, time,egybe, percstr;
        public int eros, fok, ido,ora,perc;
        public C(string sor)
        {
            var s= sor.Split(' ');
            telepules = s[0];
            ido =int.Parse( s[1]);
            ora = int.Parse(s[1].Substring(0, 2));
            perc = int.Parse(s[1].Substring(2, 2));
            szel = s[2].Substring(0, 3);
            eros =int.Parse( s[2].Substring(3, 2));
            fok =int.Parse( s[3]);
            percstr = s[1].Substring(2, 2);
            time = ora + ":" + percstr;
            egybe = s[2];
           

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var sr = new StreamReader("tavirathu13.txt");
            var lista = new List<C>();
            while (!sr.EndOfStream)
            {
                lista.Add(new C(sr.ReadLine()));                
            }
           /* foreach (var item in lista)
            {
                Console.WriteLine($"{item.telepules} {item.fok}");
            }*/
            Console.Write($"2. feladat\nAdja meg egy település kódját! Település: ");
            string v = Console.ReadLine().ToUpper();
            var se = (from sor in lista where sor.telepules == v select sor);
            Console.WriteLine($"Az utolsó mérési adat a megadott településről {se.Last().ora}:{se.Last().perc}-kor érkezett. ");

            Console.WriteLine("3. feladat");
            var min = (from sor in lista select sor.fok).Min();
            var max = (from sor in lista select sor.fok).Max();
            var minx = (from sor in lista where sor.fok == min select sor).ToList();
            var maxx = (from sor in lista where sor.fok == max select sor).ToList();
            Console.WriteLine($"A legalacsonyabb hőmérséklet: {minx[0].telepules} {minx[0].time} {minx[0].fok} fok.");
            Console.WriteLine($"A legmagasabb hőmérséklet: {maxx[0].telepules} {maxx[0].time} {maxx[0].fok} fok.");

            var csend = (from sor in lista where sor.egybe == "00000" select sor);
            if (csend.Any())
            {
                foreach (var item in csend)
                {
                    Console.WriteLine($"{item.telepules} {item.time}");
                }
            }
            else
            {
                Console.WriteLine("„Nem volt szélcsend a mérések idején.");
            }
            string[] sz = new string[] { "1", "7", "13", "19" };
            var ls = (from sor in lista where sor.ora==1 || sor.ora==7 || sor.ora==13 || sor.ora==19  select sor);

           

            var stat = new Dictionary<string, int>();
            foreach (var item in ls)
            {
                
                
                    if (stat.Keys.Contains(item.telepules))
                    {
                        stat[item.telepules] += item.fok;
                    }
                    else
                    {
                        stat[item.telepules] = item.fok;
                    }                
            }
            var ct = new Dictionary<string, int>();
            foreach (var item in ls)
            {
                if (ct.Keys.Contains(item.telepules))
                {
                    ct[item.telepules] += 1;
                }
                else
                {
                   ct[item.telepules] = 1;
                }
            }
           
            foreach (var item in stat)
            {
                var varos = item.Key;
                  var osszeg = item.Value;
                 //console.Write(ct[varos]);
                  var db = ct[varos];
                      Console.WriteLine($" ez {varos}  {(double)osszeg / db} {osszeg}");
                 
             //   Console.WriteLine("ez "+item.Value+"    "+item.Key);
            }
            
            Console.ReadKey();
        }
    }
}
