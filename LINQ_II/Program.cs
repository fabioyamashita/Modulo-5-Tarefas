using System.Linq;
using System.Transactions;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;

namespace LINQ_II
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Retornando o caractere mais frequente
            Console.WriteLine("\n## Retornando o caractere mais frequente em uma string ##");
            string stringEx1 = "aaaadfghthgjjk";
            var mostFrequentChar = stringEx1
                .GroupBy(x => x)
                .OrderByDescending(x => x.Count())
                .First()
                .Key;

            Console.WriteLine($"string: {stringEx1}");
            Console.WriteLine($"Caractere mais frequente: {mostFrequentChar}");

            // Trocando um número por caractere especial no teclado
            Console.WriteLine("\n## Trocando um número por caractere especial no teclado ##");
            Dictionary<char, char> dict = new Dictionary<char, char>();
            dict.Add('0', ')');
            dict.Add('1', '!');
            dict.Add('2', '@');
            dict.Add('3', '#');
            dict.Add('4', '$');
            dict.Add('5', '%');
            dict.Add('6', '¨');
            dict.Add('7', '&');
            dict.Add('8', '*');
            dict.Add('9', '(');

            string stringEx2 = "1234567890";
            var convertToSpecialChar = stringEx2.Select(x => dict[x]).ToArray();

            Console.WriteLine($"string: {stringEx2}");
            Console.WriteLine("Convertido: " + string.Join("",convertToSpecialChar));

            // Embaralhando uma lista ordenada
            Console.WriteLine("\n## Embaralhando uma lista ordenada ##");
            var orderedList = new List<int>() { 1, 2, 3, 4, 5, 6 };

            var random = new Random();
            var randomList = orderedList.OrderBy(x => random.Next()).ToList();

            Console.WriteLine($"Lista ordenada: [{string.Join(" ", orderedList)}]");
            Console.WriteLine($"Lista embaralhada: [{string.Join(" ", randomList)}]");

            // Transpondo uma matriz quadrada
            Console.WriteLine("\n## Transpondo uma matriz quadrada ##");
            int[,] squareMatrix = { { 1, 1, 1, 1 }, { 2, 2, 2, 2 }, { 3, 3, 3, 3 }, { 4, 4, 4, 4 } };

            Console.WriteLine("\nMatriz original");
            for (int i = 0; i < squareMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < squareMatrix.GetLength(1); j++)
                {
                    Console.Write(squareMatrix[i, j] + " ");
                }
                Console.WriteLine();
            }

            int[,] transposedMatrix = new int[squareMatrix.GetLength(0), squareMatrix.GetLength(1)];

            Enumerable.Range(0, transposedMatrix.GetLength(0))
                .Select(x => Enumerable.Range(0, transposedMatrix.GetLength(1))
                .Select(y => transposedMatrix[x, y] = squareMatrix[y, x]));


            //var test2 = Enumerable.Range(0, transposedMatrix.GetLength(0))
            //    .Select(x => Enumerable.Range(0, transposedMatrix.GetLength(1))
            //    .Select(y => squareMatrix[y, x]).ToList())
            //    .ToArray();


            //Console.WriteLine("\nMatriz transposta");
            //for (int i = 0; i < test.GetLength(0); i++)
            //{
            //    for (int j = 0; j < test.GetLength(1); j++)
            //    {
            //        test[i][j] = squareMatrix[j, i];
            //        Console.Write(test[i][j] + " ");
            //    }
            //    Console.WriteLine();
            //}

            Console.WriteLine("\nMatriz transposta");
            for (int i = 0; i < transposedMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < transposedMatrix.GetLength(1); j++)
                {
                    transposedMatrix[i, j] = squareMatrix[j, i];
                    Console.Write(transposedMatrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}