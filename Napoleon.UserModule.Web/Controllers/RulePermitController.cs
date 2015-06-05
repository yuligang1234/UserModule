using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Mvc;
using Napoleon.PublicCommon.Frame;
using Napoleon.UserModule.IBLL;
using Napoleon.UserModule.Model;
using Newtonsoft.Json;

namespace Napoleon.UserModule.Web.Controllers
{
    public class RulePermitController : Controller
    {

        private IMenuService _menuService;
        private IMenuAndRuleService _menuAndRuleService;

        public RulePermitController(IMenuService menuService, IMenuAndRuleService menuAndRuleService)
        {
            _menuService = menuService;
            _menuAndRuleService = menuAndRuleService;
        }


        public ActionResult Index(string projectId, string ruleId)
        {
            ViewData["ProjectId"] = projectId;
            ViewData["RuleId"] = ruleId;
            return View();
        }


        /// <summary>
        ///  加载角色权限树
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="ruleId">The rule identifier.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-30 21:21:44
        public ActionResult LoadTree(string projectId, string ruleId)
        {
            DataTable menus = _menuService.GetMenuTable(projectId);
            DataTable operations = _menuAndRuleService.GetOperatorTable(projectId, ruleId);
            foreach (DataRow menu in menus.Rows)
            {
                foreach (DataRow operation in operations.Rows)
                {
                    if (menu["Id"].Equals(operation["OperationId"]) && !menu["ParentId"].Equals("0"))
                    {
                        menu["checked"] = "true";
                        break;
                    }
                }
            }
            string json = menus.ConvertToTreeJson(true);
            return Content(json);
        }

        /// <summary>
        ///  更新角色权限
        /// </summary>
        /// <param name="json">The json.</param>
        /// Author  : Napoleon
        /// Created : 2015-02-04 20:44:19
        public ActionResult SaveRule(string json)
        {
            json = HttpUtility.UrlDecode(json);
            List<SystemMenuAndRule> list = JsonConvert.DeserializeObject<List<SystemMenuAndRule>>(json);
            int count = _menuAndRuleService.UpdateRuleMenu(list[0].RuleId, list[0].ProjectId, list);
            string result = count > 0 ? "更新成功，请重新登陆！" : "更新失败！";
            return Content(result);
        }

    }
}
