using Invoice.Application.UseCases.Interfaces;
using Invoice.Domain.Entities.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Application.Features.Invoice.Commands
{
    public class CreateInvoiceCommand : IRequest<ResponseGeneric>
    {
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

        public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, ResponseGeneric>
        {
            private readonly IInvoiceUseCase _invoiceUseCase;

            public CreateInvoiceCommandHandler(IInvoiceUseCase invoiceUseCase)
            {
                _invoiceUseCase = invoiceUseCase ?? throw new ArgumentNullException(nameof(invoiceUseCase));
            }

            public async Task<ResponseGeneric> Handle(CreateInvoiceCommand command, CancellationToken cancellationToken)
            {
                var response = new ResponseGeneric();

                var newId = await _invoiceUseCase.CreateInvoice(command);

                if (newId > 0)
                {
                    response.Code = 1;
                    response.Message = $"Invoice successfully created with ID {newId}";
                }
                else
                {
                    response.Code = 0;
                    response.Message = "Invoice creation failed";
                }

                return response;
            }
        }
    }

}
