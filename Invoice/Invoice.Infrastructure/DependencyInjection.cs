using Invoice.Infrastructure.DbContext.Interface;
using Invoice.Infrastructure.DbContext;
using Microsoft.Extensions.DependencyInjection;
using Invoice.Infrastructure.Queries;
using Invoice.Application.Interfaces.Queries;
using Invoice.Application.Interfaces.Command;
using Invoice.Infrastructure.Command;

namespace Invoice.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services)
        {
            #region BD
            services.AddSingleton<IAppDbContext, AppDbContext>();
            #endregion

            #region QUERIES
            services.AddTransient<IInvoiceQuery, InvoiceQuery>();
            #endregion

            #region COMMANDS
            services.AddTransient<IInvoiceCommands, InvoiceCommands>();
            #endregion
        }
    }
}