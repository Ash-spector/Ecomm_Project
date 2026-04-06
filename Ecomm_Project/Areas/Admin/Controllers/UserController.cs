using Ecomm_Project.DataAccess.Data;
using Ecomm_Project.DataAccess.Repository.IRepository;
using Ecomm_Project.Models;
using Ecomm_Project.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
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
        [HttpPost]
        public IActionResult LockUnlock([FromBody]string id)
        {
            bool isLocked = false;
            var userInDb = _unitofwork.ApplicationUser.FirstorDefault(u => u.Id == id);
            if(userInDb == null)
            {
                return Json(new { success = false, message = "Something went wrong while lock and unlock user !!" });
            }
            if(userInDb != null && userInDb.LockoutEnd > DateTime.Now)
            {
                userInDb.LockoutEnd = DateTime.Now;
                isLocked = false;
            }
            else
            {
                userInDb.LockoutEnd = DateTime.Now.AddYears(100);
                isLocked = true;
            }
            _context.SaveChanges();
            return Json(new { success = true, message = isLocked == true ?
                "User Successfully locked" : "User Successfully Unlocked"});
        }
        #endregion
    }
}