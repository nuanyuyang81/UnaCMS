using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

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
        public static bool RegUser(int idgroup,string username,string salt,string password,string email,string regip)
        {
            string cmdline = "inesrt into [una].[user](idgroup,username,salt,password,email,regip) values(@idgroup,@username,@salt,@password,@email,@regip)";
            using(SqlConnection conn = DbConn())
            {
                SqlCommand cmd = new SqlCommand(cmdline, conn);
                cmd.Parameters.AddWithValue("@idgroup", idgroup);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@salt", salt);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@regip", regip);
                try
                {
                    return cmd.ExecuteNonQuery()>0;
                }
                catch(SqlException ex)
                {
                    throw ex;
                }
            }
        }

        #endregion

        #region 文章
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