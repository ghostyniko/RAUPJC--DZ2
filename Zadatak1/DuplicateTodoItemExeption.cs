using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak1
{
    public class DuplicateTodoItemException : Exception
    {
        public DuplicateTodoItemException()
        {
            // System.Environment.Exit(1);
            Environment.Exit(1);
        }

        public DuplicateTodoItemException(String message)
        {
            Console.WriteLine(message);
            //System.Environment.Exit(1);
            Environment.Exit(1);
        }
    }
}
