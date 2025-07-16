
using Microsoft.EntityFrameworkCore;

namespace uniBomberQuote.Shared.Models
{

    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> opts) : base(opts)
        {

        }
        public List<MyText> MySents()
        {
            return [.. DataSentences];
        }
        public DbSet<MyText> DataSentences => Set<MyText>();

    }
}