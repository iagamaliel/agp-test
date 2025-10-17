using System.Data;

namespace Invoice.Infrastructure.DbContext.Interface
{
    public interface IAppDbContext
    {
        IDbConnection GetConnection();
    }
}
