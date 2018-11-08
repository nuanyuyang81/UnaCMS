using System.IO;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Security;
namespace UnaCMS
{
    public class AuthTokenAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            bool pass = false;
            var authorization = actionContext.Request.Headers.Authorization;
            if ((authorization != null) && (authorization.Parameter != null))
            {
                string encryTicket = authorization.Parameter;
                string validateresult = ValidateTicket(encryTicket);
                if (validateresult.Contains("登陆成功"))
                {
                    pass = true;
                }
            }
            return pass;
        }
        /// <summary>
        /// 验证用户名密码是否正确
        /// </summary>
        /// <param name="encryptTicket"></param>
        /// <returns></returns>
        private string ValidateTicket(string encryptTicket)
        {
            string username = encryptTicket.Substring(0,encryptTicket.IndexOf("&"));
            string password = encryptTicket.Substring(encryptTicket.IndexOf("&")+1);
            string result= DbOp.Login(username, password);
            return result;
        }
        private void WriteToLog(string content)
        {
            using(FileStream fs=new FileStream(@"D:\Log\log.txt", FileMode.Append, FileAccess.Write))
            {
                StreamWriter streamWriter = new StreamWriter(fs);
                streamWriter.WriteLine(content);
                streamWriter.Flush();
            }
        }
    }
}