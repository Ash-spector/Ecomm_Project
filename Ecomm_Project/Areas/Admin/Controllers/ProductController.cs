using Ecomm_Project.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        public ProductController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region Api's
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitofwork.Product.GetAll() });
        }
        
        #endregion
    }       
}