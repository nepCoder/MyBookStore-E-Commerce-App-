using Microsoft.AspNetCore.Mvc;
using MyBookStore.DataAccess.Repository.IRepository;
using MyBookStore.Models;

namespace MyBookStore.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objProductList = _unitOfWork.Product.GetAllValues();
            return View(objProductList);
        }

        //GET
        public IActionResult Add()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Product.Save();
                TempData["success"] = "Product Added Successfully!";
                return RedirectToAction("Index");
            }

            return View(obj);

        }

        //GET
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var productFirst = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (productFirst == null)
            {
                return NotFound();
            }

            return View(productFirst);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Product.Save();
                TempData["success"] = "Product Updated Successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //GET for Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        //POST for Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Delete(obj);
            _unitOfWork.Product.Save();
            TempData["success"] = "Product Deleted Successfully!";
            return RedirectToAction("Index");
        }
    }
}
