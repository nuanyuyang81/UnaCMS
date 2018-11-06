using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace UnaCMS.Controllers
{
    [AuthToken]
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        public string OptionsUserr()
        {
            return null;
        }
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="usn">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="email">注册邮箱</param>
        /// <param name="regip">注册IP</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost,Route("register")]
        public bool Register(string usn,string pwd,string email,string regip)
        {
            return DbOp.RegUser(usn, pwd, email, regip);
        }
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="iduser"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet,Route("delusr")]
        public bool DelUser(Guid iduser)
        {
            return DbOp.DeleteUser(iduser);
        }
    }
}
