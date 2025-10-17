using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
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
}
