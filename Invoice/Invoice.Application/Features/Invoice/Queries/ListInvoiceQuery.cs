using Invoice.Application.UseCases.Interfaces;
using Invoice.Domain.Entities.Base;
using Invoice.Utils.Exceptions;
using MediatR;

namespace Invoice.Application.Features.Invoice.Queries
{
    public class ListInvoiceQuery : IRequest<ListResponse<Domain.Entities.Invoice>>
    {
        public class ListInvoiceQueryHandler : IRequestHandler<ListInvoiceQuery, ListResponse<Domain.Entities.Invoice>>
        {
            #region ATRIBUTOS
            private readonly IInvoiceUseCase _invoiceUseCase;
            #endregion

            #region CONTRUCTOR
            public ListInvoiceQueryHandler(IInvoiceUseCase invoiceUseCase)
            {
                _invoiceUseCase = invoiceUseCase ?? throw new ArgumentNullException(nameof(invoiceUseCase));
            }
            #endregion

            #region METODOS
            public async Task<ListResponse<Domain.Entities.Invoice>> Handle(ListInvoiceQuery query, CancellationToken cancellationToken)
            {
                var response = new ListResponse<Domain.Entities.Invoice>();

                var list = await _invoiceUseCase.ListInvoice();

                if (list.Count > 0)
                {
                    response.Items = list;
                    response.Code = 1;
                    response.Message = $"There were {list.Count} matches found.";
                }
                else
                    throw new DataNotFoundException("Data Not Found.");

                return response;
            }
            #endregion
        }
    }
}
