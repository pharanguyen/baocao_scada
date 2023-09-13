using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.IO;
using System.Collections.ObjectModel;

namespace DAO
{
    /// <summary>
    /// Class thao tác với CSDL
    /// </summary>

    public static class SqlConnectString
    {
        public static string strConnectString = "";
        public static void init(string str)
        {
            strConnectString = str;
        }
    }

    public class SqlHelper
    {

        #region "# SQLAuthenticationType #"
        public enum SQLAuthenticationType
        {
            WindowsAuthentication = 0,
            SQLServerAuthentication = 1
        }
        #endregion

        #region "# Property SQL Server  #"


        private string _SQLConnectionString = "";
        private SqlConnection _objSqlConn;
        private string _LoiNgoaiLe = "";

        public string _strSQL = "";


        public string SQLConnectionString
        {
            get { return _SQLConnectionString; }
            set { _SQLConnectionString = value; }
        }

        public string LoiNgoaiLe
        {
            get { return _LoiNgoaiLe; }
            set { _LoiNgoaiLe = value; }
        }

        public SqlConnection objSqlConn
        {
            get { return _objSqlConn; }
            set { _objSqlConn = value; }
        }

        #endregion

        #region "# Sub New -#"
        public SqlHelper(string strServerName, string strDatabaseName, string strUserNameSQL, string strPasswordSQL)
        {
            //_ServerName = strServerName
            //_DatabaseName = strDatabaseName
            //_UserNameSQL = strUserNameSQL
            //_PasswordSQL = strPasswordSQL
            //_Authentication = SQLAuthenticationType.SQLServerAuthentication
        }
        private string strConn;

        public SqlHelper()
        {
            _objSqlParamtter = new List<SqlParameter>();
            _objSqlParamtterWhere = new List<SqlParameter>();
            //IConfigurationBuilder builder = new ConfigurationBuilder();
            //builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

            //var root = builder.Build();
            ////strConn = root.GetConnectionString("Demo");
            //strConn = root.GetConnectionString("Live");
        }

        public SqlHelper(string _strConn)
        {
            _objSqlParamtter = new List<SqlParameter>();
            _objSqlParamtterWhere = new List<SqlParameter>();
            strConn = _strConn;
        }

