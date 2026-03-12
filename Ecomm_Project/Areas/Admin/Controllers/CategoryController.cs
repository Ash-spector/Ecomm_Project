using Ecomm_Project.DataAccess.Repository.IRepository;
using Ecomm_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitofwork;

        public CategoryController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Category category = new Category();

            if (id == null || id == 0)
            {
                return View(category);
            }

            category = _unitofwork.Category.Get(id.Value);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        #region APIs

        [HttpGet]
        public IActionResult GetAll()
        {
            var CategoryList = _unitofwork.Category.GetAll();
            return Json(new { data = CategoryList });
        }

        #endregion
    }
}