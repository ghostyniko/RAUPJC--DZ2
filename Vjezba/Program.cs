using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Vjezba
    
{
    class Program
    {
        static int broj = 0;
         static void Main(string[] args)
        {
            

            while (broj < 20)
            {
                int stari = broj;
                Povecaj();
                while (stari == broj)
                {
                    Console.WriteLine("Ispisujem broj {0}", broj);
                    Thread.Sleep(1000);
                }

                
            }
            
        }

     /*   private async static  Task Pokreni
        {
            int broj = 0;
            while (broj < 20)
            {
                var novi = broj;
                Console.WriteLine("Prije poziva broj je {0}, a novi je {1}", broj, novi);
                // novi =  Task.Run(() => Povecaj(broj)).Result;
                var dobiveni =Task.Run(() => Povecaj(broj));
            //    Console.WriteLine("Sad sam tamo gdje ne trebam biti i ispisujem {0}. Novi je {1}", broj,novi);
               // Thread.Sleep(5000);
                while  (novi == broj)
                {
                    Console.WriteLine("Ispisujem broj {0}", broj);
                    Console.WriteLine("Novi je {0} ", novi);
                 //   await Task.Delay(1000);
                    novi = dobiveni.Result;
                    Console.WriteLine("Novi je {0} ", novi);

                }
                broj = novi;

            }
        }*/
        private static async void Povecaj()
        {
            await Task.Delay(10000);
            //Console.WriteLine("Sad sam u povecaj i broj je {0}", i);
            //    Thread.Sleep(5000);
            broj++;

        }

        private static async Task GetData()
        {
            Console.WriteLine("Sad sam u getdata");
            
            

        }
    }
}
