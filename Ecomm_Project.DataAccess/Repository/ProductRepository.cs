using Ecomm_Project.DataAccess.Data;
using Ecomm_Project.DataAccess.Repository.IRepository;
using Ecomm_Project.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecomm_Project.DataAccess.Repository
{
    public class ProductRepository : Repository <Product>,IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
