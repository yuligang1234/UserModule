using System.Data;
using System.Web.Mvc;
using Napoleon.PublicCommon.Base;
using Napoleon.PublicCommon.Frame;
using Napoleon.PublicCommon.Http;
using Napoleon.UserModule.Common;
using Napoleon.UserModule.IBLL;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.Web.Controllers
{
    public class RuleController : Controller
    {

        private IRuleService _ruleService;

        public RuleController(IRuleService ruleService)
        {
            _ruleService = ruleService;
        }

        public ActionResult Index(string projectId)
        {
            ViewData["ProjectId"] = projectId;
            return View();
        }

        /// <summary>
        ///  加载权限列表
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-27 15:56:55
        public ActionResult LoadRuleGrid(string projectId)
        {
            DataTable dt = _ruleService.GetRuleTable(projectId);
            string json = dt.ConvertToTreeGridJson();
            return Content(json);
        }

        public ActionResult Add(string projectId)
        {
            ViewData["ProjectId"] = projectId;
            return View();
        }

        /// <summary>
        ///  加载
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-27 15:50:36
        public ActionResult LoadParentId(string projectId)
        {
            DataTable dt = _ruleService.GetRuleCombobox(projectId, PublicFields.RuleParentId);
            string json = dt.ConvertToComboboxJson("Id", "Name");
            return Content(json);
        }

        /// <summary>
        ///  保存新增
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="parentId">The parent identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="sort">The sort.</param>
        /// <param name="remark">The remark.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-27 16:03:55
        public ActionResult SaveRule(string projectId, string parentId, string name, decimal sort, string remark)
        {
            SystemRule rule = new SystemRule();
            rule.ProjectId = projectId;
            rule.Id = CustomId.GetCustomId();
            rule.ParentId = string.IsNullOrWhiteSpace(parentId) ? "0" : parentId;
            rule.Name = name;
            rule.Sort = sort;
            rule.Remark = remark;
            rule.Operator = PublicFields.UserCookie.ReadCookie<SystemUser>().UserName;
            int count = _ruleService.InsertRule(rule);
            string status = "failue", msg, json;
            switch (count)
            {
                case -1:
                    msg = "添加失败,该角色名称已经存在,请不要重复添加！";
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
            ViewData["Rule"] = _ruleService.GetRule(id);
            return View();
        }

        /// <summary>
        ///  更新角色
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="parentId">The parent identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="sort">The sort.</param>
        /// <param name="remark">The remark.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-27 16:27:19
        public ActionResult UpdateRule(string projectId, string id, string parentId, string name, decimal sort, string remark)
        {
            SystemRule rule = new SystemRule();
            rule.ProjectId = projectId;
            rule.Id = id;
            rule.ParentId = string.IsNullOrWhiteSpace(parentId) ? "0" : parentId;
            rule.Name = name;
            rule.Sort = sort;
            rule.Remark = remark;
            rule.Operator = PublicFields.UserCookie.ReadCookie<SystemUser>().UserName;
            int count = _ruleService.UpdateRule(rule);
            string status = "failue", msg, json;
            switch (count)
            {
                case -1:
                    msg = "更新失败，角色名称重复！";
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
        ///  删除角色
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-27 16:42:22
        public ActionResult DeleteRule(string id)
        {
            int count = _ruleService.DeleteRule(id);
            string status = "failue", msg, json;
            switch (count)
            {
                case -1:
                    msg = "删除失败，请先删除对应父节点！";
                    break;
                case 1:
                    status = "success";
                    msg = "删除成功！";
                    break;
                default:
                    msg = "删除失败！";
                    break;
            }
            json = PublicFunc.ModelToJson(status, msg);
            return Content(json);
        }

    }
}
