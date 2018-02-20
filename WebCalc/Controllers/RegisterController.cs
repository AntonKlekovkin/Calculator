using ITUniver.Calc.DB.Models;
using ITUniver.Calc.DB.NH.Repositories;
using ITUniver.Calc.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebCalc.Models;

namespace WebCalc.Controllers
{
    public class RegisterController : Controller
    {
        private IUserRepository Users { get; set; }
       
        public RegisterController()
        {
            Users = new NHUserRepository();
        }


        // GET: Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (Users.GetByName(model.Login) == null)
            {
                var user = new User()
                {
                    Login = model.Login,
                    Name = model.Name,
                    Password = model.Password,
                    Sex = model.Sex,
                    BirthDay = model.BirthDay,
                    Status = UserStatus.Active
                };
                Users.Save(user);

                // поставить отметку о аутентификации
                FormsAuthentication.SetAuthCookie(model.Login, true);

                return RedirectToAction("Index", "Calc");
            }
            else
            {
                ModelState.AddModelError("Login", "Придумайте другое имя. Это уже занято");
            }

            return View();
        }

    }
}