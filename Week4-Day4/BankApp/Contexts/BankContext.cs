using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Models;
using BankApp.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Contexts
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
        }

        public DbSet<Models.Account> Accounts { get; set; }
        public DbSet<Models.Transaction> Transactions { get; set; }

        public DbSet<SearchAccountDto> SearchAccountDtos { get; set; }

        public async Task<ICollection<SearchAccountDto>> GetAllAccounts()
        {
            return await this.Set<SearchAccountDto>()
                        .FromSqlInterpolated($@"SELECT ""AccountId"", ""AccountHolderName"",""Email"", ""Balance"", ""CreatedAt""
                                                FROM public.""Accounts"" 
                                                ORDER BY ""AccountId"" ASC")
                        .ToListAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Account>().HasKey(acc => acc.AccountId).HasName("PK_AccountId");

            modelBuilder.Entity<Transaction>().HasKey(t => t.TransactionId).HasName("PK_TransactionId");

            modelBuilder.Entity<Transaction>().HasOne(t => t.FromAccount)
                                              .WithMany(a => a.SentTransactions)
                                              .HasForeignKey(t => t.FromAccountId)
                                              .HasConstraintName("FK_Transaction_FromAccount")
                                              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>().HasOne(t => t.ToAccount)
                                              .WithMany(a => a.RecievedTransactions)
                                              .HasForeignKey(t => t.ToAccountId)
                                              .HasConstraintName("FK_Transaction_ToAccount")
                                              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SearchAccountDto>().HasNoKey().ToView(null);
        }
    }
}