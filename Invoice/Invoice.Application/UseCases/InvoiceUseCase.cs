using Invoice.Application.Features.Invoice.Commands;
using Invoice.Application.Interfaces.Command;
using Invoice.Application.Interfaces.Queries;
using Invoice.Application.UseCases.Interfaces;

namespace Invoice.Application.UseCases
{
    public class InvoiceUseCase : IInvoiceUseCase
    {
        #region ATRIBUTOS
        private readonly IInvoiceQuery _invoiceQuery;
        private readonly IInvoiceCommands _invoiceCommands;
        #endregion

        #region CONTRUCTOR
        public InvoiceUseCase(IInvoiceQuery invoiceQuery, IInvoiceCommands invoiceCommands)
        {
            _invoiceQuery = invoiceQuery ?? throw new ArgumentNullException(nameof(invoiceQuery));
            _invoiceCommands = invoiceCommands ?? throw new ArgumentNullException(nameof(invoiceCommands));
        }
        #endregion

        #region METODOS
        public async Task<IList<Domain.Entities.Invoice>> ListInvoice()
        {
            return await _invoiceQuery.ListInvoice();
        }

        public async Task<Domain.Entities.Invoice> GetInvoiceById(int id)
        {
            return await _invoiceQuery.GetInvoiceById(id);
        }

        public async Task<int> UpdateInvoice(UpdateInvoiceCommand command)
        {
            return await _invoiceCommands.UpdateInvoice(command);
        }

        public async Task<int> DeleteInvoice(int id)
        {
            return await _invoiceCommands.DeleteInvoice(id);
        }
        #endregion
    }
}
