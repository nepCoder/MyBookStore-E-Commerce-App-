using Microsoft.AspNetCore.Mvc;
using MyBookStore.DataAccess;
using MyBookStore.DataAccess.Repository.IRepository;
using MyBookStore.Models;

namespace MyBookStore.Areas.Admin.Controllers
{
    public class CoverTypeController : Controller
    {
        //private readonly AppDbContext _db;
        //private readonly ICategoryRepository _db
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> objCategoryList = _unitOfWork.CoverType.GetAllValues();
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
        public IActionResult Add(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Name == obj.Id.ToString())
                {
                    ModelState.AddModelError("Name", "Name cannot be same as ID");
                    return View(obj);
                }
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.CoverType.Save();
                TempData["success"] = "Cover Type Added Successfully!";
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
            var categoryFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (categoryFirst == null)
            {
                return NotFound();
            }

            return View(categoryFirst);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Name == obj.Id.ToString())
                {
                    ModelState.AddModelError("Name", "Name cannot be same as Display Order");
                    return View(obj);
                }
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.CoverType.Save();
                TempData["success"] = "Cover Type Updated Successfully!";
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
            var coverType = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (coverType == null)
            {
                return NotFound();
            }

            return View(coverType);
        }

        //POST for Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.CoverType.Delete(obj);
            _unitOfWork.CoverType.Save();
            TempData["success"] = "Cover Type Deleted Successfully!";
            return RedirectToAction("Index");
        }
    }
}
