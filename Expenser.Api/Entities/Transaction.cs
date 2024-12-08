using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Expenser.Api.Entities;

public class Transaction
{
    [Key]
    public int Id { get; set; }
    public DateTime Date { get; set; }
    [Column(TypeName = "decimal(15, 2)")]
    public decimal Amount { get; set; }
    [MaxLength(100)]
    public string? Payer { get; set; }
    [MaxLength(100)]
    public string? Payee { get; set; }
    [MaxLength(500)]
    public string? Description { get; set; }
    public int TransactionTypeId { get; set; }
    public int AccountId { get; set; }

    [JsonIgnore]
    public TransactionType TransactionType { get; set; }
    [JsonIgnore]
    public Account Account { get; set; }

    [JsonIgnore]
    public List<Project> Projects { get; } = [];
    [JsonIgnore]
    public List<TransactionInProject> TransactionInProjects { get; } = [];
}