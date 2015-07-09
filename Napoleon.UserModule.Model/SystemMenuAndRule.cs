
namespace Napoleon.UserModule.Model
{
    public class SystemMenuAndRule
    {

        private string _projectId;
        /// <summary>
        ///  用来区分菜单属于哪个系统
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-23 15:10:52
        public string ProjectId
        {
            get { return _projectId; }
            set { _projectId = value; }
        }

        private string _ruleId;
        /// <summary>
        ///  角色ID
        /// </summary>
        /// Author  :Napoelon
        /// Company :绍兴标点电子技术有限公司
        /// Created :2015-01-07 10:21:23
        public string RuleId
        {
            get { return _ruleId; }
            set { _ruleId = value; }
        }

        private string _menuId;
        /// <summary>
        ///  菜单ID
        /// </summary>
        /// Author  :Napoelon
        /// Company :绍兴标点电子技术有限公司
        /// Created :2015-01-07 10:21:23
        public string MenuId
        {
            get { return _menuId; }
            set { _menuId = value; }
        }

        private string _operationId;
        /// <summary>
        ///  操作ID
        /// </summary>
        /// Author  :Napoelon
        /// Company :绍兴标点电子技术有限公司
        /// Created :2015-01-07 10:21:23
        public string OperationId
        {
            get { return _operationId; }
            set { _operationId = value; }
        }





    }
}
