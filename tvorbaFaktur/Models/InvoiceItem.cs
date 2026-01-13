using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tvorbaFaktur.Models
{
    public class InvoiceItem
    {
        public string Service { get; set; }
        public decimal Rate { get; set; }
        public int Hours { get; set; }
        public decimal Total => Rate * Hours;
    }
}
