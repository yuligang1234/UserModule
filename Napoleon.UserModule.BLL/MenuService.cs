
using System.Data;
using Napoleon.UserModule.IBLL;
using Napoleon.UserModule.IDAL;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.BLL
{
    public class MenuService : IMenuService
    {

        private IMenuDao _menuDao;

        public MenuService(IMenuDao menuDao)
        {
            _menuDao = menuDao;
        }

        /// <summary>
        ///  根据projectId查询列表
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 15:11:11
        public DataTable GetMenuTable(string projectId)
        {
            return _menuDao.GetMenuTable(projectId);
        }

        /// <summary>
        ///  菜单父节点
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// <param name="parentId">parentId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 16:35:16
        public DataTable GetTreeParentId(string projectId, string parentId = "")
        {
            return _menuDao.GetTreeParentId(projectId, parentId);
        }

        /// <summary>
        ///  新增菜单
        /// </summary>
        /// <param name="menu">The menu.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-26 14:18:32
        public int InsertMenu(SystemMenu menu)
        {
            return _menuDao.InsertMenu(menu);
        }

        /// <summary>
        ///  根据Id查询
        /// </summary>
        /// <param name="id">id</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 15:11:11
        public SystemMenu GetMenu(string id)
        {
            return _menuDao.GetMenu(id);
        }

        /// <summary>
        ///  更新菜单
        /// </summary>
        /// <param name="menu">The menu.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-26 16:06:40
        public int UpdateMenu(SystemMenu menu)
        {
            return _menuDao.UpdateMenu(menu);
        }

        /// <summary>
        ///  根据ID删除
        /// </summary>
        /// <param name="id">id</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 10:43:31
        public int DeleteMenu(string id)
        {
            return _menuDao.DeleteMenu(id);
        }


    }
}
