using SGGWSupportWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SGGWSupportWeb.Controllers
{
    public class StateController : DictionaryBaseController
    {
        public const string URL = "http://webservice.adscan.pl:8090/statuses";

        public async Task<ActionResult> Index()
        {
            return await base.Index<StateViewModel>(URL);
        }

        [HttpGet]
        public async Task<ActionResult> Add()
        {
            return await base.Add<StateViewModel>();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(StateViewModel model)
        {
            return await Add(URL, model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return await base.Edit<StateViewModel>(URL, id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(StateViewModel model)
        {
            return await Edit(URL, model);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            return await base.Delete(URL, id);
        }
    }
}