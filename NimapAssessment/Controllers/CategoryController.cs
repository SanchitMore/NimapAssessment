using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NimapAssessment.Models;

namespace NimapAssessment.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IConfiguration configuration;
        private CategoryCRUD db;
        public CategoryController(IConfiguration configuration)
        {
            this.configuration = configuration;
            db=new CategoryCRUD(this.configuration);
        }
        // GET: CategoryController
        public ActionResult Index()
        {
            var list = db.GetCategories();
            return View(list);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category c)
        {
            try
            {
                int result=db.AddCategory(c);
                if(result >= 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["message"] = "Not able to add record!";
                    return View();
                }
            }
            catch(Exception ex)
            {
                TempData["exp"]=ex.Message;
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var cat = db.GetCategoryById(id);
            return View(cat);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category cat)
        {
            try
            {
                int result = db.UpdateCategory(cat);
                if(result >= 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["message"] = "Not able to update record!";
                    return View();
                }
            }
            catch(Exception ex)
            {
                TempData["exp"]= ex.Message;
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var cat = db.GetCategoryById(id);
            return View(cat);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                int result = db.DeleteCategory(id);
                if (result >= 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["message"] = "Not able to delete record!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["exp"] = ex.Message;
                return View();
            }
        }
    }
}
