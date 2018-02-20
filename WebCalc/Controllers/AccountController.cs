using ITUniver.Calc.DB.NH.Repositories;
using ITUniver.Calc.DB.Repositories;
using System.Web.Mvc;
using System.Web.Security;
using WebCalc.Models;

namespace WebCalc.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private IUserRepository Users { get; set; }

        public AccountController()
        {
            Users = new NHUserRepository();
        }



        // GET: Account
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            if(Users.Check(model.Login, model.Password))
            {
                // поставить отметку об аутентификации
                FormsAuthentication.SetAuthCookie(model.Login, true);

                return RedirectToAction("Index", "Calc");
            }

            ModelState.AddModelError("", "Не удалось выполнить вход");

            return View();
        }


        // GET: Account
        public void Logout()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}