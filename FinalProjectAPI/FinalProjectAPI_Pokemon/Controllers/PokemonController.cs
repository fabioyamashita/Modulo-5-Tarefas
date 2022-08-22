using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using FinalProjectAPI_Pokemon.Models;
using FinalProjectAPI_Pokemon.Dtos;

namespace FinalProjectAPI_Pokemon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly ILogger<PokemonController> _logger;

        public PokemonController(ILogger<PokemonController> logger)
        {
            _logger = logger;
        }

        // GET: api/<PokemonsController>
        // Return all pokemons in Database
        [HttpGet]
        public async Task<ActionResult<Pokemon>> Get()
        {
            using var reader = new StreamReader("./dataComplete.json");
            var json = await reader.ReadToEndAsync();
            var pokemons = JsonSerializer.Deserialize<List<Pokemon>>(json);

            return Ok(pokemons);
        }

        // GET api/<PokemonsController>/5
        // Return a pokemon by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Pokemon>> Get(int id)
        {
            using var reader = new StreamReader("./dataComplete.json");
            var json = await reader.ReadToEndAsync();
            var pokemons = JsonSerializer.Deserialize<List<Pokemon>>(json);

            var pokemon = pokemons.Where(p => p.Id == id).FirstOrDefault();

            if (pokemon == null)
            {
                return NotFound(new
                {
                    Message = $"Pokémon nº {id} não encontrado!"
                });
            }

            return Ok(pokemon);
        }

        // POST api/<PokemonsController>
        [HttpPost]
        public async Task<ActionResult<Pokemon>> CreatePokemon([FromBody] CreatePokemon request)
        {
            string urlPokemonApi = $"https://pokeapi.co/api/v2/pokemon/";

            using var reader = new StreamReader("./dataComplete.json");
            var json = await reader.ReadToEndAsync();
            reader.Dispose();
            var pokemons = JsonSerializer.Deserialize<List<Pokemon>>(json);

            if (pokemons.Select(p => p.Id).Contains(request.Id))
            {
                return BadRequest($"Id #{request.Id} já existe!");
            }

            var pokemon = new Pokemon
            {
                Id = request.Id,
                Name = request.Name,
                Url = $"{urlPokemonApi}{request.Id}/"
            };

            pokemons.Add(pokemon);

            var content = JsonSerializer.Serialize(pokemons);
            System.IO.File.WriteAllText("./dataComplete.json", content);

            return Created($"{urlPokemonApi}{request.Id}/", pokemon);
        }

        // PUT api/<PokemonsController>/5
        //[HttpPut("{id}")]
        [HttpPut]
        public async Task<ActionResult<Pokemon>> UpdatePokemon([FromBody] CreatePokemon request)
        {
            using var reader = new StreamReader("./dataComplete.json");
            var json = await reader.ReadToEndAsync();
            reader.Dispose();
            var pokemons = JsonSerializer.Deserialize<List<Pokemon>>(json);

            var pokemon = pokemons.Find(p => p.Id == request.Id);
            if (pokemon == null)
            {
                return BadRequest($"Pokémon #{request.Id} não encontrado!");
            }

            pokemon.Id = request.Id;
            pokemon.Name = request.Name;

            var content = JsonSerializer.Serialize(pokemons);
            System.IO.File.WriteAllText("./dataComplete.json", content);

            return Ok($"Pokémon #{request.Id} - atualizado com sucesso!");
        }

        // DELETE api/<PokemonsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pokemon>> Delete(int id)
        {
            using var reader = new StreamReader("./dataComplete.json");
            var json = await reader.ReadToEndAsync();
            reader.Dispose();
            var pokemons = JsonSerializer.Deserialize<List<Pokemon>>(json);
            
            if (!pokemons.Select(p => p.Id).Contains(id))
            {
                return BadRequest($"Id #{id} não existe!");
            }

            pokemons.Remove(pokemons.SingleOrDefault(p => p.Id == id));

            var content = JsonSerializer.Serialize(pokemons);
            System.IO.File.WriteAllText("./dataComplete.json", content);

            return Ok($"Pokémon #{id} - deletado sucesso!");
        }
    }
}
