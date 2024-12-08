using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expenser.Api.Entities;

public class Project
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; }
    [Column(TypeName = "decimal(15, 2)")]
    public decimal TotalBudget { get; set; }
    [Column(TypeName = "decimal(15, 2)")]
    public decimal TotalSpending { get; set; }

    public IList<Transaction> Transactions { get; } = [];
    public List<TransactionInProject> TransactionInProjects { get; } = [];
    public IList<BudgetCategory> BudgetCategories { get; set; } = [];
}