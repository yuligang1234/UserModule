using System;
using System.Web.Mvc;
using Napoleon.Log4Module.Log;
using Napoleon.Log4Module.Log.Common;
using Napoleon.Log4Module.Log.Model;
using Napoleon.PublicCommon.Http;
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
                return (PublicFields.UserCookie.ReadCookie<SystemUser>() != null);
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
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            /*filterContext.RouteData.Values["action"].Equals("LoadRuleGrid") ||*/
            if ( !IsLogin)
            {
                filterContext.Result = RedirectToRoute("Default", new { Controller = "Error", Action = "Message" });
            }
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        ///  action异常捕获
        /// </summary>
        /// <param name="filterContext">有关当前请求和操作的信息。</param>
        /// Author  : Napoleon
        /// Created : 2015-06-05 11:10:24
        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;//已经处理异常
            SystemLog log = new SystemLog();
            log.IpAddress = IpFunc.GetIp();
            log.OperateTime = DateTime.Now;
            log.OperateUrl = System.Web.HttpContext.Current.Request.Url.ToString();
            log.UserName = "系统";
            log.OperateType = "DAL层错误";
            log.OperateContent = filterContext.Exception.Message;
            log.InsertLog(LogType.Error, InsertType.All);
            //页面跳转
            filterContext.Result = RedirectToRoute("Default", new { Controller = "Error", Action = "Index" });
            base.OnException(filterContext);
        }

    }
}
