
using System.Data;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.IDAL
{
    public interface IProjectDao
    {

        /// <summary>
        ///  获取系统下拉框
        /// </summary>
        /// <returns>DataTable.</returns>
        /// Author  : Napoleon
        /// Created : 2015-01-27 11:23:40
        DataTable GetProjecTable();

        /// <summary>
        ///  获取系统列表
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-23 16:20:56
        DataTable GetProjectTable(string projectId, string projectName, int startCount, int endCount);

        /// <summary>
        ///  系统总数
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-23 16:31:28
        int GetProjectCount();

        /// <summary>
        ///  新增系统
        /// </summary>
        /// <param name="project">The project.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 09:42:13
        int InsertProject(SystemProject project);

        /// <summary>
        ///  根据ID查询
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 10:06:30
        SystemProject GetProjectById(string projectId);

        /// <summary>
        ///  新增系统
        /// </summary>
        /// <param name="project">The project.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 09:42:13
        int UpdateProject(SystemProject project);

        /// <summary>
        ///  根据ID删除
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 10:43:31
        int DeleteProject(string projectId);

    }
}
