
namespace Invoice.Application.Interfaces.Queries
{
    public interface IInvoiceQuery
    {
        Task<IList<Domain.Entities.Invoice>> ListInvoice();
        Task<Domain.Entities.Invoice> GetInvoiceById(int idInvoice);
    }
}
