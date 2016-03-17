using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FitnessStuff.Models;

namespace FitnessStuff.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        #region For Men
        public ActionResult ForMen()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForMen(MaleModel model)
        {
            return View(model);
        }
        #endregion

        #region For Women
        public ActionResult ForWomen()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForWomen(FemaleModel model)
        {
            return View(model);
        }
        #endregion
    }
}