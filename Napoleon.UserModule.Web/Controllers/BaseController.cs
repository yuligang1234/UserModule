using System.Web;
using System.Web.Mvc;
using Napoleon.PublicCommon;
using Napoleon.UserModule.Common;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.Web.Controllers
{
    public class BaseController : Controller
    {

        private bool IsLogin
        {
            get
            {
                return (CookieSessionFunc.ReadCookie<SystemUser>(PublicFields.UserCookie) != null);
            }
        }

        /// <summary>
        ///  判断是否登录，登录超时就跳转登录页面
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2014-07-29 14:29:10
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //禁止页面被缓存
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!IsLogin)
            {
                filterContext.Result = RedirectToRoute("Default", new { Controller = "Error", Action = "Message" });
            }
            base.OnActionExecuting(filterContext);
        }

    }
}
