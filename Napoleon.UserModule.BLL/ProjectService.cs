
using System.Data;
using Napoleon.UserModule.IBLL;
using Napoleon.UserModule.IDAL;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.BLL
{
    public class ProjectService : IProjectService
    {

        private IProjectDao _projectDao;

        public ProjectService(IProjectDao projectDao)
        {
            _projectDao = projectDao;
        }

        /// <summary>
        ///  获取系统下拉框
        /// </summary>
        /// <returns>DataTable.</returns>
        /// Author  : Napoleon
        /// Created : 2015-01-27 11:23:40
        public DataTable GetProjecTable()
        {
            return _projectDao.GetProjecTable();
        }

        /// <summary>
        ///  获取系统列表
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-23 16:20:56
        public DataTable GetProjectTable(string projectId, string projectName, int rows, int page)
        {
            return _projectDao.GetProjectTable(projectId, projectName, rows * (page - 1), rows * page);
        }

        /// <summary>
        ///  系统总数
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-23 16:31:28
        public int GetProjectCount()
        {
            return _projectDao.GetProjectCount();
        }

        /// <summary>
        ///  新增系统
        /// </summary>
        /// <param name="project">The project.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 09:42:13
        public int InsertProject(SystemProject project)
        {
            return _projectDao.InsertProject(project);
        }

        /// <summary>
        ///  根据ID查询
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 10:06:30
        public SystemProject GetProjectById(string projectId)
        {
            return _projectDao.GetProjectById(projectId);
        }

        /// <summary>
        ///  新增系统
        /// </summary>
        /// <param name="project">The project.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 09:42:13
        public int UpdateProject(SystemProject project)
        {
            return _projectDao.UpdateProject(project);
        }

        /// <summary>
        ///  根据ID删除
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 10:43:31
        public int DeleteProject(string projectId)
        {
            return _projectDao.DeleteProject(projectId);
        }

    }
}
