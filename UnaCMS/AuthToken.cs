using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Security;

namespace UnaCMS
{
    public class AuthTokenAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //从http请求的头里面获取身份验证信息，验证是否是请求发起方的ticket
            var authorization = actionContext.Request.Headers.Authorization;
            if ((authorization != null) && (authorization.Parameter != null))
            {
                //解密用户Ticket，校验用户名密码
                var encryptTicket = authorization.Parameter;
                if (ValidateTicket(encryptTicket).Contains("登录成功"))
                {
                    base.IsAuthorized(actionContext);
                }
                else
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }
            //如果取不到身份验证信息，并且不允许匿名访问，则返回未验证401
            else
            {
                var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
                bool isAnonymous = attributes.Any(a => a is AllowAnonymousAttribute);
                if (isAnonymous) base.OnAuthorization(actionContext);
                else HandleUnauthorizedRequest(actionContext);
            }
        }
        /// <summary>
        /// 验证用户名密码是否正确
        /// </summary>
        /// <param name="encryptTicket"></param>
        /// <returns></returns>
        private string ValidateTicket(string encryptTicket)
        {
            var strTicket = FormsAuthentication.Decrypt(encryptTicket).UserData;
            var index = strTicket.IndexOf("&");
            string username = strTicket.Substring(0, index);
            string password = strTicket.Substring(index + 1);
            return DbOp.Login(username, password);          
        }
    }
}