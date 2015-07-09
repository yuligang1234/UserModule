using System;
using System.Data;
using System.Web.Mvc;
using Napoleon.PublicCommon.Base;
using Napoleon.PublicCommon.Frame;
using Napoleon.PublicCommon.Http;
using Napoleon.UserModule.Common;
using Napoleon.UserModule.IDAL;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.Web.Controllers
{
    public class MenuController : BaseController
    {

        private IMenuDao _menuDao;

        public MenuController(IMenuDao menuDao)
        {
            _menuDao = menuDao;
        }


        public ActionResult Index(string projectId)
        {
            ViewData["ProjectId"] = projectId;
            return View();
        }

        /// <summary>
        ///  加载菜单treegrid
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-24 14:50:39
        public ActionResult LoadMenuGrid(string projectId)
        {
            DataTable dt = _menuDao.GetMenuTable(projectId);
            string json = dt.ConvertToTreeGridJson("Icon");
            return Content(json);
        }

        public ActionResult Add(string projectId)
        {
            ViewData["ProjectId"] = projectId;
            return View();
        }

        /// <summary>
        ///  菜单父节点
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 16:39:33
        public ActionResult LoadParentId(string projectId)
        {
            DataTable dt = _menuDao.GetTreeParentId(projectId);
            string json = dt.ConvertToComboboxJson("Id", "Name");
            return Content(json);
        }

        /// <summary>
        ///  新增菜单
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-24 16:19:31
        public ActionResult SaveMenu(string projectName, string projectId, string parentId, string htmlId, string url, string icon, string sort, string remark)
        {
            SystemMenu menu = new SystemMenu();
            menu.ProjectId = projectId;
            menu.Id = CustomId.GetCustomId();
            menu.ParentId = string.IsNullOrWhiteSpace(parentId) ? "0" : parentId;
            menu.Name = projectName;
            menu.HtmlId = htmlId;
            menu.Url = url;
            menu.Icon = icon;
            menu.Sort = Convert.ToDecimal(sort);
            menu.Remark = remark;
            menu.Operator = PublicFields.UserCookie.ReadCookie<SystemUser>().UserName;
            int count = _menuDao.InsertMenu(menu);
            string status = "failue", msg, json;
            switch (count)
            {
                case -1:
                    msg = "添加失败，该菜单名称已经存在，请不要重复添加！";
                    break;
                case 1:
                    status = "success";
                    msg = "添加成功！";
                    break;
                default:
                    msg = "添加失败！";
                    break;
            }
            json = PublicFunc.ModelToJson(status, msg);
            return Content(json);
        }

        public ActionResult Edit(string id)
        {
            ViewData["Menu"] = _menuDao.GetMenu(id);
            return View();
        }

        /// <summary>
        ///  根据ID更新信息
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-26 16:21:38
        public ActionResult UpdateMenu(string id, string projectName, string parentId, string htmlId, string url, string icon, string sort, string remark)
        {
            SystemMenu menu = new SystemMenu();
            menu.Id = id;
            menu.Name = projectName;
            menu.ParentId = parentId;
            menu.HtmlId = htmlId;
            menu.Url = url;
            menu.Icon = icon;
            menu.Sort = Convert.ToDecimal(sort);
            menu.Remark = remark;
            menu.Operator = PublicFields.UserCookie.ReadCookie<SystemUser>().UserName;
            int count = _menuDao.UpdateMenu(menu);
            string status = "failue", msg, json;
            switch (count)
            {
                case -1:
                    msg = "该菜单已经存在，请不要使用重复的菜单名！";
                    break;
                case 1:
                    status = "success";
                    msg = "更新成功！";
                    break;
                default:
                    msg = "更新失败！";
                    break;
            }
            json = PublicFunc.ModelToJson(status, msg);
            return Content(json);
        }

        /// <summary>
        ///  删除系统
        /// </summary>
        /// <param name="id">id</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 10:40:18
        public ActionResult DeleteMenu(string id)
        {
            int count = _menuDao.DeleteMenu(id);
            string status = "failue", msg, json;
            if (count > 0)
            {
                status = "success";
                msg = "删除成功！";
            }
            else
            {
                msg = count == -1 ? "删除失败，请先删除该对应的子菜单！" : "删除失败！";
            }
            json = PublicFunc.ModelToJson(status, msg);
            return Content(json);
        }


    }
}
