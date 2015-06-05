
using System;
using System.Web;
using Napoleon.Log4Module.Log;
using Napoleon.Log4Module.Log.Common;
using Napoleon.Log4Module.Log.Model;
using Napoleon.PublicCommon.Http;

namespace Napoleon.UserModule.DAL
{
    public class Log4Dao
    {

        /// <summary>
        ///  写入错误日志
        /// </summary>
        /// <param name="msg">日志信息</param>
        /// <param name="insertType">写入方法(数据库/记事本)</param>
        /// <param name="logType">(错误/信息)</param>
        /// Author  : Napoleon
        /// Created : 2015-06-05 09:47:20
        public static void InsertLog4(string msg, LogType logType = LogType.Error, InsertType insertType = InsertType.All)
        {
            SystemLog log = new SystemLog();
            log.IpAddress = IpFunc.GetIp();
            log.OperateTime = DateTime.Now;
            log.OperateUrl = HttpContext.Current.Request.Url.ToString();
            log.UserName = "系统";
            log.OperateType = "系统错误";
            log.OperateContent = msg;
            log.InsertLog(logType, insertType);
        }

    }
}
