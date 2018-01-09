using SGGWSupportWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SGGWSupportWeb.Controllers
{
    public class PriorityController : DictionaryBaseController
    {
        public const string URL = "http://webservice.adscan.pl:8090/priority";

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return await base.Index<PriorityViewModel>(URL);
        }

        [HttpGet]
        public async Task<ActionResult> Add()
        {
            return await base.Add<PriorityViewModel>();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(PriorityViewModel model)
        {
            return await Add(URL, model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return await base.Edit<PriorityViewModel>(URL, id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PriorityViewModel model)
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