        protected virtual void Dispose()
        {
            try
            {
                _objSqlConn.Dispose();
                if ((_objSqlConnWithTrans != null))
                {
                    _objSqlConnWithTrans.Dispose();
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region '#GetServerTime#'
        public DateTime GetServerTime()
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return _objSqlConn.QuerySingle<DateTime>("Select GetDate()");
                else
                    return _objSqlConn.QuerySingle<DateTime>("Select GetDate()", _objTransaction);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;
                //return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }
        }
        #endregion

        #region "# getSQLConnectionString #"
        public string getSQLConnectionString()
        {
            return SqlConnectString.strConnectString;
            //return strConn;
        }
        #endregion

        #region "# AddParameter #"
        private List<SqlParameter> _objSqlParamtter;
        private List<SqlParameter> _objSqlParamtterWhere;
        public List<SqlParameter> objSqlParamtter
        {
            get { return _objSqlParamtter; }// amàm
        }
        public List<SqlParameter> objSqlParamtterWhere
        {
            get { return _objSqlParamtterWhere; }
        }

        public void AddParameterBinaryNull(string parmName)
        {
            SqlParameter prm = new SqlParameter(parmName, SqlDbType.VarBinary);
            prm.Value = DBNull.Value;
            _objSqlParamtter.Add(prm);
        }

        public void AddParameter(string parmName, object parmValue)
        {
            if ((parmValue != null))
            {
                _objSqlParamtter.Add(new SqlParameter(parmName, parmValue));
            }
            else
            {
                _objSqlParamtter.Add(new SqlParameter(parmName, DBNull.Value));
            }
        }
        public void AddParameterWhere(string parmName, object parmValue)
        {
            if ((parmValue != null))
            {
                _objSqlParamtterWhere.Add(new SqlParameter(parmName, parmValue));
            }
            else
            {
                _objSqlParamtterWhere.Add(new SqlParameter(parmName, DBNull.Value));
            }
        }
        #endregion

        #region "# Transaction #"
        private SqlTransaction _objTransaction = null;
        private SqlConnection _objSqlConnWithTrans = null;
        public bool BeginTransaction()
        {
            _objSqlConnWithTrans = new SqlConnection(getSQLConnectionString());
            try
            {
                _objSqlConnWithTrans.Open();
                _objTransaction = _objSqlConnWithTrans.BeginTransaction();
                return true;
            }
            catch (Exception ex)
            {
                _LoiNgoaiLe = ex.Message;
                return false;
            }
        }

        public bool ComitTransaction()
        {
            try
            {
                if (_objSqlConnWithTrans == null)
                    return false;
                _objTransaction.Commit();
                if (_objSqlConnWithTrans.State != ConnectionState.Closed)
                {
                    _objSqlConnWithTrans.Close();
                    _objTransaction = null;
                    //_objTransaction.Dispose();
                    _objSqlConnWithTrans = null;
                    //_objSqlConnWithTrans.Dispose();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RollBackTransaction()
        {
            try
            {
                if (_objSqlConnWithTrans == null)
                    return false;
                _objTransaction.Rollback();
                if (_objSqlConnWithTrans.State != ConnectionState.Closed)
                {
                    _objSqlConnWithTrans.Close();
                    _objTransaction = null;
                    //_objTransaction.Dispose();
                    _objSqlConnWithTrans = null;
                    //_objSqlConnWithTrans.Dispose();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region "# ExecuteSQLDataTable #"
        public DataTable ExecuteSQLDataTable(string sSQL)
        {
            DataTable dt = new DataTable();
            SqlCommand _sqlCmd = new SqlCommand();

            try
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _sqlCmd.CommandText = sSQL;
                _sqlCmd.Connection = _objSqlConn;
                _objSqlConn.Open();
                foreach (SqlParameter p in _objSqlParamtter)
                {
                    _sqlCmd.Parameters.Add(p);
                }
                foreach (SqlParameter p in _objSqlParamtterWhere)
                {
                    _sqlCmd.Parameters.Add(p);
                }
                SqlDataAdapter da = new SqlDataAdapter(_sqlCmd);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                _LoiNgoaiLe = ex.Message;
                return null;
            }
            finally
            {
                _sqlCmd.Dispose();
                _objSqlParamtter.Clear();
                _objSqlParamtterWhere.Clear();
                if (_objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }
        }
        #endregion

        #region "# ExecuteSQLDataSet #"
        public DataSet ExecuteSQLDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            SqlCommand _sqlCmd = new SqlCommand();
            try
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _sqlCmd.CommandText = sSQL;
                _sqlCmd.Connection = _objSqlConn;
                _objSqlConn.Open();
                foreach (SqlParameter p in _objSqlParamtter)
                {
                    _sqlCmd.Parameters.Add(p);
                }
                foreach (SqlParameter p in _objSqlParamtterWhere)
                {
                    _sqlCmd.Parameters.Add(p);
                }
                SqlDataAdapter da = new SqlDataAdapter(_sqlCmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                _LoiNgoaiLe = ex.Message;
                return null;
            }
            finally
            {
                _sqlCmd.Dispose();
                _objSqlParamtter.Clear();
                _objSqlParamtterWhere.Clear();
                if (_objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }
        }
        #endregion

        #region "# ExecuteSQLNonQuery #"
        public object ExecuteSQLNonQuery(string sSQL)
        {
            SqlCommand _sqlCmd = new SqlCommand();
            try
            {
                if (_objSqlConnWithTrans == null)
                {
                    _objSqlConn = new SqlConnection(getSQLConnectionString());
                    _sqlCmd.Connection = _objSqlConn;
                    _objSqlConn.Open();
                }
                else
                {
                    _sqlCmd.Connection = _objSqlConnWithTrans;
                    _sqlCmd.Transaction = _objTransaction;
                }
                _sqlCmd.CommandText = sSQL;
                foreach (SqlParameter p in _objSqlParamtter)
                {
                    _sqlCmd.Parameters.Add(p);
                }
                foreach (SqlParameter p in _objSqlParamtterWhere)
                {
                    _sqlCmd.Parameters.Add(p);
                }
                return Convert.ToInt32(_sqlCmd.ExecuteNonQuery());
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex; // them ngay 5/1/2019
                //return null;
            }
            finally
            {
                _sqlCmd.Dispose();
                _objSqlParamtter.Clear();
                _objSqlParamtterWhere.Clear();
                if (_objSqlConnWithTrans == null)
                {
                    if (_objSqlConn.State != ConnectionState.Closed)
                    {
                        _objSqlConn.Close();
                        _objSqlConn.Dispose();
                    }
                }
            }
        }
        #endregion

        #region "# ExecuteSQLScalar #"
        public object ExecuteSQLScalar(string sSQL)
        {
            SqlCommand _sqlCmd = new SqlCommand();
            bool useTran = false;
            try
            {
                if (_objSqlConnWithTrans == null)
                {
                    _objSqlConn = new SqlConnection(getSQLConnectionString());
                    _objSqlConn.Open();
                    _sqlCmd.Connection = _objSqlConn;
                }
                else
                {
                    useTran = true;
                    _sqlCmd.Connection = _objSqlConnWithTrans;
                    _sqlCmd.Transaction = _objTransaction;
                }
                _sqlCmd.CommandText = sSQL;
                foreach (SqlParameter p in _objSqlParamtter)
                {
                    _sqlCmd.Parameters.Add(p);
                }
                foreach (SqlParameter p in _objSqlParamtterWhere)
                {
                    _sqlCmd.Parameters.Add(p);
                }
                return _sqlCmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;//them 5/1/2019
            }
            finally
            {
                _sqlCmd.Dispose();
                _objSqlParamtter.Clear();
                _objSqlParamtterWhere.Clear();
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }
        }
        #endregion

        #region "# ExecutePrcDataTable #"
        public DataTable ExecutePrcDataTable(string sSQL)
        {
            DataTable dt = new DataTable();
            SqlCommand _sqlCmd = new SqlCommand();
            try
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _sqlCmd.CommandText = sSQL;
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _sqlCmd.Connection = _objSqlConn;
                _objSqlConn.Open();
                foreach (SqlParameter p in _objSqlParamtter)
                {
                    _sqlCmd.Parameters.Add(p);
                }
                foreach (SqlParameter p in _objSqlParamtterWhere)
                {
                    _sqlCmd.Parameters.Add(p);
                }
                SqlDataAdapter da = new SqlDataAdapter(_sqlCmd);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                _LoiNgoaiLe = ex.Message;
                Console.WriteLine(ex.Message);
                throw ex; // sua 5/1/2019
                //return null;
            }
            finally
            {
                _sqlCmd.Dispose();
                _objSqlParamtter.Clear();
                _objSqlParamtterWhere.Clear();
                if (_objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }
        }
        #endregion

        #region "# ExecutePrcDataSet #"
        public DataSet ExecutePrcDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            SqlCommand _sqlCmd = new SqlCommand();
            try
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _sqlCmd.CommandText = sSQL;
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _sqlCmd.Connection = _objSqlConn;
                _objSqlConn.Open();
                foreach (SqlParameter p in _objSqlParamtter)
                {
                    _sqlCmd.Parameters.Add(p);
                }
                foreach (SqlParameter p in _objSqlParamtterWhere)
                {
                    _sqlCmd.Parameters.Add(p);
                }
                SqlDataAdapter da = new SqlDataAdapter(_sqlCmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                _LoiNgoaiLe = ex.Message;
                throw ex; // them ngay 5/1/2019
                //return null;
            }
            finally
            {
                _sqlCmd.Dispose();
                _objSqlParamtter.Clear();
                _objSqlParamtterWhere.Clear();
                if (_objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }
        }
        #endregion

        #region "# doInsert #"
        public object doInsert(string TenBang)
        {
            string sql = "SET DATEFORMAT DMY ";
            string str1 = "";
            string str2 = "";
            foreach (SqlParameter p in objSqlParamtter)
            {
                str1 += p.ParameterName.Substring(1) + ", ";
                str2 += p.ParameterName + ", ";
            }
            if (str1.Length > 2)
                str1 = str1.Substring(0, str1.Length - 2);
            if (str2.Length > 2)
                str2 = str2.Substring(0, str2.Length - 2);

            sql += $@"INSERT INTO [{ TenBang }]({str1}) VALUES({str2});SELECT SCOPE_IDENTITY();";
            return ExecuteSQLScalar(sql);
        }
        #endregion

        #region "# doInsertWithIDentity #"
        public object doInsertWithIdentity(string TenBang)
        {
            string sql = "SET DATEFORMAT DMY SET IDENTITY_INSERT " + TenBang + " ON ";
            string str1 = "";
            string str2 = "";
            foreach (SqlParameter p in objSqlParamtter)
            {
                str1 += p.ParameterName.Substring(1) + ", ";
                str2 += p.ParameterName + ", ";
            }
            if (str1.Length > 2)
                str1 = str1.Substring(0, str1.Length - 2);
            if (str2.Length > 2)
                str2 = str2.Substring(0, str2.Length - 2);
            sql += "INSERT INTO [" + TenBang + "](" + str1 + ") VALUES(" + str2 + ")  SET IDENTITY_INSERT " + TenBang + " OFF ; SELECT SCOPE_IDENTITY();";
            return ExecuteSQLScalar(sql);
        }
        #endregion

        #region "# doUpdate #"
        public object doUpdate(string TenBang, string @where)
        {
            string sql = "SET DATEFORMAT DMY UPDATE [" + TenBang + "] ";
            string str1 = "";
            foreach (SqlParameter p in objSqlParamtter)
            {
                str1 += p.ParameterName.Substring(1) + " = " + p.ParameterName + ", ";
            }
            if (str1.Length > 2)
                str1 = str1.Substring(0, str1.Length - 2);
            sql += "SET " + str1 + " ";
            sql += "WHERE " + @where + " ";
            return ExecuteSQLNonQuery(sql);
        }
        #endregion

        #region "# doDelete #"
        public object doDelete(string TenBang, string @where)
        {
            string sql = "DELETE FROM [" + TenBang + "] WHERE " + @where;
            return ExecuteSQLNonQuery(sql);
        }
        #endregion


        /// <summary>
        /// Các hàm thao tác với đối tượng
        /// </summary>

        #region "# CRUD_Object #"

        public int? InsertCompositeKey<T>(T obj) where T : class
        {
            string tableName = obj.GetType().Name;
            try
            {
                tableName = obj.GetType().CustomAttributes.Where(x => x.AttributeType.Name == "TableAttribute").SingleOrDefault().ConstructorArguments[0].Value.ToString();

            }
            catch
            {
                tableName = obj.GetType().Name;
            }
            DynamicParameters dynamicParameters = new DynamicParameters();
            string vlclause = "values(";
            foreach (PropertyInfo item in obj.GetType().GetProperties())
            {
                dynamicParameters.Add($"@{item.Name}", item.GetValue(obj));
                vlclause = vlclause + $"@{item.Name},";
            }
            vlclause = $"INSERT INTO {tableName} " + vlclause.Remove(vlclause.Length - 1) + ")";

            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return _objSqlConn.Execute(vlclause, dynamicParameters);
                else
                    return _objSqlConnWithTrans.Execute(vlclause, dynamicParameters, _objTransaction, null, null);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;
                //return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }
        }
        public int? InsertEntity<T>(T obj) where T : class  // them ngay 5/1/2019
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    //thư viện Dapper.SimpleCRUD
                    return _objSqlConn.Insert<T>(obj);
         
                else
                    return _objSqlConnWithTrans.Insert<T>(obj, _objTransaction);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;
                //return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }

        }


        public TKey InsertEntity<TKey, T>(T obj) where T : class  // them ngay 5/1/2019
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return _objSqlConn.Insert<TKey, T>(obj);
                else
                    return _objSqlConnWithTrans.Insert<TKey, T>(obj, _objTransaction);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;
                //return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }

        }

        public int? UpdateEntity<T>(T obj) where T : class  // them ngay 5/1/2019
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return _objSqlConn.Update<T>(obj);
                else
                    return _objSqlConnWithTrans.Update<T>(obj, _objTransaction);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;
                //return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }

        }

        public T GetSingleEntityById<T>(long Id) where T : class  // them ngay 5/1/2019
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return _objSqlConn.Get<T>(Id);
                else
                    return _objSqlConnWithTrans.Get<T>(Id, _objTransaction);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;
                //return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }

        }

        public T GetSingleEntityByKey<T, K>(K UniqueKey) where T : class  // them ngay 5/1/2019
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return _objSqlConn.Get<T>(UniqueKey);
                else
                    return _objSqlConnWithTrans.Get<T>(UniqueKey, _objTransaction);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;
                //return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }

        }

        public IEnumerable<T> GetAll<T>(object whereCondition = null) where T : class  // them ngay 5/1/2019
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return _objSqlConn.GetList<T>(whereCondition);
                else
                    return _objSqlConnWithTrans.GetList<T>(whereCondition, _objTransaction);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;
                //return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }
        }

        public IEnumerable<T> GetAllByWhereCondition<T>(string whereCondition, object parameters = null) where T : class  // them ngay 5/1/2019
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return _objSqlConn.GetList<T>(whereCondition, parameters);
                else
                    return _objSqlConnWithTrans.GetList<T>(whereCondition, parameters, _objTransaction);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                //throw ex;
                return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }
        }

