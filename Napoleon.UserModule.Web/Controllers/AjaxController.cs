using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Mvc;
using Napoleon.PublicCommon.Frame;
using Napoleon.PublicCommon.Http;
using Napoleon.UserModule.Common;
using Napoleon.UserModule.IBLL;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.Web.Controllers
{
    public class AjaxController : BaseController
    {

        private IMenuAndRuleService _menuAndRuleService;

        public AjaxController(IMenuAndRuleService menuAndDepartmentService)
        {
            _menuAndRuleService = menuAndDepartmentService;
        }

        #region Easyui公共函数

        /// <summary>
        ///  获取菜单操作
        /// </summary>
        /// <param name="menuId">menuId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-12 20:06:03
        public ActionResult GetOperate(string menuId)
        {
            string ruleId = PublicFields.RuleIdCookies.ReadCookie();
            List<SystemMenu> operations = _menuAndRuleService.GetOperation(ruleId, menuId, PublicFields.ProjectId);
            string html = FormatOperate(operations);
            return Content(html);
        }

        /// <summary>
        ///  将菜单操作格式化成html
        /// </summary>
        /// <param name="menus">operation</param>
        /// Author  : Napoleon
        /// Created : 2015-01-12 20:11:46
        private string FormatOperate(List<SystemMenu> menus)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<td colspan=\"{0}\">", menus.Count);
            foreach (SystemMenu menu in menus)
            {
                sb.AppendFormat(
                    "<div style=\"float:left;\"><a href=\"javascript:void(0);\" id=\"{0}\" class=\"easyui-linkbutton\" data-options=\"iconCls:'{1}',plain:true\" style=\"float: left;\">{2}</a> <div class=\"datagrid-btn-separator\"></div></div>",
                    menu.HtmlId, menu.Icon, menu.Name);
            }
            sb.Append("</td>");
            return sb.ToString();
        }

        /// <summary>
        ///  树节点
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-10 19:46:37
        public ActionResult GetTree()
        {
            string ruleId = PublicFields.RuleIdCookies.ReadCookie();
            DataTable dt = _menuAndRuleService.GetMenuTable(ruleId, PublicFields.ProjectId);
            string json = dt.ConvertToTreeJson(false);
            return Content(json);
        }


        #endregion


    }
}
