using Invoice.Application.UseCases.Interfaces;
using Invoice.Domain.Entities.Base;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Invoice.Application.Features.Invoice.Commands
{
    public  class DeleteInvoiceCommand : IRequest<ResponseGeneric>
    {
        [Required(ErrorMessage = "IdInvoice Required")]
        public int IdInvoice { get; set; }

        public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, ResponseGeneric>
        {
            #region ATRIBUTOS
            private readonly IInvoiceUseCase _invoiceUseCase;
            #endregion

            #region CONTRUCTOR
            public DeleteInvoiceCommandHandler(IInvoiceUseCase invoiceUseCase)
            {
                _invoiceUseCase = invoiceUseCase ?? throw new ArgumentNullException(nameof(invoiceUseCase));
            }
            #endregion

            #region METODOS
            public async Task<ResponseGeneric> Handle(DeleteInvoiceCommand command, CancellationToken cancellationToken)
            {
                var response = new ResponseGeneric();
                var insert = await _invoiceUseCase.DeleteInvoice(command.IdInvoice);

                if (insert > 0)
                {
                    response.Code = 1;
                    response.Message = "Invoice successfully deleted";
                }
                else
                {
                    response.Code = 0;
                    response.Message = "Invoice failed delete";
                }

                return response;
            }
            #endregion
        }
    }
}