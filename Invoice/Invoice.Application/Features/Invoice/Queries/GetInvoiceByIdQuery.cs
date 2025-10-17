using Invoice.Application.UseCases.Interfaces;
using Invoice.Domain.Entities.Base;
using Invoice.Utils.Exceptions;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Invoice.Application.Features.Invoice.Queries
{
    public class GetInvoiceByIdQuery : IRequest<ObjectResponse<Domain.Entities.Invoice>>
    {
        [Required(ErrorMessage = "IdInvoice is required")]
    public int IdInvoice { get; set; }

    public class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, ObjectResponse<Domain.Entities.Invoice>>
    {
        #region ATRIBUTOS
        private readonly IInvoiceUseCase _invoiceUseCase;
        #endregion

        #region CONTRUCTOR
        public GetInvoiceByIdQueryHandler(IInvoiceUseCase invoiceUseCase)
        {
             _invoiceUseCase = invoiceUseCase ?? throw new ArgumentNullException(nameof(invoiceUseCase));
        }
        #endregion

        #region METODOS
        public async Task<ObjectResponse<Domain.Entities.Invoice>> Handle(GetInvoiceByIdQuery query, CancellationToken cancellationToken)
        {
            var response = new ObjectResponse<Domain.Entities.Invoice>();

            var item = await _invoiceUseCase.GetInvoiceById(query.IdInvoice);

            if (item!= null && item.IdInvoice > 0)
            {
                response.Items = item;
                response.Code = 1;
                response.Message = $"found";
            }
            else
                throw new DataNotFoundException("Data Not Found.");

            return response;
        }
        #endregion
    }
}
}