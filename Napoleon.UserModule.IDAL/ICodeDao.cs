
using System.Data;

namespace Napoleon.UserModule.IDAL
{
    public interface ICodeDao
    {

        /// <summary>
        ///  根据父节点获取下拉框内容
        /// </summary>
        /// <param name="parentId">parentId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-19 09:29:19
        DataTable GetTableByParentId(string parentId);

    }
}
