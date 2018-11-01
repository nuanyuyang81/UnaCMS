using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UnaUtility;

namespace UnaCMS
{
    public class DbOp
    {
        #region 通用函数
        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <returns></returns>
        private static SqlConnection DbConn()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["defaultdb"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionstring);
            try
            {
                conn.Open();
            }
            catch(SqlException ex)
            {
                throw ex;
            }
            return conn;
        }
        /// <summary>
        /// 执行带参NonQuery数据库命令，返回是否执行成功
        /// </summary>
        /// <param name="cmdline"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static bool ExecuteNonQueryWithParam(string cmdline,SqlParameter[] parameters)
        {
            using(SqlConnection conn = DbConn())
            {
                SqlCommand cmd = new SqlCommand(cmdline, conn);
                cmd.Parameters.AddRange(parameters);
                try
                {
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch(SqlException ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 执行带参Query数据库命令，返回JArry
        /// </summary>
        /// <param name="cmdline"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static JArray ExecuteQueryWithParam(string cmdline, SqlParameter[] parameters)
        {
            using (SqlConnection conn = DbConn())
            {
                SqlCommand cmd = new SqlCommand(cmdline, conn);
                cmd.Parameters.AddRange(parameters);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    return JArray.FromObject(ds.Tables[0], JsonSerializer.CreateDefault(new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
                }
            }
            return null;
        }
        /// <summary>
        /// 执行不带参Query数据库命令，返回JArry
        /// </summary>
        /// <param name="cmdline"></param>
        /// <returns></returns>
        private static JArray ExecuteQueryWithNoParam(string cmdline)
        {
            using (SqlConnection conn = DbConn())
            {
                SqlCommand cmd = new SqlCommand(cmdline, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    return JArray.FromObject(ds.Tables[0], JsonSerializer.CreateDefault(new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
                }
            }
            return null;
        }

        /// <summary>
        /// 执行带参Query数据库命令，返回JOBject
        /// </summary>
        /// <param name="cmdline"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static JObject ExecuteQueryObject(string cmdline,SqlParameter[] parameters)
        {
            using(SqlConnection conn = DbConn())
            {
                SqlCommand cmd = new SqlCommand(cmdline,conn);
                cmd.Parameters.AddRange(parameters);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return JObject.FromObject(reader, JsonSerializer.CreateDefault(new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
                }
            }
            return null;
        }
        /// <summary>
        /// 执行带参查询Count命令，返回行数
        /// </summary>
        /// <param name="cmdline"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>

        private static int ExecuteGetCountWithParam(string cmdline,SqlParameter[] parameters)
        {
            using(SqlConnection conn = DbConn())
            {
                SqlCommand cmd = new SqlCommand(cmdline, conn);
                cmd.Parameters.AddRange(parameters);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return Convert.ToInt32(reader[0]);
                }
            }
            return -1;
        }
        /// <summary>
        /// 执行不带参查询Count命令，返回行数
        /// </summary>
        /// <param name="cmdline"></param>
        /// <returns></returns>
        private static int ExecuteGetCount(string cmdline)
        {
            using (SqlConnection conn = DbConn())
            {
                SqlCommand cmd = new SqlCommand(cmdline, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return Convert.ToInt32(reader[0]);
                }
            }
            return -1;
        }
        #endregion

        #region 用户组
        /// <summary>
        /// 添加用户组
        /// </summary>
        /// <param name="title"></param>
        /// <param name="grade"></param>
        /// <param name="exp"></param>
        /// <param name="isdefault"></param>
        /// <returns></returns>
        public static bool AddUserGroup(string title,int grade,int exp,bool isdefault)
        {
            string cmdline = "insert into [una].[usergroup] values(@title,@grade,@exp,@isdefault)";
            using(SqlConnection conn = DbConn())
            {
                SqlCommand cmd = new SqlCommand(cmdline, conn);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@grade", grade);
                cmd.Parameters.AddWithValue("@exp", exp);
                cmd.Parameters.AddWithValue("@isdefault", isdefault);
                try
                {
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch(SqlException ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 删除用户组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteUserGroup(int id)
        {
            string cmdline = "delete from [una].[usergroup] where id=@id";
            using(SqlConnection conn = DbConn())
            {
                SqlCommand cmd = new SqlCommand(cmdline, conn);
                cmd.Parameters.AddWithValue("@id", id);
                try
                {
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch(SqlException ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 获取默认用户组
        /// </summary>
        /// <returns></returns>
        public static int GetDefaultUserGroup()
        {
            string cmdline = "select id from [una].[usergroup] where isdefault=@isdefault";
            SqlParameter[] parameters =
            {
                new SqlParameter("@isdefault",true)
            };
            JObject result = ExecuteQueryObject(cmdline, parameters);
            if (result != null)
            {
                return Convert.ToInt32(result["id"]);
            }
            return -1;
        }
        #endregion

        #region 用户
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="idgroup"></param>
        /// <param name="username"></param>
        /// <param name="salt"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="regip"></param>
        /// <returns></returns>
        public static bool RegUser(string username,string password,string email,string regip)
        {
            Guid salt = Guid.NewGuid();
            int defaultusergroup = GetDefaultUserGroup();
            byte[] passwordAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
            byte[] hashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAndSaltBytes);
            string hashString = Convert.ToBase64String(hashBytes);
            if (defaultusergroup > 0)
            {
                string cmdline = "inesrt into [una].[user](id,idgroup,username,salt,password,email,addtime,regip) values(@id,@idgroup,@username,@salt,@password,@email,@addtime,@regip)";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@id",Guid.NewGuid()),
                    new SqlParameter("@idgroup",defaultusergroup),
                    new SqlParameter("@username",username),
                    new SqlParameter("@salt",salt),
                    new SqlParameter("@password",hashString),
                    new SqlParameter("@email",email),
                    new SqlParameter("@addtime",DateTime.Now),
                    new SqlParameter("@regip",regip)
                };
                return ExecuteNonQueryWithParam(cmdline, parameters);
            }
            return false;
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="email"></param>
        /// <param name="mobile"></param>
        /// <param name="avatar"></param>
        /// <param name="nickname"></param>
        /// <param name="sex"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool UpdateUserInfo(string email,string mobile,string avatar,string nickname,int sex,Guid id)
        {
            string cmdline = "update [una].[user] set email=@email,mobile=@mobile,avatar=@avatar,nickname=@nickname,sex=@sex where id=@id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@email",email),
                new SqlParameter("@mobile",mobile),
                new SqlParameter("avatar",avatar),
                new SqlParameter("@nickname",nickname),
                new SqlParameter("@sex",sex),
                new SqlParameter("@id",id)
            };
            return ExecuteNonQueryWithParam(cmdline, parameters);
        }
        /// <summary>
        /// 获取用户Salt
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetSalt(string user)
        {
            string cmdline = "select salt from [una].[user] where username=@user or email=@user or mobile=@user";
            SqlParameter[] parameters =
            {
                new SqlParameter("@user",user)
            };
            JObject saltreader = ExecuteQueryObject(cmdline, parameters);
            if (saltreader != null)
            {
                return saltreader["salt"].ToString();
            }
            return string.Empty;
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Login(string user,string password)
        {
            string salt = GetSalt(user);
            if (!String.IsNullOrEmpty(salt))
            {
                byte[] passwordAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(password + salt);

                byte[] hashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAndSaltBytes);

                string hashpassword = Convert.ToBase64String(hashBytes);
                string cmdline = "select * from [una].[user] where salt=@salt and password=@password";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@salt",salt),
                    new SqlParameter("@password",hashpassword)
                };
                JArray result = ExecuteQueryWithParam(cmdline, parameters);
                if (result != null && result.Count > 0)
                {
                    return string.Format("用户:{0} 登录成功，Id:{1}",result["username"].ToString(),result["id"].ToString());
                }
                else
                {
                    return "密码不正确";
                }
            }
            else
            {
                return "用户不存在";
            }
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static JObject GetUserInfo(Guid id)
        {
            string cmdline = "select * from [una].[user] where id=@id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@id",id)
            };
            return ExecuteQueryObject(cmdline, parameters);
        }
        /// <summary>
        /// 按注册时间排序获取分页用户列表
        /// </summary>
        /// <param name="startindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public static JArray GetUserByAddTime(int startindex,int pagesize)
        {
            string cmdline = @"select top @pagesize * from 
                                (select row_number()over(order by addtime)rownumber,* from [una].[user])a
                                where rownumber>@prev";

            SqlParameter[] parameters =
            {
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@prev",(startindex-1)*pagesize)
            };
            return ExecuteQueryWithParam(cmdline, parameters);
        }
        /// <summary>
        /// 获取用户总数
        /// </summary>
        /// <returns></returns>
        public static int GetTotalUserCount()
        {
            string cmdline = "select count(*) from [una].[user]";
            return ExecuteGetCount(cmdline);
        }
        #endregion

        #region 文章
        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="idchannel"></param>
        /// <param name="title"></param>
        /// <param name="imgurl"></param>
        /// <param name="summary"></param>
        /// <param name="content"></param>
        /// <param name="iduser"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool AddArticle(int idchannel,string title,string imgurl,string summary,string content,Guid iduser,string username)
        {
            string cmdline = "insert into [una].[article](idchannel,title,imgurl,summary,[content],iduser,username,addtime,updatetime) values(@idchannel,@title,@imgurl,@summary,@content,@iduser,@username,@addtime,@updatetime)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@idchannel",idchannel),
                new SqlParameter("@title",title),
                new SqlParameter("@imgurl",imgurl),
                new SqlParameter("@summary",summary),
                new SqlParameter("@content",content),
                new SqlParameter("@iduser",iduser),
                new SqlParameter("@username",username),
                new SqlParameter("@addtime",DateTime.Now),
                new SqlParameter("@updatetime",DateTime.Now)
            };
            return ExecuteNonQueryWithParam(cmdline, parameters);
        }
        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="idchannel"></param>
        /// <param name="title"></param>
        /// <param name="imgurl"></param>
        /// <param name="summary"></param>
        /// <param name="content"></param>
        /// <param name="tags"></param>
        /// <param name="status"></param>
        /// <param name="recommend"></param>
        /// <param name="iduser"></param>
        /// <param name="username"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool UpdateArticle(int idchannel,string title,string imgurl,string summary,string content,string tags,ArticleStatus status,RecommendStatus recommend,Guid iduser,string username,Guid id)
        {
            string cmdline = "update [una].[article] set idchannel=@idchannel,title=@title,imgurl=@imgurl,summary=@summary,[content]=@";
            SqlParameter[] parameters =
            {
                new SqlParameter("@idchannel",idchannel),
                new SqlParameter("@title",title),
                new SqlParameter("@imgurl",imgurl),
                new SqlParameter("@summary",summary),
                new SqlParameter("@content",content),
                new SqlParameter("@tags",tags),
                new SqlParameter("@status",status),
                new SqlParameter("@recommend",recommend),
                new SqlParameter("@iduser",iduser),
                new SqlParameter("@username",username),
                new SqlParameter("@updatetime",DateTime.Now)
            };
            return ExecuteNonQueryWithParam(cmdline, parameters);
        }
        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteArticle(Guid id)
        {
            string cmdline = "delete from [una].[article] where id=@id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@id",id)
            };
            return ExecuteNonQueryWithParam(cmdline, parameters);
        }
        /// <summary>
        /// 按添加时间排序获取分页文章列表
        /// </summary>
        /// <param name="idchannel"></param>
        /// <param name="startindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public static JArray GetArticleForChannelOrderByAddTime(int idchannel,int startindex,int pagesize)
        {
            string cmdline = @"select top @pagesize * from 
                                (select row_number()over(order by addtime)rownumber,* from [una].[article])a
                                where rownumber>@prev";

            SqlParameter[] parameters =
            {
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@prev",(startindex-1)*pagesize)
            };
            return ExecuteQueryWithParam(cmdline, parameters);
        }
        /// <summary>
        /// 按更新时间获取分页文章列表
        /// </summary>
        /// <param name="idchannel"></param>
        /// <param name="startindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public static JArray GetArticleForChannelOrderByUpdateTime(int idchannel, int startindex, int pagesize)
        {
            string cmdline = @"select top @pagesize * from 
                                (select row_number()over(order by updatetime)rownumber,* from [una].[article])a
                                where rownumber>@prev";

            SqlParameter[] parameters =
            {
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@prev",(startindex-1)*pagesize)
            };
            return ExecuteQueryWithParam(cmdline, parameters);
        }
        /// <summary>
        /// 按Channel获取文章总数
        /// </summary>
        /// <param name="idchannel"></param>
        /// <returns></returns>
        public static int GetTotalArticleCountForChannel(int idchannel)
        {
            string cmdline = "select count(*) from [una].[article] where idchannel=@idchannel";
            SqlParameter[] parameters =
            {
                new SqlParameter("@idchannel",idchannel)
            };
            return ExecuteGetCountWithParam(cmdline, parameters);
        }
        /// <summary>
        /// 获取文章总数
        /// </summary>
        /// <returns></returns>
        public static int GetTotalArticleCount()
        {
            string cmdline = "select count(*) from [una].[article]";
            return ExecuteGetCount(cmdline);
        }
        #endregion

        #region 频道
        /// <summary>
        /// 添加频道，默认status true
        /// </summary>
        /// <param name="name"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static bool AddChannel(string name,string title)
        {
            string cmdline = "insert into [una].[channel] values(@status,@name,@title)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@status",true),
                new SqlParameter("@name",name),
                new SqlParameter("@title",title)
            };
            return ExecuteNonQueryWithParam(cmdline, parameters);
        }
        /// <summary>
        /// 更新频道
        /// </summary>
        /// <param name="status"></param>
        /// <param name="name"></param>
        /// <param name="title"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool UpdateChannel(bool status,string name,string title,int id)
        {
            string cmdline = "update [una].[channel] set status=@status,name=@name,title=@title where id=@id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@status",status),
                new SqlParameter("@name",name),
                new SqlParameter("@title",title),
                new SqlParameter("@id",id)
            };
            return ExecuteNonQueryWithParam(cmdline, parameters);
        }
        /// <summary>
        /// 删除频道
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteChannel(int id)
        {
            string cmdline = "delete from [una].[channel] where id=@id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@id",id)
            };
            return ExecuteNonQueryWithParam(cmdline, parameters);
        }
        /// <summary>
        /// 获取所有频道
        /// </summary>
        /// <returns></returns>
        public static JArray QueryChannel()
        {
            string cmdline = "select * from [una].[channel]";
            return ExecuteQueryWithNoParam(cmdline);
        }
        #endregion

