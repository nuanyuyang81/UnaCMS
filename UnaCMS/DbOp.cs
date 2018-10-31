using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace UnaCMS
{
    public class DbOp
    {
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
        #endregion
    }
}