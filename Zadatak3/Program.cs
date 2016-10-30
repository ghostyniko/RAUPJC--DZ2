using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak3
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] integers = new[] { 1, 2, 2, 2, 3, 3, 4, 5 };
          /*  string[] strings = integers.GroupBy(i => i)
                                       .Select(group =>
                                       new { Number = group.Key, Occurance = group.Count() })
                                       .Select(Element => String.Format("Broj {0} pojavljuje se {1} puta",
                                       Element.Number, Element.Occurance)).ToArray();
*/
            string[] strings = integers.GroupBy(i => i)
                                       .Select(group =>
                                       String.Format("Broj {0} pojavljuje se {1} puta",
                                       group.Key, group.Count())).ToArray();

            foreach ( var x in strings)
            {
                Console.WriteLine(x);
            }

                                      
                                       
        }
    }
}
