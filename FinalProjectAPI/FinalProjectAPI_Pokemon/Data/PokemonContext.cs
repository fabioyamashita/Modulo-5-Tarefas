using FinalProjectAPI_Pokemon.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAPI_Pokemon.Data
{
    public class PokemonContext : DbContext
    {
        public PokemonContext(DbContextOptions<PokemonContext> options)
        : base(options) { }

        public DbSet<Pokemon> Pokemon { get; set; }
    }
}
