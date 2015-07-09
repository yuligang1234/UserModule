
using Napoleon.UserModule.Model;
using Newtonsoft.Json;

namespace Napoleon.UserModule.Common
{
    public class PublicFunc
    {
        /// <summary>
        ///  返回结果
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="msg">信息</param>
        /// Author  : Napoleon
        /// Created : 2015-07-09 10:19:12
        public static string ModelToJson(string status,string msg)
        {
            Result result=new Result();
            result.Status = status;
            result.Msg = msg;
            string json = JsonConvert.SerializeObject(result);
            return json;
        }

    }
}
