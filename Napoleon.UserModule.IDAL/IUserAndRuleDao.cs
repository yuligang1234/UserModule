
using System.Data;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.IDAL
{
    public interface IUserAndRuleDao
    {

        /// <summary>
        ///  获取权限ID
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="projectId">系统代码</param>
        /// Author  : Napoleon
        /// Created : 2015-01-20 13:24:49
        SystemUserAndRule GetRule(string userId, string projectId);

        /// <summary>
        ///  获取用户所在角色的列表
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="startCount">The start count.</param>
        /// <param name="endCount">The end count.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-21 15:38:44
        DataTable GetRuleTable(string userId, int startCount, int endCount);

        /// <summary>
        ///  用户所在角色的总数
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-21 15:41:22
        int GetRuleCount(string userId);

        /// <summary>
        ///  根据ID删除用户对应的权限数据
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-21 15:56:01
        int DeleteRuleById(string id);

        /// <summary>
        ///  新增用户权限
        /// </summary>
        /// <param name="rules">rules</param>
        /// Author  : Napoleon
        /// Created : 2015-01-23 10:04:00
        int InsertUserRule(SystemUserAndRule rules);

    }
}
