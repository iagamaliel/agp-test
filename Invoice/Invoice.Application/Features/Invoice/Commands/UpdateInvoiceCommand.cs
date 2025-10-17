using Invoice.Application.UseCases.Interfaces;
using Invoice.Domain.Entities.Base;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Invoice.Application.Features.Invoice.Commands
{
    public class UpdateInvoiceCommand : IRequest<ResponseGeneric>
    {
        [Required(ErrorMessage = "IdInvoice Required")]
        public int IdInvoice { get; set; }

        [StringLength(4000, ErrorMessage = "Description cannot exceed 4000 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [RegularExpression("Education|Health|Fun|Monthly Expenses|Other",
            ErrorMessage = "Category must be one of: Education, Health, Fun, Monthly Expenses, Other")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Expense Date is required")]
        public DateTime ExpenseDate { get; set; }

        public DateTime? LastPaymentDate { get; set; }

        [StringLength(100, ErrorMessage = "Destination cannot exceed 100 characters")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [RegularExpression("Pending|Paid", ErrorMessage = "Status must be either Pending or Paid")]
        public string Status { get; set; }

        public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, ResponseGeneric>
        {
            #region ATRIBUTOS
            private readonly IInvoiceUseCase _invoiceUseCase;
            #endregion

            #region CONTRUCTOR
            public UpdateInvoiceCommandHandler(IInvoiceUseCase invoiceUseCase)
            {
                _invoiceUseCase = invoiceUseCase ?? throw new ArgumentNullException(nameof(invoiceUseCase));
            }
            #endregion

            #region METODOS
            public async Task<ResponseGeneric> Handle(UpdateInvoiceCommand command, CancellationToken cancellationToken)
            {
                var response = new ResponseGeneric();
                var insert = await _invoiceUseCase.UpdateInvoice(command);

                if (insert > 0)
                {
                    response.Code = 1;
                    response.Message = "Invoice successfully updated";
                }
                else
                {
                    response.Code = 0;
                    response.Message = "Invoice failed updated";
                }

                return response;
            }
            #endregion
        }
    }
}
