using Ecomm_Project.DataAccess.Data;
using Ecomm_Project.DataAccess.Repository.IRepository;
using Ecomm_Project.Models;
using Ecomm_Project.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly ApplicationDbContext _context;
        public UserController(IUnitOfWork unitofwork, ApplicationDbContext context)
        {
            _unitofwork = unitofwork;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region Api's
        [HttpGet]
        public IActionResult GetAll()
        {
            var userList = _unitofwork.ApplicationUser.GetAll().ToList();
            var roleList = _context.Roles.ToList();
            var userRole = _context.UserRoles.ToList();
            foreach (var user in userList)
            {
                var userRoleEntry = userRole.FirstOrDefault(u => u.UserId == user.Id);
                if (userRoleEntry != null)
                {
                    user.Role = roleList.FirstOrDefault(r => r.Id == userRoleEntry.RoleId)?.Name;
                }
                if (user.CompanyId == null)
                {
                    user.Company = new Company()
                    {
                        Name = ""
                    };
                }
                if (user.CompanyId != null)
                {
                    user.Company = new Company()
                    {
                        Name = _unitofwork.Company.Get(Convert.ToInt32(user.CompanyId)).Name
                    };
                }
            }
            var adminUser = userList.FirstOrDefault(u => u.Role == SD.Role_Admin);
            if(adminUser !=null )
                userList.Remove(adminUser);
            return Json(new { data = userList });
        }
        #endregion
    }
}