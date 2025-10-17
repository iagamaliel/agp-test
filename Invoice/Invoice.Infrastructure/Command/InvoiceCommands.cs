using Dapper;
using Invoice.Application.Features.Invoice.Commands;
using Invoice.Application.Interfaces.Command;
using Invoice.Infrastructure.DbContext.Interface;
using System.Data;

namespace Invoice.Infrastructure.Command
{
    public class InvoiceCommands : IInvoiceCommands
    {
        #region ATTRIBUTES
        private readonly IAppDbContext _appContext;
        #endregion

        #region "CONSTRUCTOR"
        public InvoiceCommands(IAppDbContext appDbContext)
        {
            _appContext = appDbContext;
        }
        #endregion

        #region "METODOS"
        public async Task<int> DeleteInvoice(int idInvoice)
        {
            using var connection = _appContext.GetConnection();
            var procedureName = "DeleteExpense";

            var parameters = new DynamicParameters();
            parameters.Add("IdInvoice", idInvoice, DbType.Int32, ParameterDirection.Input);

            parameters.Add("Code", 0, DbType.Int32, ParameterDirection.Output);
            parameters.Add("Message", "", DbType.String, ParameterDirection.Output);

            await connection.ExecuteAsync
                    (procedureName, parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<int>("Code");
        }

        public async Task<int> UpdateInvoice(UpdateInvoiceCommand command)
        {
            using var connection = _appContext.GetConnection();
            var procedureName = "UpdateExpense";

            var parameters = new DynamicParameters();

            // Campos obligatorios
            parameters.Add("IdInvoice", command.IdInvoice, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Amount", command.Amount, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("Category", command.Category, DbType.String, ParameterDirection.Input);
            parameters.Add("ExpenseDate", command.ExpenseDate, DbType.Date, ParameterDirection.Input);
            parameters.Add("Status", command.Status, DbType.String, ParameterDirection.Input);

            // Campos opcionales
            parameters.Add("Description", command.Description, DbType.String, ParameterDirection.Input);
            parameters.Add("LastPaymentDate", command.LastPaymentDate, DbType.Date, ParameterDirection.Input);
            parameters.Add("Destination", command.Destination, DbType.String, ParameterDirection.Input);

            // Salida
            parameters.Add("Code", 0, DbType.Int32, ParameterDirection.Output);
            parameters.Add("Message", "", DbType.String, ParameterDirection.Output, size: 255);

            await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<int>("Code");
        }

        #endregion
    }
}
