using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using FinalProjectAPI_Pokemon.Models;
using FinalProjectAPI_Pokemon.Dtos;
using FinalProjectAPI_Pokemon.Data;
using Microsoft.EntityFrameworkCore;
using FinalProjectAPI_Pokemon.Services;

namespace FinalProjectAPI_Pokemon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly PokemonContext _context;

        public PokemonController(PokemonContext context)
        {
            _context = context;
        }

        #region "Return all Pokémons"
        [HttpGet]
        [Route("listAll")]
        public async Task<ActionResult<Pokemon>> GetAll()
        {
            return Ok(await _context.Pokemon.ToListAsync());
        }
        #endregion

        #region "Return all Pokémons with pagination"
        // Return all pokemons in Database
        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<IEnumerable<Pokemon>>> GetAllPagination([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var pokemons = await _context.Pokemon.ToListAsync();

            if (page < 1 || pageSize < 1)
            {
                return BadRequest("Dados inválidos.");
            }

            var pokemonsPerPage = pokemons
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(new
            {
                StatusCode = 200,
                Message = $"Mostrando página {page}",
                Meta = new
                {
                    CurrentPage = page,
                    PageSize = pageSize
                },
                Data = pokemonsPerPage
            });
        }
        #endregion

        #region "Pokémon by Id"
        [HttpGet("{id}")]
        public async Task<ActionResult<Pokemon>> GetById(int id)
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
        #endregion

        #region "Search Engine"
        // Return a pokemon by word or letter
        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<IEnumerable<Pokemon>>> SearchPokemon([FromQuery] string term)
        {
            var pokemons = await _context.Pokemon.ToListAsync();

            if (string.IsNullOrEmpty(term))
            {
                return BadRequest("Especifique uma palavra ou letra válida!");
            }

            var filteredData = pokemons.Where(x => x.Name.StartsWith(term)).ToList();

            if (filteredData.Count == 0)
            {
                return NotFound(new
                {
                    Message = $"Nenhum Pokémon encontrado!"
                });
            }

            return Ok(filteredData);
        }
        #endregion

        #region "Order Pokémons by Name"
        [HttpGet]
        [Route("orderBy")]
        public async Task<ActionResult<IEnumerable<Pokemon>>> OrderByName()
        {
            var pokemons = await _context.Pokemon.ToListAsync();

            var pokemonsOrderByAscending = pokemons.OrderBy(x => x.Name);

            return Ok(pokemonsOrderByAscending);
        }
        #endregion

        #region "Insert Pokémon"
        [HttpPost]
        public async Task<ActionResult<Pokemon>> CreatePokemon([FromBody] CreatePokemon request)
        {
            string urlPokemonApi = $"https://pokeapi.co/api/v2/pokemon/";

            var pokemons = await _context.Pokemon.ToListAsync();

            if (pokemons.Select(p => p.Id).Contains(request.Id))
            {
                return BadRequest($"Id #{request.Id} já existe!");
            }

            var pokemonFromAPI = await PokemonService.GetPokemonFromOfficialAPI(request.Id);

            var pokemon = new Pokemon
            {
                Id = request.Id,
                Name = pokemonFromAPI[0].Name,
                Url = pokemonFromAPI[0].Url
            };

            _context.Pokemon.Add(pokemon);
            await _context.SaveChangesAsync();

            return Created($"{pokemonFromAPI[0].Url}", pokemon);
        }
        #endregion

        #region "Update Pokémon"
        [HttpPut]
        public async Task<ActionResult<Pokemon>> UpdatePokemon([FromBody] UpdatePokemon request)
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
        #endregion

        #region "Delete Pokémon"
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
        #endregion
    }
}
