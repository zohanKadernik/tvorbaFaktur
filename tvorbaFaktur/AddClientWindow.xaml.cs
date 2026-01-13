using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace tvorbaFaktur
{
    public partial class AddClientWindow : Window
    {
        public string ClientName { get; private set; }
        public string ClientICO { get; private set; }
        public DateTime ClientBirthDate { get; private set; }
        public string ClientAddress { get; private set; }

        public AddClientWindow()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameText.Text))
            {
                MessageBox.Show("Vyplň jméno!");
                return;
            }

            ClientName = NameText.Text;
            ClientICO = ICOText.Text;
            ClientBirthDate = BirthDatePicker.SelectedDate ?? DateTime.Now;
            ClientAddress = AddressText.Text;

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
