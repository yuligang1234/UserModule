using System.Data;
using System.Web.Mvc;
using Napoleon.PublicCommon.Frame;
using Napoleon.PublicCommon.Http;
using Napoleon.UserModule.Common;
using Napoleon.UserModule.IBLL;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.Web.Controllers
{
    public class ProjectController : BaseController
    {

        private IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///  系统列表
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// <param name="projectName">projectName</param>
        /// <param name="rows">The rows.</param>
        /// <param name="page">The page.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-23 16:32:46
        public ActionResult LoadProjectGrid(string projectId, string projectName, int rows, int page)
        {
            projectId = string.IsNullOrWhiteSpace(projectId) ? "" : projectId;
            projectName = string.IsNullOrWhiteSpace(projectName) ? "" : projectName;
            DataTable dt = _projectService.GetProjectTable(projectId, projectName, rows, page);
            int count = _projectService.GetProjectCount();
            string json = dt.ConvertTableToGridJson(count);
            return Content(json);
        }

        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        ///  新增系统
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-24 09:29:42
        public ActionResult SaveAdd(string projectId, string projectName, string remark)
        {
            SystemProject project = new SystemProject();
            project.ProjectId = projectId;
            project.ProjectName = projectName;
            project.Remark = remark;
            project.Operator = PublicFields.UserCookie.ReadCookie<SystemUser>().UserName;
            int count = _projectService.InsertProject(project);
            string status = "failue", msg, json;
            if (count > 0)
            {
                status = "success";
                msg = "添加成功！";
            }
            else if (count == 0)
            {
                msg = "添加失败！";
            }
            else
            {
                msg = "添加失败，该系统代码已经存在，请不要重复添加！";
            }
            json = PublicFunc.ModelToJson(status, msg);
            return Content(json);
        }

        public ActionResult Edit(string projectId)
        {
            ViewData["Project"] = _projectService.GetProjectById(projectId);
            return View();
        }

        /// <summary>
        ///  更新
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="remark">The remark.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 10:37:25
        public ActionResult UpdateProject(string projectId, string projectName, string remark)
        {
            SystemProject project = new SystemProject();
            project.ProjectId = projectId;
            project.ProjectName = projectName;
            project.Remark = remark;
            int count = _projectService.UpdateProject(project);
            string status = "failue", msg = "更新失败!", json;
            if (count > 0)
            {
                status = "success";
                msg = "更新成功!";
            }
            json = PublicFunc.ModelToJson(status, msg);
            return Content(json);
        }

        /// <summary>
        ///  删除系统
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 10:40:18
        public ActionResult DeleteProject(string projectId)
        {
            int count = _projectService.DeleteProject(projectId);
            string status = "failue", msg, json;
            if (count > 0)
            {
                status = "success";
                msg = "删除成功！";
            }
            else
            {
                msg = count == -1 ? "删除失败，请先删除该系统对应的菜单或权限！" : "删除失败！";
            }
            json = PublicFunc.ModelToJson(status, msg);
            return Content(json);
        }

    }
}
