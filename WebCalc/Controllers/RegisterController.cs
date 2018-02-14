using ITUniver.Calc.DB.Models;
using ITUniver.Calc.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCalc.Models;

namespace WebCalc.Controllers
{
    public class RegisterController : Controller
    {
        private IUserRepository Users { get; set; }
        private IBaseRepository<User> UsersDB = new BaseRepository<User>("User");

        public RegisterController()
        {
            Users = new UserRepository();
        }


        // GET: Register
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

            // проверить не занят ли логин
            if(!Users.Check(model.Login))
            {
                // добавить пользователя в базу данных
                AddToUserDB(model.Login, model.Name, model.Password, model.BirthDay, model.Sex);

                return RedirectToAction("Login", "Account");
            }

            ModelState.AddModelError("", "Не удалось выполнить регистрацию");

            return View();
        }

        public void AddToUserDB(string login, string name, string password, DateTime birthday, bool? sex)
        {
            
            var user = new User();
            user.Id = Users.GetAll().Max(u => u.Id) + 1; 
            user.Login = login;
            user.Name = name;
            user.Password = password;
            user.BirthDay = birthday;
            user.Sex = sex;

            UsersDB.Save(user);
        }
    }
}