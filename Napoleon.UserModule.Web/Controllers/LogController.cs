using System;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Web.Mvc;
using Napoleon.Log4Module.Log.DAL;
using Napoleon.Log4Module.Log.Model;
using Napoleon.PublicCommon.File;
using Napoleon.PublicCommon.Frame;
using Napoleon.PublicCommon.Office;

namespace Napoleon.UserModule.Web.Controllers
{
    public class LogController : BaseController
    {



        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///  加载表格
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="content">The content.</param>
        /// <param name="datetime1">The datetime1.</param>
        /// <param name="datetime2">The datetime2.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="page">The page.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-17 11:00:34
        public ActionResult LoadDataGrid(string userName, string content, string datetime1, string datetime2, int rows, int page)
        {
            SystemLog log = new SystemLog();
            log.UserName = userName;
            log.OperateContent = content;
            datetime1 = string.IsNullOrWhiteSpace(datetime1) ? SqlDateTime.MinValue.ToString() : datetime1;
            datetime2 = string.IsNullOrWhiteSpace(datetime2) ? SqlDateTime.MaxValue.ToString() : datetime2;
            DataTable dt = log.SelectLog(datetime1, datetime2, rows * (page - 1), rows * page);
            int count = log.LogCount(datetime1, datetime2);
            string json = dt.ConvertTableToGridJson(count);
            return Content(json);
        }

        /// <summary>
        ///  导出Excel
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="content">The content.</param>
        /// <param name="datetime1">The datetime1.</param>
        /// <param name="datetime2">The datetime2.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-14 14:15:33
        public ActionResult ExcelLog(string userName, string content, string datetime1, string datetime2)
        {
            string fileName = "系统日志" + DateTime.Now.ToString("yyyyMMddhhmmssff") + ".xls";
            string filePath = Server.MapPath("../Export/");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            SystemLog log = new SystemLog();
            log.UserName = userName;
            log.OperateContent = content;
            datetime1 = string.IsNullOrWhiteSpace(datetime1) ? SqlDateTime.MinValue.ToString() : datetime1;
            datetime2 = string.IsNullOrWhiteSpace(datetime2) ? SqlDateTime.MaxValue.ToString() : datetime2;
            DataTable dt = log.SelectLogTable(datetime1, datetime2);
            string[] titles = { "ID", "操作用户", "IP地址", "日志时间", "日志类型", "错误地址", "日志内容" };
            string[] columns = { "Id", "UserName", "IpAddress", "OperateTime", "OperateType", "OperateUrl", "OperateContent" };
            MemoryStream fileStream = dt.CreateSheet(titles, columns);
            fileStream.Seek(0, SeekOrigin.Begin);
            fileStream.StreamToFile(filePath + fileName);
            return Content("../Export/" + fileName);
            //return File(fileStream, "application/vnd.ms-excel", PublicFields.LogExcelName);
        }

        public ActionResult ViewInfo(string id)
        {
            ViewData["Info"] = UserDao.GetSystemLog(id);
            return View();
        }

    }
}
