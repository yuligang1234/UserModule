
using System.Collections.Generic;
using System.Data;
using Napoleon.UserModule.IBLL;
using Napoleon.UserModule.IDAL;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.BLL
{
    public class MenuAndRuleService : IMenuAndRuleService
    {

        private IMenuAndRuleDao _menuAndRuleDao;

        public MenuAndRuleService(IMenuAndRuleDao menuAndRuleDao)
        {
            _menuAndRuleDao = menuAndRuleDao;
        }

        /// <summary>
        ///  获取菜单
        /// </summary>
        /// <param name="ruleId">角色ID</param>
        /// <param name="projectId">系统ID</param>
        /// Author  : Napoleon
        /// Created : 2015-01-10 15:15:22
        public DataTable GetMenuTable(string ruleId, string projectId)
        {
            return _menuAndRuleDao.GetMenuTable(ruleId, projectId);
        }

        /// <summary>
        ///  获取
        /// </summary>
        /// <param name="ruleId">ruleId</param>
        /// <param name="menuId">menuId</param>
        /// <param name="projectId">projectId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-12 20:03:14
        public List<SystemMenu> GetOperation(string ruleId, string menuId, string projectId)
        {
            return _menuAndRuleDao.GetOperation(ruleId, menuId, projectId);
        }

        /// <summary>
        ///  获取拥有的权限
        /// </summary>
        /// <param name="ruleId">权限ID</param>
        /// <param name="projectId">系统ID</param>
        /// Author  : Napoleon
        /// Created : 2015-01-10 15:15:22
        public DataTable GetOperatorTable(string projectId, string ruleId)
        {
            return _menuAndRuleDao.GetOperatorTable(projectId, ruleId);
        }

        /// <summary>
        ///  需要先删除原先的权限，再新增权限
        /// </summary>
        /// <param name="ruleId">The rule identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="list">The list.</param>
        /// Author  : Napoleon
        /// Created : 2015-02-04 20:22:08
        public int UpdateRuleMenu(string ruleId, string projectId, List<SystemMenuAndRule> list)
        {
            return _menuAndRuleDao.UpdateRuleMenu(ruleId, projectId, list);
        }

    }
}
