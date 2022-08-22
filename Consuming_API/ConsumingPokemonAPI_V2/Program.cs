using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsumingPokemonAPI_V2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var pokemonsTotalCount = createPokedex(1, 151).GetAwaiter().GetResult().Count;
            var pokedex = createPokedex(1, 151).GetAwaiter().GetResult().Pokemons;

            var endApp = false;

            while (!endApp)
            {
                Console.WriteLine("Bem vindo à Pokedex Raiz!");
                Console.WriteLine("Essa Pokedex só disponibiliza os 151 pokemons iniciais.");
                Console.WriteLine("\n1 - Pesquise pelo número");
                Console.WriteLine("2 - Pesquise pelo nome");
                Console.WriteLine("3 - Liste todos");
                Console.WriteLine("4 - Informações (in)úteis");
                Console.WriteLine("9 - SAIR");

                Console.Write("\nDigite uma das opções: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Write("Digite o número: ");
                        var validPokemonNumber = int.TryParse(Console.ReadLine(), out int pokemonNumber);
                        if (!validPokemonNumber || pokemonNumber < 1 || pokemonNumber > 151)
                        {
                            Console.WriteLine("\nNúmero inválido!");
                        }
                        else
                        {
                            Console.WriteLine($"\nO pokémon #{pokemonNumber} é: {pokedex[pokemonNumber - 1].Name.ToUpper()}");
                        }
                        break;
                    case "2":
                        Console.Write("\nDigite o nome: ");
                        var pokemonName = Console.ReadLine();
                        var pokemonIndexFound = pokedex
                            .Select(p => p.Name)
                            .ToList()
                            .FindIndex(p => p.ToUpper() == pokemonName.ToUpper());

                        if (pokemonIndexFound >= 0)
                        {
                            Console.WriteLine($"\nPokémon achado: #{pokemonIndexFound + 1} - {pokedex[pokemonIndexFound].Name.ToUpper()}");
                        }
                        else
                        {
                            Console.WriteLine("\nPokémon não encontrado!");
                        }
                        break;
                    case "3":
                        var count = 1;
                        Console.WriteLine();
                        foreach (var pokemon in pokedex)
                        {
                            Console.WriteLine($"#{count}: {pokemon.Name.ToUpper()}");
                            count++;
                        }
                        break;
                    case "4":
                        var letterACount = pokedex.Count(p => p.Name.ToUpper().First() == 'A');
                        Console.WriteLine($"\n- Existem {letterACount} pokémons que começam com a letra A");

                        var countTotalLetters = pokedex.Sum(p => p.Name.Count());
                        Console.WriteLine($"- Existe um total de {countTotalLetters} caracteres contidos em todos os nomes dos 151 pokemons");

                        Console.WriteLine($"- Existem {pokemonsTotalCount} Pokemons até hoje");
                        break;
                    case "9":
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
                Console.WriteLine("\nClique em qualquer tecla para CONTINUAR...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        static async Task<Pokedex> createPokedex(int startNumber, int count = 1)
        {
            var httpClient = new HttpClient();

            try
            {
                var url = $"https://pokeapi.co/api/v2/pokemon/?offset={startNumber - 1}&limit={count}";
                var response = await httpClient.GetAsync(url);
                var message = await response.Content.ReadAsStringAsync();
                var pokedex = JsonSerializer.Deserialize<Pokedex>(message);

                return pokedex;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                throw;
            }
        }
    }
}