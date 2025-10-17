using Invoice.Application.Features.Invoice.Commands;

namespace Invoice.Application.Interfaces.Command
{
    public interface IInvoiceCommands
    {
        Task<int> DeleteInvoice(int idInvoice);
        Task<int> UpdateInvoice(UpdateInvoiceCommand command);
    }
}
