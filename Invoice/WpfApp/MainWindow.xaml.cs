using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        private readonly InvoiceService _invoiceService;

        public MainWindow()
        {
            InitializeComponent();
            _invoiceService = new InvoiceService();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadExpensesAsync();
        }

        private async void LoadExpenses_Click(object sender, RoutedEventArgs e)
        {
            await LoadExpensesAsync();
        }

        private async Task LoadExpensesAsync()
        {
            try
            {
                List<Invoice> expenses = await _invoiceService.GetInvoicesAsync();
                dgInvoices.ItemsSource = expenses;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading invoices: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                // Obtenemos la fila correspondiente
                var row = DataGridRow.GetRowContainingElement(btn);
                if (row != null)
                {
                    row.IsEnabled = true; // Habilita edición de la fila si estaba bloqueada

                    // Mostrar el botón Update y ocultar Edit
                    var sp = FindVisualChild<StackPanel>(row);
                    if (sp != null)
                    {
                        foreach (var child in sp.Children)
                        {
                            if (child is Button b)
                            {
                                if (b.Name == "btnEdit") b.Visibility = Visibility.Collapsed;
                                if (b.Name == "btnUpdate") b.Visibility = Visibility.Visible;
                            }
                        }
                    }
                }
            }
        }

        // Botón Update
        private async void UpdateInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                var row = DataGridRow.GetRowContainingElement(btn);
                if (row != null)
                {
                    var invoice = row.Item as Invoice;
                    if (invoice != null)
                    {
                        var result = await _invoiceService.UpdateInvoiceAsync(invoice);
                        MessageBox.Show(result.Message, "Update", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Terminar edición antes de refrescar
                        dgInvoices.CommitEdit(DataGridEditingUnit.Row, true);

                        // Recargar datos desde la API
                        dgInvoices.ItemsSource = await _invoiceService.GetInvoicesAsync();

                        // Restaurar visibilidad de botones
                        var sp = FindVisualChild<StackPanel>(row);
                        if (sp != null)
                        {
                            foreach (var child in sp.Children)
                            {
                                if (child is Button b)
                                {
                                    if (b.Name == "btnEdit") b.Visibility = Visibility.Visible;
                                    if (b.Name == "btnUpdate") b.Visibility = Visibility.Collapsed;
                                }
                            }
                        }
                    }
                }
            }
        }


        // Helper para encontrar StackPanel dentro del DataGridRow
        private static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                if (child is T t) return t;
                var childOfChild = FindVisualChild<T>(child);
                if (childOfChild != null) return childOfChild;
            }
            return null;
        }


        private async void DeleteInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Invoice selectedInvoice)
            {
                var confirm = MessageBox.Show($"Are you sure you want to delete invoice #{selectedInvoice.IdInvoice}?",
                                              "Confirm Delete",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Question);

                if (confirm == MessageBoxResult.Yes)
                {
                    try
                    {
                        await _invoiceService.DeleteInvoiceAsync(selectedInvoice.IdInvoice);
                        MessageBox.Show("Invoice deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        await LoadExpensesAsync(); // refrescar
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting invoice: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private async void AddExpense_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddExpenseWindow
            {
                Owner = this
            };

            bool? result = addWindow.ShowDialog();

            if (result == true)
            {
                await LoadExpensesAsync();
            }
        }

    }
}
