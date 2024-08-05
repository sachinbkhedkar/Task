using Domain.Interface;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Web.Controllers
{
    public class UserFormController : Controller
    {
        private readonly FormInterface _repo;
        public UserFormController(FormInterface _repo) { 
            this._repo = _repo;
        }

        public IActionResult Index()
        {

            return View();
        }
        public IActionResult GetUserList()
        {
            return Json(_repo.GetUserList());
        }
        public IActionResult AddPopup()
        {
            ViewBag.State = new List<object>

            {
                  new { State= "Maharashtra" },
     new { State= "Gujarat" }


            };
            return PartialView("AddUserForm",new _FormViewModel());
        }
        public IActionResult EditPopup(int Id)
        {
            ViewBag.State = new List<object>

            {
                  new { State= "Maharashtra" },
     new { State= "Gujarat" }


            };
            var model=_repo.GetModelForEdit(Id);
            return PartialView("EditUserForm", model);
        }
        public IActionResult AddUser(_FormViewModel model)
        {
            if(ModelState.IsValid||model.Id!=0)
            {
                _repo.AddUser(model);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int Id)
        {
            return Json(_repo.DeleteUser(Id));
        }
      
    }
}