        public int? DeleteListEntityByWhereCondition<T>(string whereCondition, object parameters)
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return _objSqlConn.DeleteList<T>(whereCondition, parameters);
                else
                    return _objSqlConnWithTrans.DeleteList<T>(whereCondition, parameters, _objTransaction);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;
                //return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }
        }

        public int? DeleteEntity<T>(T obj)
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return _objSqlConn.Delete<T>(obj);
                else
                    return _objSqlConnWithTrans.Delete<T>(obj, _objTransaction);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;
                //return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }
        }

        public int? DeleteEntityById<T>(object Id)
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return _objSqlConn.Delete<T>(Id);
                else
                    return _objSqlConnWithTrans.Delete<T>(Id, _objTransaction);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;
                //return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }
        }

        public int? ExcuteProc(string procName, object parameters = null)
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return _objSqlConn.Execute(procName, parameters, null, null, CommandType.StoredProcedure);
                else
                    return _objSqlConnWithTrans.Execute(procName, parameters, _objTransaction, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;
                //return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }
        }

        public IEnumerable<T> QueryProc<T>(string procName, object parameters = null)
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return _objSqlConn.Query<T>(procName, parameters, null, true, null, CommandType.StoredProcedure);
                else
                    return _objSqlConnWithTrans.Query<T>(procName, parameters, _objTransaction, true, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
              //  throw ex;
                return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }
        }



        public IEnumerable<T> GetMultiPageList<T>(int pageNumber = 1, int rowsPerPage = 10, string conditions = null, string orderBy = null, object parameters = null)
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return _objSqlConn.GetListPaged<T>(pageNumber, rowsPerPage, conditions, orderBy, parameters);
                else
                    return _objSqlConnWithTrans.GetListPaged<T>(pageNumber, rowsPerPage, conditions, orderBy, parameters, _objTransaction);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;
                //return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }
        }

        #endregion

        /// <summary>
        /// Các hàm thao tác bất đồng bộ dữ liệu với đối tượng
        /// </summary>

        #region "# Async_CRUD_OBJECT #"

        public async Task<int?> InsertEntityAsync<T>(T obj) where T : class  // them ngay 5/1/2019
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return await _objSqlConn.InsertAsync<T>(obj);
                else
                    return await _objSqlConnWithTrans.InsertAsync<T>(obj, _objTransaction);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;
                //return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }

        }
        public async Task<int?> UpdateEntityAsync<T>(T obj) where T : class  // them ngay 5/1/2019
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return await _objSqlConn.UpdateAsync<T>(obj);
                else
                    return await _objSqlConnWithTrans.UpdateAsync<T>(obj, _objTransaction);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;
                //return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }

        }

        public async Task<T> GetSingleEntityByIdAsync<T>(long Id) where T : class  // them ngay 5/1/2019
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return await _objSqlConn.GetAsync<T>(Id);
                else
                    return await _objSqlConnWithTrans.GetAsync<T>(Id, _objTransaction);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;
                //return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }

        }

        public async Task<T> GetSingleEntityByKeyAsync<T, K>(K UniqueKey) where T : class  // them ngay 5/1/2019
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return await _objSqlConn.GetAsync<T>(UniqueKey);
                else
                    return await _objSqlConnWithTrans.GetAsync<T>(UniqueKey, _objTransaction);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;
                //return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }

        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(object whereCondition = null) where T : class  // them ngay 5/1/2019
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return await _objSqlConn.GetListAsync<T>(whereCondition);
                else
                    return await _objSqlConnWithTrans.GetListAsync<T>(whereCondition, _objTransaction);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;
                //return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }
        }

        public async Task<IEnumerable<T>> GetAllByWhereConditionAsync<T>(string whereCondition, object parameters = null) where T : class  // them ngay 5/1/2019
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return await _objSqlConn.GetListAsync<T>(whereCondition, parameters);
                else
                    return await _objSqlConnWithTrans.GetListAsync<T>(whereCondition, parameters, _objTransaction);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;
                //return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }
        }

        public async Task<int?> DeleteListEntityByWhereConditionAsync<T>(string whereCondition, object parameters)
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return await _objSqlConn.DeleteListAsync<T>(whereCondition, parameters);
                else
                    return await _objSqlConnWithTrans.DeleteListAsync<T>(whereCondition, parameters, _objTransaction);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;
                //return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }
        }

        public async Task<int?> DeleteEntityAsync<T>(T obj)
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return await _objSqlConn.DeleteAsync<T>(obj);
                else
                    return await _objSqlConnWithTrans.DeleteAsync<T>(obj, _objTransaction);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;
                //return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }
        }

        public async Task<int?> DeleteEntityByIdAsync<T>(object Id)
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return await _objSqlConn.DeleteAsync<T>(Id);
                else
                    return await _objSqlConnWithTrans.DeleteAsync<T>(Id, _objTransaction);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;
                //return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }
        }

        public async Task<int?> ExcuteProcAsync(string procName, object parameters = null)
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return await _objSqlConn.ExecuteAsync(procName, parameters, null, null, CommandType.StoredProcedure);
                else
                    return await _objSqlConnWithTrans.ExecuteAsync(procName, parameters, _objTransaction, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;
                //return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }
        }

        public async Task<IEnumerable<T>> QueryProcAsync<T>(string procName, object parameters = null)
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return await _objSqlConn.QueryAsync<T>(procName, parameters, null, null, CommandType.StoredProcedure);
                else
                    return await _objSqlConnWithTrans.QueryAsync<T>(procName, parameters, _objTransaction, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;
                //return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }
        }


        public async Task<IEnumerable<T>> GetMultiPageListAsync<T>(int pageNumber = 1, int rowsPerPage = 10, string conditions = null, string orderBy = null, object parameters = null)
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return await _objSqlConn.GetListPagedAsync<T>(pageNumber, rowsPerPage, conditions, orderBy, parameters);
                else
                    return await _objSqlConnWithTrans.GetListPagedAsync<T>(pageNumber, rowsPerPage, conditions, orderBy, parameters, _objTransaction);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
                throw ex;
                //return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }
        }

        #endregion

        #region #Dapper Basic SQL Command#
        public int? DapperExcuteSqlNonQuery(string sql, DynamicParameters parameters)
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return _objSqlConn.Execute(sql, parameters);
                else
                    return _objSqlConnWithTrans.Execute(sql, parameters, _objTransaction);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
               // throw ex;
                return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }
        }

        public async Task<int?> DapperExcuteSqlNonQueryAsync(string sql, DynamicParameters parameters)
        {
            bool useTran = false;
            if (_objSqlConnWithTrans == null)
            {
                _objSqlConn = new SqlConnection(getSQLConnectionString());
                _objSqlConn.Open();
            }
            else
            {
                useTran = true;
            }
            try
            {
                if (!useTran)
                    return await _objSqlConn.ExecuteAsync(sql, parameters);
                else
                    return await _objSqlConnWithTrans.ExecuteAsync(sql, parameters, _objTransaction);
            }
            catch (Exception ex)
            {
                LoiNgoaiLe = ex.Message;
            //    throw ex;
                return null;
            }
            finally
            {
                if (!useTran && _objSqlConn != null && _objSqlConn.State != ConnectionState.Closed)
                {
                    _objSqlConn.Close();
                    _objSqlConn.Dispose();
                }
            }
        }
        #endregion
    }
}
