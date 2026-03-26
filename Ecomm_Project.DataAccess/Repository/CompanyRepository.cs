using Ecomm_Project.DataAccess.Data;
using Ecomm_Project.DataAccess.Repository.IRepository;
using Ecomm_Project.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecomm_Project.DataAccess.Repository
{
    public class CompanyRepository : Repository <Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _context;
        public CompanyRepository (ApplicationDbContext context)
            : base (context)
        {
            _context = context;

        }
    }
}
