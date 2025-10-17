using Dapper;
using Invoice.Application.Interfaces.Queries;
using Invoice.Infrastructure.DbContext.Interface;

namespace Invoice.Infrastructure.Queries
{
    public class InvoiceQuery : IInvoiceQuery
    {
        #region ATTRIBUTES
        private readonly IAppDbContext _appContext;
        #endregion

        #region "CONSTRUCTOR"
        public InvoiceQuery(IAppDbContext appDbContext)
        {
            _appContext = appDbContext;
        }
        #endregion

        #region "METODOS"
        public async Task<IList<Domain.Entities.Invoice>> ListInvoice()
        {
            string _query = @"SELECT 
                              [IdInvoice],
                              [ExpenseDate],
                              [Description],
                              [Amount],
                              [Category],
                              [LastPaymentDate],
                              [Destination],
                              [Status]
                        FROM [dbo].[Expense]";


            using var connection = _appContext.GetConnection();
            return (await connection.QueryAsync<Domain.Entities.Invoice>(_query)).ToList();
        }

        public async Task<Domain.Entities.Invoice> GetInvoiceById(int idInvoice)
        {
            string _query = @"SELECT 
                              [IdInvoice],
                              [ExpenseDate],
                              [Description],
                              [Amount],
                              [Category],
                              [LastPaymentDate],
                              [Destination],
                              [Status]
                        FROM [dbo].[Expense]
                        WHERE [IdInvoice] = @IdInvoice;";

            using var connection = _appContext.GetConnection();
            return await connection.QuerySingleOrDefaultAsync<Domain.Entities.Invoice>(_query, new { IdInvoice = idInvoice });
        }
        #endregion
    }
}
