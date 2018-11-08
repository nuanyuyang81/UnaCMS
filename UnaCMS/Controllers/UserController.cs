using Newtonsoft.Json.Linq;
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
        public string Register(dynamic obj)
        {
            string usn = Convert.ToString(obj.usn);
            string pwd = Convert.ToString(obj.pwd);
            string email = Convert.ToString(obj.email);
            string regip = Convert.ToString(obj.regip);
            return DbOp.RegUser(usn, pwd, email, regip);
        }
        [AllowAnonymous]
        [HttpPost,Route("login")]
        public string Login(dynamic obj)
        {
            string userinfo = Convert.ToString(obj.usr);
            string pwd = Convert.ToString(obj.pwd);

            return DbOp.Login(userinfo, pwd);
        }
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="iduser"></param>
        /// <returns></returns>
        [AuthToken]
        [HttpGet,Route("delusr")]
        public bool DelUser(Guid iduser)
        {
            return DbOp.DeleteUser(iduser);
        }

        /// <summary>
        /// 用户列表,最大单页显示100条
        /// </summary>
        /// <param name="st"></param>
        /// <param name="pz"></param>
        /// <returns></returns>
        [AuthToken]
        [HttpGet,Route("listuser")]
        public JArray ListUsre(int st,int pz)
        {
            if (pz > 100)
            {
                pz = 100;
            }
            return DbOp.GetUserByAddTime(st, pz);
        }
    }
}
