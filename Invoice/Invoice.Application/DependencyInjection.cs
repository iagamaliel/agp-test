using Invoice.Application.UseCases;
using Invoice.Application.UseCases.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Invoice.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IInvoiceUseCase, InvoiceUseCase>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}