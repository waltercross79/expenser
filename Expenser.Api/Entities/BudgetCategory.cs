using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expenser.Api.Entities;

public class BudgetCategory
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(500)]
    public string? Description { get; set; }
    [Column(TypeName = "decimal(15, 2)")]
    public decimal BudgetAmount { get; set; }
    [Column(TypeName = "decimal(15, 2)")]
    public decimal SpentAmount { get; set; }
    public int ProjectId { get; set; }

    public Project Project { get; set; }
}