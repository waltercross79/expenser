using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expenser.Api.Entities;

public class TransactionType
{
    // Debit, Credit, Withdrawal, Deposit
    
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(500)]
    public string? Description { get; set; }

    public static class Values
    {
        public const int Debit = 1;
        public const int Credit = 2;
    }
}