using System.Web.Mvc;
using Napoleon.PublicCommon;
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
            SystemUser user = CookieSessionFunc.ReadCookie<SystemUser>(PublicFields.UserCookie);
            ViewData["User"] = user;
            return View();
        }

    }
}
