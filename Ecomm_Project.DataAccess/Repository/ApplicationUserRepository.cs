using Ecomm_Project.DataAccess.Data;
using Ecomm_Project.DataAccess.Repository.IRepository;
using Ecomm_Project.Models;  // ← also make sure this using is present

namespace Ecomm_Project.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;  
        }
    }
}