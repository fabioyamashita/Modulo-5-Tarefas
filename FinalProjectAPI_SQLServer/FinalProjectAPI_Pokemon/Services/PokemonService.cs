using FinalProjectAPI_Pokemon.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace FinalProjectAPI_Pokemon.Services
{
    public static class PokemonService
    {
        public static async Task<List<Pokemon>> GetPokemonFromOfficialAPI(int startNumber, int count = 1)
        {
            var httpClient = new HttpClient();

            try
            {
                var url = $"https://pokeapi.co/api/v2/pokemon/?offset={startNumber - 1}&limit={count}";
                var response = await httpClient.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();

                var pokedex = JsonSerializer.Deserialize<JsonObject>(content)["results"].ToJsonString();
                
                var pokemons = JsonSerializer.Deserialize<List<Pokemon>>(pokedex);

                return pokemons;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                throw;
            }
        }
    }
}
