using Microsoft.AspNetCore.Mvc;
using MyBookStore.DataAccess;
using MyBookStore.DataAccess.Repository.IRepository;
using MyBookStore.Models;

namespace MyBookStore.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        //private readonly AppDbContext _db;
        //private readonly ICategoryRepository _db
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAllValues();
            return View(objCategoryList);
        }

        //GET
        public IActionResult Add()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Category obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Name == obj.DisplayOrder.ToString())
                {
                    ModelState.AddModelError("Name", "Name cannot be same as Display Order");
                    return View(obj);
                }
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Category.Save();
                TempData["success"] = "Category Added Successfully!";
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
            var categoryFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (categoryFirst == null)
            {
                return NotFound();
            }

            return View(categoryFirst);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Category obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Name == obj.DisplayOrder.ToString())
                {
                    ModelState.AddModelError("Name", "Name cannot be same as Display Order");
                    return View(obj);
                }
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Category.Save();
                TempData["success"] = "Category Updated Successfully!";
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
            var category = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        //POST for Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Delete(obj);
            _unitOfWork.Category.Save();
            TempData["success"] = "Category Deleted Successfully!";
            return RedirectToAction("Index");
        }
    }
}
