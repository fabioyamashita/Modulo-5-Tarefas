using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueExerciseOOP
{
    internal class StandardMessages
    {
        public static void RefreshingPage()
        {
            Console.Clear();
        }
        public static void NoPersonInLine()
        {
            Console.WriteLine("\nNão há pessoas na fila!");
        }

        public static void ExitApp()
        {
            Console.WriteLine("\nVocê está saindo do atendimento. Volte sempre!");
        }

        public static void InvalidNumber()
        {
            Console.WriteLine("\nDigite um número válido!");
        }
    }
}
