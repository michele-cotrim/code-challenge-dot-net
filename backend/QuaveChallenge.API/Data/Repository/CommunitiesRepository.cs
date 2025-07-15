using QuaveChallenge.API.Models;

namespace QuaveChallenge.API.Data.Repository
{
    public class CommunitiesRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly IUnitOfWork _unitOfWork;

        public CommunitiesRepository(ApplicationDbContext dbcontext, IUnitOfWork unitOfWork)
        {
            _dbcontext = dbcontext;
            _unitOfWork = unitOfWork;
        }

        public Community GetByID(int id)
        {
            return _dbcontext.Communities.First(x => x.Id == id);
        }

        public Community GetByName(string name)
        {
            return _dbcontext.Communities.First(x => x.Name == name);
        }

        public List<Community> GetList()
        {
            return [.. _dbcontext.Communities];
        }
    }
}
