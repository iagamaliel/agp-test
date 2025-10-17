using Invoice.Application.Features.Invoice.Commands;
using Invoice.Application.Features.Invoice.Queries;
using Invoice.Domain.Entities.Base;
using Invoice.Utils.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class InvoiceController : BaseApiController
    {
        public InvoiceController() { }

        [HttpGet]
        [ProducesResponseType(typeof(ListResponse<Domain.Entities.Invoice>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ListResponse<Domain.Entities.Invoice>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ListResponse<Domain.Entities.Invoice>))]

        public async Task<IActionResult> ListInvoice()
        {
            try
            {
                return Ok(await Mediator.Send(new ListInvoiceQuery()));
            }
            catch (DataNotFoundException ex)
            {
                var response = new ListResponse<Domain.Entities.Invoice>
                {
                    Code = 0,
                    Message = ex.Message
                };

                return NotFound(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.StackTrace);
            }
        }

        [HttpGet("GetById")]
        [ProducesResponseType(typeof(ObjectResponse<Domain.Entities.Invoice>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ObjectResponse<Domain.Entities.Invoice>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ObjectResponse<Domain.Entities.Invoice>))]
        public async Task<IActionResult> GetInvoiceByIdQuery([FromQuery] GetInvoiceByIdQuery query)
        {
            try
            {
                return Ok(await Mediator.Send(query));
            }
            catch (DataNotFoundException ex)
            {
                var response = new ObjectResponse<Domain.Entities.Invoice>
                {
                    Code = 0,
                    Message = ex.Message
                };

                return NotFound(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.StackTrace);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseGeneric))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseGeneric))]
        public async Task<IActionResult> UpdateInvoice([FromBody] UpdateInvoiceCommand command)
        {
            try
            {
                return StatusCode(
                StatusCodes.Status200OK, await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.StackTrace);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseGeneric))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseGeneric))]
        public async Task<IActionResult> DeleteInvoice([FromQuery] DeleteInvoiceCommand command)
        {
            try
            {
                return StatusCode(
                StatusCodes.Status200OK, await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.StackTrace);
            }
        }
    }
}
