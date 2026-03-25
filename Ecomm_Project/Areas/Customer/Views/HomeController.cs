using Ecomm_Project.DataAccess.Repository.IRepository;
using Ecomm_Project.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ecomm_Project.Areas.Customer.Views
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        public HomeController (IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        
        public IActionResult Index()
        {
            var productList = _unitofwork.Product.GetAll();
            return View(productList);
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
    }
}
