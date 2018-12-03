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
                         Title = "預金",
                         Price = 150306,
                         TargetMonth = DateTime.Parse("2018-11-1"),
                         CreateDate = DateTime.Now,
                         Status = "done",

                     },

                     new Transaction
                     {
                         Type = "out",
                         Title = "積立",
                         Price = 30000,
                         TargetMonth = DateTime.Parse("2018-11-1"),
                         CreateDate = DateTime.Now,
                         Status = "done",
                     },

                     new Transaction
                     {
                         Type = "out",
                         Title = "返済",
                         Price = 30000,
                         TargetMonth = DateTime.Parse("2018-11-1"),
                         CreateDate = DateTime.Now,
                         Status = "done",
                     },

                     new Transaction
                     {
                         Type = "out",
                         Title = "2018年分自動車保険",
                         Price = 45000,
                         TargetMonth = DateTime.Parse("2018-11-1"),
                         CreateDate = DateTime.Now,
                         Status = "done",
                     },

                     new Transaction
                     {
                         Type = "out",
                         Title = "引落Amex",
                         Price = 100000,
                         TargetMonth = DateTime.Parse("2018-11-1"),
                         CreateDate = DateTime.Now,
                         Status = "done",
                     },

                     new Transaction
                     {
                         Type = "out",
                         Title = "引落VISA",
                         Price = 40000,
                         TargetMonth = DateTime.Parse("2018-11-1"),
                         CreateDate = DateTime.Now,
                         Status = "done",
                     }

               );
                context.SaveChanges();
            }
        }
    }
}