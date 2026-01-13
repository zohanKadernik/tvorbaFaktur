using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using tvorbaFaktur.Models;

namespace tvorbaFaktur
{
    public partial class MainWindow : Window
    {
        private List<Client> Clients = new();
        private Dictionary<Client, List<Invoice>> Invoices = new();

        public MainWindow()
        {
            InitializeComponent();

            ClientList.SelectionChanged += ClientList_SelectionChanged;
            InvoiceList.SelectionChanged += InvoiceList_SelectionChanged;

            AddClientButton.Click += AddClientButton_Click;
            RemoveClientButton.Click += RemoveClientButton_Click;

            CreateInvoiceButton.Click += CreateInvoiceButton_Click;
            RemoveInvoiceButton.Click += RemoveInvoiceButton_Click;
            SaveInvoiceButton.Click += SaveInvoiceButton_Click;
        }

        // ============ KLIENTI ============

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddClientWindow();
            dialog.Owner = this;

            if (dialog.ShowDialog() == true)
            {
                var client = new Client
                {
                    Name = dialog.ClientName,
                    ICO = dialog.ClientICO,
                    BirthDate = dialog.ClientBirthDate,
                    Address = dialog.ClientAddress
                };

                Clients.Add(client);
                ClientList.Items.Add(client);

                Invoices[client] = new List<Invoice>();
            }

            UpdateButtonStates();
        }

        private void RemoveClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientList.SelectedItem == null) return;

            var client = (Client)ClientList.SelectedItem;
            Clients.Remove(client);
            Invoices.Remove(client);
            ClientList.Items.Remove(client);

            InvoiceList.Items.Clear();
            UpdateButtonStates();
        }

        private void ClientList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadInvoicesForSelectedClient();
            UpdateButtonStates();
        }

        // ============ FAKTURY ============

        private void CreateInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientList.SelectedItem == null) return;

            var client = (Client)ClientList.SelectedItem;

            var dialog = new AddInvoiceWindow(client);
            dialog.Owner = this;

            if (dialog.ShowDialog() == true)
            {
                var invoice = dialog.CreatedInvoice;

                Invoices[client].Add(invoice);
                InvoiceList.Items.Add(invoice);
            }

            UpdateButtonStates();
        }

        private void RemoveInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientList.SelectedItem == null || InvoiceList.SelectedItem == null) return;

            var client = (Client)ClientList.SelectedItem;
            var invoice = (Invoice)InvoiceList.SelectedItem;

            Invoices[client].Remove(invoice);
            InvoiceList.Items.Remove(invoice);

            UpdateButtonStates();
        }

        private void SaveInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            if (InvoiceList.SelectedItem == null) return;

            var invoice = (Invoice)InvoiceList.SelectedItem;

            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Filter = "Textový soubor (*.txt)|*.txt";
            dialog.FileName = $"{invoice.Number}.txt";

            if (dialog.ShowDialog() == true)
            {
                System.IO.File.WriteAllText(dialog.FileName, invoice.ToText());
                MessageBox.Show("Faktura byla úspěšně uložena!", "Uloženo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }



        private void InvoiceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateButtonStates();
        }

        // ============ HELPER ============

        private void LoadInvoicesForSelectedClient()
        {
            InvoiceList.Items.Clear();

            if (ClientList.SelectedItem == null) return;

            var client = (Client)ClientList.SelectedItem;

            foreach (var invoice in Invoices[client])
                InvoiceList.Items.Add(invoice);
        }

        private void UpdateButtonStates()
        {
            bool clientSelected = ClientList.SelectedItem != null;
            bool invoiceSelected = InvoiceList.SelectedItem != null;

            CreateInvoiceButton.IsEnabled = clientSelected;
            RemoveClientButton.IsEnabled = clientSelected;

            SaveInvoiceButton.IsEnabled = invoiceSelected;
            RemoveInvoiceButton.IsEnabled = invoiceSelected;
        }
    }
}
