using ConsoleCalc;
using ITUniver.Calc.DB.Models;
using ITUniver.Calc.DB.NH.Repositories;
using ITUniver.Calc.DB.Repositories;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using WebCalc.Models;

namespace WebCalc.Controllers
{
    [Authorize]
    public class CalcController : Controller
    {
        private IHistoryRepository HistoryRepository
            = new NHHistoryRepository();

        private IOperationRepository OperationRepository
            = new OperationRepository();

        private IUserRepository UserRepository
            = new NHUserRepository();

        private Calc calc { get; set; }

        public CalcController()
        {
            var extPath = ConfigurationManager.AppSettings["ExtensionPath"];
            calc = new Calc(extPath);
        }

        // GET: Calc
        [AllowAnonymous]
        public ActionResult Index()
        {
            var model = new OperationModel();

            model.Operations = calc.GetOpers().Select(
                o => new SelectListItem()
                {
                    Text = $"{o.Name}",
                    Value = $"{o.Name}"
                });

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Exec(OperationModel model)
        {            
            var sw = new Stopwatch();
                        
            model.Operations = calc.GetOpers().Select(o => new SelectListItem() { Text = $"{o.Name}", Value = $"{o.Name}" });
            
            sw.Start();
            model.Result = calc.Exec(model.Operation, model.Args.ToArray());
            sw.Stop();

            #region Save

            if (User.Identity.IsAuthenticated)
            {
                var item = new HistoryItem();
                item.Args = string.Join(" ", model.Args);
                item.Operation = OperationRepository
                    .GetAll($"[Name] = N'{model.Operation}'")
                    .FirstOrDefault()
                    .Id;
                item.Result = model.Result;
                item.ExecDate = DateTime.Now;
                item.Author = UserRepository.GetByName(User.Identity.Name);
                item.TimeCalculation = sw.ElapsedMilliseconds;

                HistoryRepository.Save(item);
            }

            #endregion

            return Json(new { model.Result });
        }

        // GET: Calc
        public ActionResult History()
        {
            var history = User.IsInRole("admin")
                ? HistoryRepository.GetAll()
                : UserRepository.GetByName(User.Identity.Name).History;

            return View(history);
        }
    }
}