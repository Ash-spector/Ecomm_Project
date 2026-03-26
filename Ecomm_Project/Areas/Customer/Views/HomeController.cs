using Ecomm_Project.DataAccess.Repository;
using Ecomm_Project.DataAccess.Repository.IRepository;
using Ecomm_Project.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ecomm_Project.Areas.Customer.Views
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;  // ← Fixed casing

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;  // ← Fixed casing
        }

        public IActionResult Index()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
            return View(productList);
        }

        public IActionResult Details(int id)
        {
            var product = _unitOfWork.Product.Get(id);
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region Api's
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties: "Category");  // ← Fixed casing
            return Json(new { data = productList });
        }
        #endregion
    }
}