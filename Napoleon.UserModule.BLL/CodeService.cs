using System.Data;
using Napoleon.UserModule.IBLL;
using Napoleon.UserModule.IDAL;

namespace Napoleon.UserModule.BLL
{
    public class CodeService : ICodeService
    {

        private ICodeDao _codeDao;

        public CodeService(ICodeDao codeDao)
        {
            _codeDao = codeDao;
        }

        /// <summary>
        ///  根据父节点获取下拉框内容
        /// </summary>
        /// <param name="parentId">parentId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-19 09:29:19
        public DataTable GetTableByParentId(string parentId)
        {
            return _codeDao.GetTableByParentId(parentId);
        }

    }
}
