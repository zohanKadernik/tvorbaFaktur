using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using tvorbaFaktur.Models;

namespace tvorbaFaktur
{
    public partial class AddInvoiceWindow : Window
    {
        public Client CurrentClient { get; }
        private List<InvoiceItem> items = new List<InvoiceItem>();

        public Invoice CreatedInvoice { get; private set; }

        public AddInvoiceWindow(Client client)
        {
            InitializeComponent();
            CurrentClient = client;
            HoursText.Text = "1";
        }

        private void ServiceCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ServiceCombo.SelectedItem is ComboBoxItem item)
            {
                string service = item.Content.ToString();

                if (service == "Finanční poradenství")
                    RateText.Text = "800";
                else if (service == "Energetické poradenství")
                    RateText.Text = "700";
            }
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ServiceCombo.Text))
            {
                MessageBox.Show("Vyber službu.");
                return;
            }

            if (!decimal.TryParse(RateText.Text, out decimal rate))
            {
                MessageBox.Show("Neplatná sazba.");
                return;
            }

            if (!int.TryParse(HoursText.Text, out int hours))
            {
                MessageBox.Show("Neplatný počet hodin.");
                return;
            }

            items.Add(new InvoiceItem
            {
                Service = ServiceCombo.Text,
                Rate = rate,
                Hours = hours
            });

            RefreshItems();
        }

        private void RefreshItems()
        {
            ItemsList.Items.Clear();

            foreach (var i in items)
                ItemsList.Items.Add($"{i.Service} - {i.Hours}h x {i.Rate} = {i.Total} Kč");

            TotalText.Text = $"Celkem: {items.Sum(x => x.Total)} Kč";
        }

        private void SaveInvoice_Click(object sender, RoutedEventArgs e)
        {
            CreatedInvoice = new Invoice
            {
                Number = InvoiceNumberText.Text,
                DueDate = DueDatePicker.SelectedDate ?? DateTime.Now,
                Client = CurrentClient,
                Items = items.ToList()
            };

            DialogResult = true;
            Close();
        }

        private void CancelInvoice_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
