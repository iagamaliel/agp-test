using System.ComponentModel.DataAnnotations;

namespace Invoice.Domain.Entities
{
    public class Invoice
    {
        public int IdInvoice { get; set; }

        public DateTime ExpenseDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

        public string Category { get; set; }

        public DateTime? LastPaymentDate { get; set; }
        public string? Destination { get; set; }
        public string Status { get; set; }
    }

    public enum ExpenseCategory
    {
        Education,
        Health,
        Entertainment,
        MonthlyExpenses,
        Other
    }

    public enum ExpenseStatus
    {
        Pending,
        Paid
    }
}