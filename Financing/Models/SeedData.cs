using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Financing.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TransactionContext(
                serviceProvider.GetRequiredService<DbContextOptions<TransactionContext>>()))
            {
                // Look for any movies.
                if (context.Transaction.Any())
                {
                    return;   // DB has been seeded
                }

                context.Transaction.AddRange(
                     new Transaction
                     {
                         Type = "in",
                         Title = "Salary",
                         Price = 250000,
                         TargetMonth = DateTime.Parse("1989-1-11"),
                         CreateDate = DateTime.Now,
                         Status = "done",

                     },

                     new Transaction
                     {
                         Type = "out",
                         Title = "Credit",
                         Price = 250000,
                         TargetMonth = DateTime.Parse("1989-1-11"),
                         CreateDate = DateTime.Now,
                         Status = "done",
                     }
               );
                context.SaveChanges();
            }
        }
    }
}