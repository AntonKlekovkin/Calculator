using ITUniver.Calc.DB.Models;
using ITUniver.Calc.DB.NH.Repositories;
using ITUniver.Calc.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCalc.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private IUserRepository Users { get; set; }

        public ManageController()
        {
            Users = new NHUserRepository();
        }

        // GET: Manage
        public ActionResult Index()
        {
            var users = Users.GetAll();
                
            return View(users);
        }

        public ActionResult Delete(long id)
        {
            var user = Users.Find(id);
            if (user != null)
            {
                user.Status = UserStatus.Deleted;
                Users.Save(user);
            }
            return RedirectToAction("Index");
        }

    }
}