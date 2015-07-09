using System.Web.Mvc;
using Napoleon.PublicCommon.Http;
using Napoleon.UserModule.Common;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.Web.Common
{
    public class SupportFilterAttribute : ActionFilterAttribute
    {

        private bool IsLogin
        {
            get
            {
                return (PublicFields.UserCookie.ReadCookie<SystemUser>() != null);
            }
        }

        /// <summary>
        ///  判断是否登录，登录超时就跳转登录页面
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2014-07-29 14:29:10
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //禁止页面被缓存
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!IsLogin)
            {
                //filterContext.Result = RedirectToRoute("Default", new { Controller = "Login", Action = "Index" });
                filterContext.HttpContext.Response.Write("<script type='text/javascript'> top.window.location.href='Login/Index'; </script>");
                //filterContext.Result = new EmptyResult();
            }
            //base.OnActionExecuting(filterContext);
        }

    }
}