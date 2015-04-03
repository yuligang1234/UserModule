
using System.Collections.Generic;
using System.Data;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.IBLL
{
    public interface IMenuAndRuleService
    {

        /// <summary>
        ///  获取菜单
        /// </summary>
        /// <param name="ruleId">角色ID</param>
        /// <param name="projectId">系统ID</param>
        /// Author  : Napoleon
        /// Created : 2015-01-10 15:15:22
        DataTable GetMenuTable(string ruleId, string projectId);

        /// <summary>
        ///  获取
        /// </summary>
        /// <param name="ruleId">departmentId</param>
        /// <param name="menuId">menuId</param>
        /// <param name="projectId">projectId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-12 20:03:14
        List<SystemMenu> GetOperation(string ruleId, string menuId, string projectId);

        /// <summary>
        ///  获取拥有的权限
        /// </summary>
        /// <param name="ruleId">菜单ID</param>
        /// <param name="projectId">系统ID</param>
        /// Author  : Napoleon
        /// Created : 2015-01-10 15:15:22
        DataTable GetOperatorTable(string projectId, string ruleId);

        /// <summary>
        ///  需要先删除原先的权限，再新增权限
        /// </summary>
        /// <param name="ruleId">The rule identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="list">The list.</param>
        /// Author  : Napoleon
        /// Created : 2015-02-04 20:22:08
        int UpdateRuleMenu(string ruleId, string projectId, List<SystemMenuAndRule> list);

    }
}
