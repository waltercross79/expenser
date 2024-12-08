using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expenser.Api.Entities;

[Table("TransactionInProject")]
public class TransactionInProject
{
    [Key]
    public int Id { get; set; }
    
    public int ProjectId { get; set; }
    public int TransactionId { get; set; }
    public int BudgetCategoryId { get; set; }

    public BudgetCategory BudgetCategory { get; set; }

    public Project Project { get; set; }

    public Transaction Transaction { get; set; }

}