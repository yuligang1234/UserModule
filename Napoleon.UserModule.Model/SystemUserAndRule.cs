
namespace Napoleon.UserModule.Model
{
    public class SystemUserAndRule
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

        private string _id;
        /// <summary>
        ///  Id
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-01-20 09:52:18
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _userId;
        /// <summary>
        ///  UserId
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-01-20 09:52:18
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private string _ruleId;
        /// <summary>
        ///  RuletId
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-01-20 09:52:18
        public string RuleId
        {
            get { return _ruleId; }
            set { _ruleId = value; }
        }

        private string _ruleParentId;
        /// <summary>
        ///  RuletId
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-01-20 09:52:18
        public string RuleParentId
        {
            get { return _ruleParentId; }
            set { _ruleParentId = value; }
        }

    }
}