        #region 导航
        /// <summary>
        /// 添加导航
        /// </summary>
        /// <param name="idparent"></param>
        /// <param name="idchannel"></param>
        /// <param name="navtype"></param>
        /// <param name="name"></param>
        /// <param name="iconurl"></param>
        /// <param name="linkurl"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public static bool AddNavigation(int idparent,int idchannel,string navtype,string name,string iconurl,string linkurl,string remark)
        {
            string cmdline = "insert into [una].[navigation] values(@idparent,@idchannel,@navtype,@name,@iconurl,@linkurl,@remark,@addtime)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@idparent",idparent),
                new SqlParameter("@idchannel",idchannel),
                new SqlParameter("@navtype",navtype),
                new SqlParameter("@name",name),
                new SqlParameter("@iconurl",iconurl),
                new SqlParameter("@linkurl",linkurl),
                new SqlParameter("@remark",remark),
                new SqlParameter("@addtime",DateTime.Now)
            };
            return ExecuteNonQueryWithParam(cmdline, parameters);
        }
        /// <summary>
        /// 更新导航信息
        /// </summary>
        /// <param name="idparent"></param>
        /// <param name="idchannel"></param>
        /// <param name="navtype"></param>
        /// <param name="name"></param>
        /// <param name="iconurl"></param>
        /// <param name="linkurl"></param>
        /// <param name="remark"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool UpdateNavigation(int idparent,int idchannel,string navtype,string name,string iconurl,string linkurl,string remark,int id)
        {
            string cmdline = "update [una].[navigation] set idparent=@idparent,idchannel=@idchannel,navtype=@navtype,name=@name,iconurl=@iconurl,linkurl=@linkurl,remark=@remark where id=@id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@idparent",idparent),
                new SqlParameter("@idchannel",idchannel),
                new SqlParameter("@navtype",navtype),
                new SqlParameter("@name",name),
                new SqlParameter("@iconurl",iconurl),
                new SqlParameter("@linkurl",linkurl),
                new SqlParameter("@remark",remark),
                new SqlParameter("@id",id)
            };
            return ExecuteNonQueryWithParam(cmdline, parameters);
        }
        /// <summary>
        /// 删除导航信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteNavigation(int id)
        {
            string cmdline = "delete from [una].[navigation] where id=@id or idparent=@id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@id",id)
            };
            return ExecuteNonQueryWithParam(cmdline, parameters);
        }
        /// <summary>
        /// 获取所有导航信息
        /// </summary>
        /// <returns></returns>
        public static JArray QueryNavigation()
        {
            string cmdline = "select * from [una].[navigation]";
            return ExecuteQueryWithNoParam(cmdline);
        }
        #endregion

        #region 文章标签
        /// <summary>
        /// 添加文章标签
        /// </summary>
        /// <param name="title"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        public static bool AddArticleTag(string title,int rank)
        {
            string cmdline="insert into [una].[articletag] values(@title,@rank,@addtime)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@title",title),
                new SqlParameter("@rank",rank),
                new SqlParameter("@addtime",DateTime.Now)
            };
            return ExecuteNonQueryWithParam(cmdline, parameters);
        }
        /// <summary>
        /// 更新文章标签
        /// </summary>
        /// <param name="title"></param>
        /// <param name="rank"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool UpdateArticleTag(string title,int rank,int id)
        {
            string cmdline = "update [una].[articletag] set title=@title,rank=@rank where id=@id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@title",title),
                new SqlParameter("@rank",rank),
                new SqlParameter("@id",id)
            };
            return ExecuteNonQueryWithParam(cmdline, parameters);
        }
        /// <summary>
        /// 删除文章标签
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteArticleTag(int id)
        {
            string cmdline = "delete from [una].[articletag] where id=@id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@id",id)
            };
            return ExecuteNonQueryWithParam(cmdline, parameters);
        }
        /// <summary>
        /// 获取所有文章标签
        /// </summary>
        /// <returns></returns>
        public static JArray QueryArticleTag()
        {
            string cmdline = "select * from [una].[articletag]";
            return ExecuteQueryWithNoParam(cmdline);
        }
        #endregion

    }
}