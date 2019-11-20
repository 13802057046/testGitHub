using System;
using System.Data;
using System.Data.SqlClient;

namespace BaseInfo
{
    /// <summary>
    /// 操作数据库公共类
    /// </summary>
    public class SqlDataHelp
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private readonly string SqlConnectionString = GetConnString();

        private SqlConnection sqlConnection = null;

        public SqlDataHelp()
        {
            sqlConnection = new SqlConnection(SqlConnectionString);
        }

        /// <summary>
        /// 从ini文件读取数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetConnString()
        {
            string connStr = "";
            string server = "";
            string uid = "";
            string pwd = "";
            string database = "";
            // 读取ini文件 
            // 服务器
            server = G_INI.ReadValue("db", "server");
            // 用户名
            uid = G_INI.ReadValue("db", "uid");
            // 密码
            pwd = G_INI.ReadValue("db", "pwd");
            // 数据库
            database = G_INI.ReadValue("db", "database");
            connStr = "server=" + server + ";uid=" + uid + ";pwd=" + pwd + ";database=" + database;
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
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            SqlTransaction tx = sqlConnection.BeginTransaction();
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
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// 执行SQL语句,成功全体提交，失败全体回滚
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public bool ExecuteMuteSqlTran(string[] sql)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            SqlTransaction tx = sqlConnection.BeginTransaction();

            try
            {
                for (int i = 0; i < sql.Length; i++)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = sqlConnection;
                    cmd.Transaction = tx;
                    cmd.CommandText = sql[i];
                    int result = cmd.ExecuteNonQuery();
                    cmd.CommandTimeout = 3000;
                }
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
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// 执行SQL语句，并返回数据库受影响的行数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public int ExcuteNonQuery(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            int result = cmd.ExecuteNonQuery();
            sqlConnection.Close();
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
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, sqlConnection);
                adapter.SelectCommand.CommandTimeout = 0;
                adapter.Fill(ds);
                if (ds == null || ds.Tables[0] == null)
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Log.WriteLog(e.Message, 0);
                return null;
            }
            finally
            {
                sqlConnection.Close();
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
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, sqlConnection);
                adapter.SelectCommand.CommandTimeout = 0;
                adapter.Fill(ds);
                if (ds == null || ds.Tables[0] == null)
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Log.WriteLog(e.Message, 0);
                return null;
            }
            finally
            {
                sqlConnection.Close();
            }

            return ds;
        }

        public void BulkToDB(DataTable dt, String tableName)
        {
            SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection);
            bulkCopy.DestinationTableName = tableName;
            bulkCopy.BatchSize = dt.Rows.Count;

            try
            {
                sqlConnection.Open();
                if (dt != null && dt.Rows.Count != 0)
                    bulkCopy.WriteToServer(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
                if (bulkCopy != null)
                    bulkCopy.Close();
            }
        }

        /// <summary>
        /// 关闭sql链接
        /// </summary>
        public void CloseConn()
        {
            try
            {
                sqlConnection.Close();
            }
            catch (Exception e)
            {
                Log.WriteLog(e.Message, 0);
            }
        }
    }
}