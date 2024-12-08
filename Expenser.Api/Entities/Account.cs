using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expenser.Api.Entities;

public class Account
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; }
    [Column(TypeName = "decimal(15, 2)")]
    public decimal Balance { get; set; }
    public int AccountTypeId { get; set; }

    public IList<Transaction> Transactions { get; set; } = [];
}