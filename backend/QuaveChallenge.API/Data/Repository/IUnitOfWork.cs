namespace QuaveChallenge.API.Data.Repository
{
    public interface IUnitOfWork
    {
        void Commit();
        public void BeginTransaction();

        Task SaveChanges(CancellationToken cancellationToken);

    }
}
