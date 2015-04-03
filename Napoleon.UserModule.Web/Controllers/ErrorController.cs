using System.Web.Mvc;

namespace Napoleon.UserModule.Web.Controllers
{
    public class ErrorController : Controller
    {



        /// <summary>
        ///  报错页面
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2014-09-24 09:59:30
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///  404错误
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2014-09-24 09:59:30
        public ActionResult NoFound()
        {
            return View();
        }

        /// <summary>
        ///  信息提示
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-30 16:16:48
        public ActionResult Message()
        {
            return View();
        }


    }
}
