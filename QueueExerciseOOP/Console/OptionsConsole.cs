using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueExerciseOOP
{
    internal class OptionsConsole
    {
        public static string? InputRegularPersonName()
        {
            Console.Write("Digite o nome da pessoa: ");
            var nameRegularPerson = Console.ReadLine();
            return nameRegularPerson;
        }

        public static string? InputPriorityPersonName()
        {
            Console.Write("Digite o nome da pessoa: ");
            var namePriorityPerson = Console.ReadLine();
            return namePriorityPerson;
        }
    }
}
