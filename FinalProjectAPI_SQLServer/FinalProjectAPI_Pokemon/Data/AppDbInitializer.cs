using FinalProjectAPI_Pokemon.Models;
using FinalProjectAPI_Pokemon.Services;
using System.Text.Json;

namespace FinalProjectAPI_Pokemon.Data
{
    public static class AppDbInitializer
    {
        public async static Task Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScoped = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScoped.ServiceProvider.GetRequiredService<PokemonContext>();

                if (!context.Pokemon.Any())
                {
                    var pokemons = await PokemonService.GetPokemonFromOfficialAPI(1, 151);
                    var count = 1;

                    foreach (var pokemon in pokemons)
                    {
                        var pokemonToAdd = new Pokemon
                        {
                            Id = count,
                            Name = pokemon.Name,
                            Url = pokemon.Url
                        };

                        context.Pokemon.Add(pokemonToAdd);
                        count++;
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
