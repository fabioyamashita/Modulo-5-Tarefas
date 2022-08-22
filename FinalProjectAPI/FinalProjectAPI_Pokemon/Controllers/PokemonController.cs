using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using FinalProjectAPI_Pokemon.Models;
using FinalProjectAPI_Pokemon.Dtos;
using FinalProjectAPI_Pokemon.Data;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAPI_Pokemon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        //private ICollection<Pokemon> pokemons1 = new List<Pokemon>();
        private readonly PokemonContext _context;

        public PokemonController(PokemonContext context)
        {
            _context = context;
            //pokemons1 = _context.Pokemon.ToList();
        }

        // GET: api/<PokemonsController>
        // Return all pokemons in Database
        [HttpGet]
        public async Task<ActionResult<Pokemon>> Get()
        {
            return Ok(await _context.Pokemon.ToListAsync());
        }

        // GET api/<PokemonsController>/5
        // Return a pokemon by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Pokemon>> Get(int id)
        {
            var pokemons = await _context.Pokemon.ToListAsync();

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

            var pokemons = await _context.Pokemon.ToListAsync();

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

            _context.Pokemon.Add(pokemon);
            await _context.SaveChangesAsync();

            return Created($"{urlPokemonApi}{request.Id}/", pokemon);
        }

        // PUT api/<PokemonsController>/5
        //[HttpPut("{id}")]
        [HttpPut]
        public async Task<ActionResult<Pokemon>> UpdatePokemon([FromBody] CreatePokemon request)
        {
            var pokemons = await _context.Pokemon.ToListAsync();

            var dbPokemon = await _context.Pokemon.FindAsync(request.Id);
            if (dbPokemon == null)
            {
                return BadRequest($"Pokémon #{request.Id} não encontrado!");
            }

            dbPokemon.Id = request.Id;
            dbPokemon.Name = request.Name;

            await _context.SaveChangesAsync();

            return Ok($"Pokémon #{request.Id} - atualizado com sucesso!");
        }

        // DELETE api/<PokemonsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pokemon>> Delete(int id)
        {
            var dbPokemon = await _context.Pokemon.FindAsync(id);

            if (dbPokemon == null)
            {
                return BadRequest($"Id #{id} não existe!");
            }

            _context.Pokemon.Remove(dbPokemon);

            await _context.SaveChangesAsync();

            return Ok($"Pokémon #{id} - deletado sucesso!");
        }
    }
}
