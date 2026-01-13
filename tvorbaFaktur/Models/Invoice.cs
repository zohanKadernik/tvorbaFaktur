using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tvorbaFaktur.Models
{
    public class Invoice
    {
        public string Number { get; set; }
        public DateTime DueDate { get; set; }
        public Client Client { get; set; }
        public List<InvoiceItem> Items { get; set; } = new();

        public decimal Total => Items.Sum(x => x.Total);

        public override string ToString()
            => $"{Number} ({DueDate:dd.MM.yyyy})";
        public string ToText()
        {
            var lines = new List<string>();

            lines.Add($"FAKTURA č.: {Number}");
            lines.Add($"Datum splatnosti: {DueDate:dd.MM.yyyy}");
            lines.Add("");
            lines.Add("KLIENT:");
            lines.Add($"Jméno: {Client.Name}");
            lines.Add($"IČO: {Client.ICO}");
            lines.Add($"Adresa: {Client.Address}");
            lines.Add("");
            lines.Add("POLOŽKY:");
            foreach (var i in Items)
                lines.Add($" - {i.Service}: {i.Hours}h x {i.Rate} Kč = {i.Total} Kč");
            lines.Add("");
            lines.Add($"CELKEM: {Total} Kč");

            return string.Join(Environment.NewLine, lines);
        }


    }
}
