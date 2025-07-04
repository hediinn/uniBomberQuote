
using Microsoft.EntityFrameworkCore;

namespace uniBomberQuote.Models
{

    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> opts) : base(opts)
        {

        }
        public DbSet<Sentences> Sentences => Set<Sentences>();


    }
}