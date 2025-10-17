using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
    public class InvoiceResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public List<Invoice> Items { get; set; }
    }
}
