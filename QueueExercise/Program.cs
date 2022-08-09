using System;
using System.Collections.Generic;

class Program
{
    public static void Main(string[] args)
    {
        var priorityQueue = new Queue<string>();
        var regularQueue = new Queue<string>();
        var peopleServed = new List<string>();
        var countPriority = 0;

        var endApp = false;

        while (!endApp)
        {
            Console.WriteLine($"\nFila prioritária: {priorityQueue.Count} pessoa(s)");
            if (priorityQueue.Count > 0)
            {
                Console.WriteLine($"Próximo da fila prioritária: {priorityQueue.Peek()}");
            }

            Console.WriteLine($"\nFila regular: {regularQueue.Count} pessoa(s)");
            if (regularQueue.Count > 0)
            {
                Console.WriteLine($"Próximo da fila regular: {regularQueue.Peek()}");
            }

            if (peopleServed.Count > 0)
            {
                Console.WriteLine("\nAtendimentos Realizados:");
                for (int i = peopleServed.Count - 1; i >= 0; i--)
                {
                    Console.WriteLine($"{i + 1}º - {peopleServed[i]}");
                }
            }

            Console.WriteLine("\n----------------------------------------");
            Console.WriteLine("\nSelecione uma das opções:");
            Console.WriteLine("1 - Adicionar pessoa na fila regular");
            Console.WriteLine("2 - Adicionar pessoa na fila prioritária");
            Console.WriteLine("3 - Atender pessoa");
            Console.WriteLine("9 - SAIR");

            Console.Write("\nOpção escolhida: ");
            int.TryParse(Console.ReadLine(), out int option);
        
            switch (option)
            {
                case 1:
                    Console.Write("Digite o nome da pessoa: ");
                    var nameRegularPerson = Console.ReadLine();
                    regularQueue.Enqueue(nameRegularPerson);
                    Console.Clear();
                    break;
                case 2:
                    Console.Write("Digite o nome da pessoa: ");
                    var namePriorityPerson = Console.ReadLine();
                    priorityQueue.Enqueue(namePriorityPerson);
                    Console.Clear();
                    break;
                case 3:
                    if (priorityQueue.Count == 0 && regularQueue.Count == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("\nNão há pessoas na fila!");
                    }
                    else if (countPriority < 3 && priorityQueue.Count > 0)
                    {     
                        var priorityPersonServed = priorityQueue.Dequeue();
                        peopleServed.Add("(P) " + priorityPersonServed);
                        countPriority++;
                        Console.Clear();
                    }
                    else
                    {       
                        var regularPersonServed = regularQueue.Dequeue();
                        peopleServed.Add("(R) " + regularPersonServed);
                        countPriority = 0;
                        Console.Clear();
                    }
                    break;
                case 9:
                    Console.WriteLine("\nVocê está saindo do atendimento. Volte sempre!");
                    endApp = true;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("\nDigite um número válido!");
                    break;
            }  
        }
    }
}