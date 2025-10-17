using Invoice.Application.Features.Invoice.Commands;

namespace Invoice.Application.UseCases.Interfaces
{
    public interface IInvoiceUseCase
    {
        Task<IList<Domain.Entities.Invoice>> ListInvoice();
        Task<Domain.Entities.Invoice> GetInvoiceById(int id);
        Task<int> UpdateInvoice(UpdateInvoiceCommand command);
        Task<int> DeleteInvoice(int id);
    }
}
