
using System.Data;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.IDAL
{
    public interface IMenuDao
    {

        /// <summary>
        ///  根据projectId查询列表
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 15:11:11
        DataTable GetMenuTable(string projectId);

        /// <summary>
        ///  菜单父节点
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// <param name="parentId">parentId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 16:35:16
        DataTable GetTreeParentId(string projectId, string parentId = "");

        /// <summary>
        ///  新增菜单
        /// </summary>
        /// <param name="menu">The menu.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-26 14:18:32
        int InsertMenu(SystemMenu menu);

        /// <summary>
        ///  根据Id查询
        /// </summary>
        /// <param name="id">id</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 15:11:11
        SystemMenu GetMenu(string id);

        /// <summary>
        ///  更新菜单
        /// </summary>
        /// <param name="menu">The menu.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-26 16:06:40
        int UpdateMenu(SystemMenu menu);

        /// <summary>
        ///  根据ID删除
        /// </summary>
        /// <param name="id">id</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 10:43:31
        int DeleteMenu(string id);

    }
}
