using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;

namespace WpfApp.Services
{
    public class InvoiceService
    {
        private readonly HttpClient _httpClient;

        public InvoiceService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7088/");
        }

        public async Task<List<Invoice>> GetInvoicesAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<InvoiceResponse>("api/v1/Invoice");

                if (response != null && response.Code == 1 && response.Items != null)
                    return response.Items;

                return new List<Invoice>();
            }
            catch (Exception ex)
            {
                // Puedes loguear o mostrar un mensaje de error
                Console.WriteLine($"Error al obtener facturas: {ex.Message}");
                return new List<Invoice>();
            }
        }

        public async Task<(int Code, string Message)> UpdateInvoiceAsync(Invoice invoice)
        {
            try
            {
                // Enviamos PUT con el objeto invoice como JSON
                var response = await _httpClient.PutAsJsonAsync("api/v1/Invoice", invoice);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ResponseGeneric>();
                    return (result.Code, result.Message);
                }

                return (0, $"HTTP Error: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating invoice: {ex.Message}");
                return (0, ex.Message);
            }
        }

        public async Task<(int Code, string Message)> DeleteInvoiceAsync(int idInvoice)
        {
            try
            {
                // Construimos la URL con query string
                var response = await _httpClient.DeleteAsync($"api/v1/Invoice?IdInvoice={idInvoice}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ResponseGeneric>();
                    return (result.Code, result.Message);
                }

                return (0, $"HTTP Error: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting invoice: {ex.Message}");
                return (0, ex.Message);
            }
        }

        public async Task<(int Code, string Message)> CreateInvoiceAsync(Invoice invoice)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/v1/Invoice", invoice);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ResponseGeneric>();
                    return (result.Code, result.Message);
                }

                return (0, $"HTTP Error: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating invoice: {ex.Message}");
                return (0, ex.Message);
            }
        }

    }
}
