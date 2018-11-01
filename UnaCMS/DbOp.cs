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
        private static bool ExuecuteNonQueryWithParam(string cmdline,SqlParameter[] parameters)
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
        public static void AddChannel()
        #endregion
    }
}