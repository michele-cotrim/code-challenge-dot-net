
using Microsoft.EntityFrameworkCore.Storage;

namespace QuaveChallenge.API.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void BeginTransaction()
        {
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction?.Commit();
        }

        public Task SaveChanges(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
