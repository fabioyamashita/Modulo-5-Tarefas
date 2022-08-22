using System.Text.Json.Serialization;

namespace ConsumingPokemonAPI_V2
{
    public class Pokedex
    {
        [JsonPropertyName("count")]
        public long Count { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("previous")]
        public string Previous { get; set; }

        [JsonPropertyName("results")]
        public Pokemon[] Pokemons { get; set; }
    }
}
