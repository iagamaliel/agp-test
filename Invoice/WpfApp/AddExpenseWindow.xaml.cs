using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp
{
    public partial class AddExpenseWindow : Window
    {
        private readonly InvoiceService _invoiceService;

        public AddExpenseWindow()
        {
            InitializeComponent();
            _invoiceService = new InvoiceService();
            cbCategory.SelectedIndex = 0;
            cbStatus.SelectedIndex = 0;
            dpExpenseDate.SelectedDate = DateTime.Now;
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            // Validaciones básicas
            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Please enter a valid amount.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var category = (cbCategory.SelectedItem as ComboBoxItem)?.Content?.ToString();
            var status = (cbStatus.SelectedItem as ComboBoxItem)?.Content?.ToString();

            if (string.IsNullOrWhiteSpace(category) || string.IsNullOrWhiteSpace(status))
            {
                MessageBox.Show("Category and Status are required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var invoice = new Invoice
            {
                Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text,
                Amount = amount,
                Category = category,
                ExpenseDate = dpExpenseDate.SelectedDate ?? DateTime.Now,
                LastPaymentDate = dpLastPaymentDate.SelectedDate, // puede ser null
                Destination = string.IsNullOrWhiteSpace(txtDestination.Text) ? null : txtDestination.Text,
                Status = status
            };

            try
            {
                (sender as Button).IsEnabled = false;

                var (code, message) = await _invoiceService.CreateInvoiceAsync(invoice);

                if (code == 1)
                {
                    MessageBox.Show(message ?? "Expense created.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(message ?? "Failed to create expense.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                (sender as Button).IsEnabled = true;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
