using FinalProjectAPI_Pokemon.Models;
using System.Text.Json;

namespace FinalProjectAPI_Pokemon.Data
{
    public static class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScoped = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScoped.ServiceProvider.GetRequiredService<PokemonContext>();

                if (!context.Pokemon.Any())
                {
                    using var reader = new StreamReader("./dataComplete.json");
                    var json = reader.ReadToEnd();
                    var pokemons = JsonSerializer.Deserialize<List<Pokemon>>(json);

                    foreach (var pokemon in pokemons)
                    {
                        var pokemonToAdd = new Pokemon
                        {
                            Id = pokemon.Id,
                            Name = pokemon.Name,
                            Url = pokemon.Url
                        };

                        context.Pokemon.Add(pokemonToAdd);
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
