using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class PageErrorController : Controller
    {
        //
        // GET: /PageError/

        public ActionResult Error404()
        {
            return View();
        }

    }
}
