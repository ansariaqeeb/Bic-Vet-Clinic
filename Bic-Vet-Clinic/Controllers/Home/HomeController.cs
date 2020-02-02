using Bic_Vet_Clinic.Filters;
using System.Web.Mvc;

namespace Bic_Vet_Clinic.Controllers.Home
{
    [CustomSessionAttribute]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //Action method for loading menus on layout
        public ActionResult Menu(int id)
        {
            ViewBag.id = id;
            return PartialView();
        } 
    }
}