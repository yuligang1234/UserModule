
namespace Napoleon.UserModule.Model
{
    public class SystemCode
    {

        private string _id;
        /// <summary>
        ///  形式: guid(10)+yyMMddhhmmssffffff
        /// </summary>
        /// Author  :Napoelon
        /// Created :2015-01-07 10:19:42
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;
        /// <summary>
        ///  编码名称
        /// </summary>
        /// Author  :Napoelon
        /// Created :2015-01-07 10:19:42
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _parentId;
        /// <summary>
        ///  父节点
        /// </summary>
        /// Author  :Napoelon
        /// Created :2015-01-07 10:19:42
        public string ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        private decimal _sort;
        /// <summary>
        ///  排序
        /// </summary>
        /// Author  :Napoelon
        /// Created :2015-01-07 10:19:42
        public decimal Sort
        {
            get { return _sort; }
            set { _sort = value; }
        }





    }
}
