using QuaveChallenge.API.Models;

namespace QuaveChallenge.API.Data.Repository
{
    public class PeopleRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly IUnitOfWork _unitOfWork;

        public PeopleRepository(ApplicationDbContext dbcontext, IUnitOfWork unitOfWork)
        {
            _dbcontext = dbcontext;
            _unitOfWork = unitOfWork;
        }

        public Person GetByID(int id) 
        { 
            return _dbcontext.People.First(x => x.Id == id);
        }

        public Person GetByCommunityId(int id)
        {
            return _dbcontext.People.First(x => x.CommunityId == id);

        }

        public List<Person> GetList()
        {
            return [.. _dbcontext.People];
        }
    }
}
