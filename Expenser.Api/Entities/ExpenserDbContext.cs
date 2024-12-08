using Microsoft.EntityFrameworkCore;

namespace Expenser.Api.Entities;

public class ExpenserDbContext(DbContextOptions<ExpenserDbContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<BudgetCategory> BudgetCategories { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionType> TransactionTypes { get; set; }
    public DbSet<TransactionInProject> TransactionInProjects { get; set; }
}