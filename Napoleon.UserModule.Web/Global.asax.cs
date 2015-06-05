using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Napoleon.UserModule.DAL;

namespace Napoleon.UserModule.Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleTable.EnableOptimizations = true;//打包压缩css和js文件(开发的时候可以关闭压缩)
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //autofac注入
            AuthConfig.InitAutofc();
        }

        /// <summary>
        ///  错误信息
        /// </summary>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-10-27 14:17:41
        protected void Application_Error(object sender, EventArgs e)
        {
            string msg;
            Exception exception = Server.GetLastError();
            Response.Clear();
            HttpException httpException = exception as HttpException;
            string url = "/Error/Index";
            if (httpException != null)
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        url = "/Error/NoFound";
                        break;
                }
                Server.ClearError();
                msg = httpException.Message;
            }
            else
            {
                Server.ClearError();
                msg = exception.Message;
            }
            Log4Dao.InsertLog4(msg);
            Response.Write("<script>top.window.location.href='" + url + "'</script>");
        }
    }
}