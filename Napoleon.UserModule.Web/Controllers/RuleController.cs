using System.Data;
using System.Web.Mvc;
using Napoleon.PublicCommon;
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
            rule.Operator = CookieSessionFunc.ReadCookie<SystemUser>(PublicFields.UserCookie).UserName;
            int count = _ruleService.InsertRule(rule);
            string result;
            switch (count)
            {
                case -1:
                    result = "添加失败,该角色名称已经存在,请不要重复添加！";
                    break;
                case 1:
                    result = "添加成功！";
                    break;
                default:
                    result = "添加失败！";
                    break;
            }
            return Content(result);
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
            rule.Operator = CookieSessionFunc.ReadCookie<SystemUser>(PublicFields.UserCookie).UserName;
            int count = _ruleService.UpdateRule(rule);
            string result;
            switch (count)
            {
                case -1:
                    result = "更新失败，角色名称重复！";
                    break;
                case 1:
                    result = "更新成功！";
                    break;
                default:
                    result = "更新失败！";
                    break;
            }
            return Content(result);
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
            string result;
            switch (count)
            {
                case -1:
                    result = "删除失败，请先删除对应父节点！";
                    break;
                case 1:
                    result = "删除成功！";
                    break;
                default:
                    result = "删除失败！";
                    break;
            }
            return Content(result);
        }

    }
}
