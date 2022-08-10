using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueExerciseOOP
{
    internal class QueueSummaryConsole
    {
        public static void Print(IQueue queue)
        {
            Console.WriteLine($"--- {queue.CountPeopleInLine()} pessoa(s)");
            if (queue.CountPeopleInLine() > 0)
            {
                Console.WriteLine($"Próximo a ser atendido: {queue.CheckNextInLine()}");
            }
        }

        public static void Print(ICustomersServed customersServed)
        {
            if (customersServed.Count() > 0)
            {
                Console.WriteLine("\nAtendimentos Realizados:");
                for (int i = customersServed.Count() - 1; i >= 0; i--)
                {
                    Console.WriteLine($"{i + 1}º - {customersServed.Customers[i]}");
                }
            }
        }
    }
}
