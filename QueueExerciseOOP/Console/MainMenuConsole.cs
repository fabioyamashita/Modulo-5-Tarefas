using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueExerciseOOP
{
    internal class MainMenuConsole
    {
        public static void Show()
        {
            Console.WriteLine("\n----------------------------------------");
            Console.WriteLine("\nSelecione uma das opções:");
            Console.WriteLine("1 - Adicionar pessoa na fila regular");
            Console.WriteLine("2 - Adicionar pessoa na fila prioritária");
            Console.WriteLine("3 - Atender pessoa");
            Console.WriteLine("9 - SAIR");
        }

        public static int ChooseOption()
        {
            Console.Write("\nOpção escolhida: ");
            int.TryParse(Console.ReadLine(), out int option);

            return option;
        }
    }
}
