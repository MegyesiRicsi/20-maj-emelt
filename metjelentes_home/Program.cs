using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace metjelentes_home
{
    class C {
        public string telepules, szeleros,ido;
        public int ho,eros,ora;
        public C(string sor){
            var s = sor.Split(' ');
            telepules = s[0];
            ido = s[1].Substring(0, 2) +":"+ s[1].Substring(2, 2);
            szeleros = s[2];
            ho = int.Parse(s[3]);
            eros =int.Parse( szeleros.Substring(3, 2));
            ora = int.Parse(ido.Substring(0,2));
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
                lista.Add(new C( sr.ReadLine()));
            }
            Console.WriteLine("2. feladat");
            Console.Write("Adja meg egy település kódját! Település: ");
            string be = Console.ReadLine().ToUpper();
            var bek = (from sor in lista where be == sor.telepules select sor).Last();
            Console.WriteLine($"Az utolsó mérési adat a megadott településről {bek.ido}-kor érkezett.");
            var mini = (from sor in lista orderby sor.ho select sor);
            Console.WriteLine($"A legalacsonyabb hőmérséklet: {mini.First().telepules} {mini.First().ido} {mini.First().ho} fok. ");
            Console.WriteLine($"A  legmagasabb hőmérséklet: {mini.Last().telepules} {mini.Last().ido} {mini.Last().ho} fok. ");
            Console.WriteLine("4. feladat");
            var csend = (from sor in lista where sor.szeleros == "00000" select (sor.telepules, sor.ido));
            if (csend.Any())
            {
            foreach (var item in csend)
            {
                Console.WriteLine($"{item.telepules} {item.ido}");
            }
            }
            else
            {
                Console.WriteLine("Nem volt szélcsend a mérések idején.");
            }
            var kul = (from sor in lista select sor.telepules).Distinct();
            /* foreach (var item in lista)
             {
                 Console.WriteLine(item.ora);
             }*/
            string[] sz = new string[] { "1", "7", "13", "19" };
            foreach (var item in kul)
            {
                var sad = (from sor in lista  where (sor.ora == 1 || sor.ora == 7 || sor.ora == 13 || sor.ora == 19) && sor.telepules==item select sor.ho);

                Console.WriteLine("---------");

              //  foreach (var itemo in sad)Console.WriteLine(itemo.telepules+" "+itemo.ora);     
                
                var saddes = (from sor in lista
                              where item == sor.telepules
                              orderby sor.ho
                              select sor.ho);
                Console.WriteLine($"{item} Középhőmérséklet: {sad.Sum()/sad.Count()}; Hőmérséklet-ingadozás: {saddes.Last()-saddes.Min()} ");
               
            }

            var sw = new StreamWriter($"{be}.txt");          
            sw.Write(be);
            foreach (var asd in lista)
            {
                if (asd.telepules==be)
                {
                    sw.Write($" \n{asd.ido} ");
                    for (int i = 0; i < asd.eros; i++)
                    {
                        sw.Write($"#");
                    }
                }
            }
            sw.Close();

            Console.ReadKey();
        }
    }
}
