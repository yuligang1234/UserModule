
using System;

namespace Napoleon.UserModule.Model
{
    [Serializable]
    public class SystemUser
    {

        private string _id;
        /// <summary>
        ///  guid(10)+yyMMddhhmmssffffff
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-01-07 10:22:09
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _projectId;
        /// <summary>
        ///  项目ID
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-01-07 10:22:09
        public string ProjectId
        {
            get { return _projectId; }
            set { _projectId = value; }
        }

        private string _userName;
        /// <summary>
        ///  用户账号
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-01-07 10:22:09
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private string _passWords;
        /// <summary>
        ///  用户密码
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-01-07 10:22:09
        public string PassWords
        {
            get { return _passWords; }
            set { _passWords = value; }
        }

        private string _realName;
        /// <summary>
        ///  用户姓名
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-01-07 10:22:09
        public string RealName
        {
            get { return _realName; }
            set { _realName = value; }
        }

        private string _mobilePhone;
        /// <summary>
        ///  联系电话
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-01-07 10:22:09
        public string MobilePhone
        {
            get { return _mobilePhone; }
            set { _mobilePhone = value; }
        }

        private string _isUse;
        /// <summary>
        ///  是否启用
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-01-07 10:22:09
        public string IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        private string _userAddress;
        /// <summary>
        ///  地址
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-01-07 10:22:09
        public string UserAddress
        {
            get { return _userAddress; }
            set { _userAddress = value; }
        }

        private decimal _sort;
        /// <summary>
        ///  排序
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-01-07 10:22:09
        public decimal Sort
        {
            get { return _sort; }
            set { _sort = value; }
        }

        private string _remark;
        /// <summary>
        ///  备注
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-01-07 10:22:09
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        private string _operator;
        /// <summary>
        ///  操作者
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-01-07 10:22:09
        public string Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }





    }
}
