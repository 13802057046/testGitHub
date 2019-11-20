using System;
using System.Data;
using System.Data.SqlClient;
using NLog;

namespace KssApi.Common
{
    /// <summary>
    /// 操作数据库公共类
    /// </summary>
    public class SqlDataHelp
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private readonly string _sqlConnectionString = GetConnString();
        private readonly SqlConnection _sqlConnection = null;
        private Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 构造函数
        /// </summary>
        public SqlDataHelp()
        {
            _sqlConnection = new SqlConnection(_sqlConnectionString);
        }

        /// <summary>
        /// 从Web.config文件读取数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetConnString()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString;
            return connStr;
        }
        /// <summary>
        /// 判断数据是否存在
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool IsExist(string str)
        {
            DataTable dt = GetDataTable(str);
            if (dt == null || dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 执行SQL语句,成功全体提交，失败全体回滚
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public bool ExecuteSqlTran(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, _sqlConnection);
            if (_sqlConnection.State == ConnectionState.Closed)
            {
                _sqlConnection.Open();
            }
            SqlTransaction tx = _sqlConnection.BeginTransaction();
            cmd.Transaction = tx;
            try
            {
                int result = cmd.ExecuteNonQuery();
                tx.Commit();
                return true;
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                tx.Rollback();
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        /// <summary>
        /// 执行SQL语句，并返回数据库受影响的行数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, _sqlConnection);
            if (_sqlConnection.State == ConnectionState.Closed)
            {
                _sqlConnection.Open();
            }
            int result = cmd.ExecuteNonQuery();
            _sqlConnection.Close();
            return result;
        }

        /// <summary>
        /// 获取一个DataSet  此方法已被修改 sqlConnection默认打开 之后必须使用关闭方法
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql)
        {
            DataSet ds = new DataSet();
            try
            {
                _sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, _sqlConnection);
                adapter.SelectCommand.CommandTimeout = 0;
                adapter.Fill(ds);
                if (ds == null || ds.Tables[0] == null)
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                logger.Error("数据库错误。SQL文：" + sql);
                return null;
            }
            finally
            {
                _sqlConnection.Close();
            }

            return ds.Tables[0];
        }

        /// <summary>
        /// 获取一个DataSet  此方法已被修改 sqlConnection默认打开 之后必须使用关闭方法
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql)
        {
            DataSet ds = new DataSet();
            try
            {
                _sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, _sqlConnection);
                adapter.SelectCommand.CommandTimeout = 0;
                adapter.Fill(ds);
                if (ds == null || ds.Tables[0] == null)
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                logger.Error("数据库错误。SQL文：" + sql);
                return null;
            }
            finally
            {
                _sqlConnection.Close();
            }

            return ds;
        }

    }
}


