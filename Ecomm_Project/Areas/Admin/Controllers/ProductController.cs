using Ecomm_Project.DataAccess.Repository.IRepository;
using Ecomm_Project.Models;
using Ecomm_Project.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecomm_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(cl => new SelectListItem()
                {
                    Text = cl.Name,
                    Value = cl.id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(ct => new SelectListItem()
                {
                    Text = ct.Name,
                    Value = ct.Id.ToString()
                })
            };
            if (id == null || id == 0)
            {
                return View(productVM);
            }
            productVM.Product = _unitOfWork.Product.Get(id.GetValueOrDefault());
            if (productVM.Product == null)
            {
                return NotFound();
            }
            return View(productVM);
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM)
        {
            ModelState.Remove("Product.ImageUrl");
            ModelState.Remove("Product.Name");

            if (ModelState.IsValid)
            {
                var webRootPath = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if (files.Count() > 0)
                {
                    var fileName = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(files[0].FileName);
                    var uploads = Path.Combine(webRootPath, @"images\Products");

                    if (productVM.Product.Id != 0)
                    {
                        var imageExists = _unitOfWork.Product.Get(productVM.Product.Id).ImageUrl;
                        productVM.Product.ImageUrl = imageExists;
                        if (productVM.Product.ImageUrl != null)
                        {
                            var imagePath = Path.Combine(webRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                            if (System.IO.File.Exists(imagePath))
                            {
                                System.IO.File.Delete(imagePath);
                            }
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\images\Products\" + fileName + extension;
                }
                else
                {
                    if (productVM.Product.Id != 0)
                    {
                        productVM.Product.ImageUrl = _unitOfWork.Product.Get(productVM.Product.Id).ImageUrl;
                    }
                }

                productVM.Product.Name = productVM.Product.Title;

                if (productVM.Product.Id == 0)
                    _unitOfWork.Product.Add(productVM.Product);
                else
                    _unitOfWork.Product.Update(productVM.Product);

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            productVM.CategoryList = _unitOfWork.Category.GetAll().Select(cl => new SelectListItem()
            {
                Text = cl.Name,
                Value = cl.id.ToString()
            });
            productVM.CoverTypeList = _unitOfWork.CoverType.GetAll().Select(ct => new SelectListItem()
            {
                Text = ct.Name,
                Value = ct.Id.ToString()
            });

            return View(productVM);
        }

        #region Apis
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Product.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var productInDb = _unitOfWork.Product.Get(id);
            if (productInDb == null)
                return Json(new { success = false, message = "Unable to Delete Data !!" });

            if (productInDb.ImageUrl != null)
            {
                var webRootPath = _webHostEnvironment.WebRootPath;
                var imagePath = Path.Combine(webRootPath, productInDb.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _unitOfWork.Product.Remove(productInDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Data Deleted Successfully !!" });
        }
        #endregion
    }
}