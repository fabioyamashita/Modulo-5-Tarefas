using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsumingPokemonAPI
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
