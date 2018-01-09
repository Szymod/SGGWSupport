using SGGWSupportWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SGGWSupportWeb.Controllers
{
    public class CategoryController : DictionaryBaseController
    {
        public const string URL = "http://webservice.adscan.pl:8090/category";

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return await base.Index<CategoryViewModel>(URL);
        }

        [HttpGet]
        public async Task<ActionResult> Add()
        {
            return await base.Add<CategoryViewModel>();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(CategoryViewModel model)
        {
            return await Add(URL, model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return await base.Edit<CategoryViewModel>(URL, id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CategoryViewModel model)
        {
            return await Edit(URL, model);
        }

        [HttpGet]
        protected async Task<ActionResult> Delete(int id)
        {
            return await base.Delete(URL, id);
        }
    }
}