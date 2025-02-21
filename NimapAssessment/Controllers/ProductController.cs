using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NimapAssessment.Models;

namespace NimapAssessment.Controllers
{
    public class ProductController : Controller
    {
        private readonly IConfiguration configuration;
        private ProductCRUD pc;

        public ProductController(IConfiguration configuration)
        {
            this.configuration = configuration;
            pc=new ProductCRUD(this.configuration);
        }
        // GET: ProductController
        public ActionResult Index(int pg=1)
        {
            const int pagesize = 5;
            if (pg < 1)
            {
                pg = 1;
            }
            int recscount = pc.GetProducts().Count();
            var pager=new Pager(recscount,pg,pagesize);
            int recskip = (pg - 1) * pagesize;
            var data=pc.GetProducts().Skip(recskip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(data);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product p)
        {
            try
            {
                int result = pc.AddProduct(p);
                if (result >= 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["message"] = "Not able to add record!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["exp"] = ex.Message;
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var cat = pc.GetProductById(id);
            return View(cat);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product pro)
        {
            try
            {
                int result = pc.UpdateProduct(pro);
                if (result >= 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["message"] = "Not able to update record!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["exp"] = ex.Message;
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            var cat = pc.GetProductById(id);
            return View(cat);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                int result = pc.DeleteProduct(id);
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
