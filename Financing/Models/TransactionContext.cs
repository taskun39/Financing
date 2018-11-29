using Microsoft.EntityFrameworkCore;

namespace Financing.Models
{
    public class TransactionContext : DbContext
    {
        public TransactionContext(DbContextOptions<TransactionContext> options)
            : base(options)
        {
        }

        public DbSet<Financing.Models.Transaction> Transaction { get; set; }
    }
}