using Ecomm_Project.DataAccess.Data;
using Ecomm_Project.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecomm_Project.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUserRepository>,IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserRepository (ApplicationDbContext context) : base (context)
        {
            context = _context;
        }

    }
}
