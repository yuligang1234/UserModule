using System.Web.Mvc;
using Napoleon.PublicCommon;
using Napoleon.PublicCommon.Http;
using Napoleon.UserModule.Common;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.Web.Controllers
{
    public class HomeController : BaseController
    {



        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HomeRight()
        {
            return View();
        }

        public ActionResult ChangePw()
        {
            SystemUser user = PublicFields.UserCookie.ReadCookie<SystemUser>();
            ViewData["User"] = user;
            return View();
        }

    }
}
