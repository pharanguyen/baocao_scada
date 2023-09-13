using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DAO
{
    public interface ISqlHelper
    {
        string LoiNgoaiLe { get; set; }
        SqlConnection objSqlConn { get; set; }
        List<SqlParameter> objSqlParamtter { get; }
        List<SqlParameter> objSqlParamtterWhere { get; }
        string SQLConnectionString { get; set; }

        void AddParameter(string parmName, object parmValue);
        void AddParameterBinaryNull(string parmName);
        void AddParameterWhere(string parmName, object parmValue);
        bool BeginTransaction();
        bool ComitTransaction();
        int? DeleteEntity<T>(T obj);
        Task<int?> DeleteEntityAsync<T>(T obj);
        int? DeleteEntityById<T>(object Id);
        Task<int?> DeleteEntityByIdAsync<T>(object Id);
        int? DeleteListEntityByWhereCondition<T>(string whereCondition, object parameters);
        Task<int?> DeleteListEntityByWhereConditionAsync<T>(string whereCondition, object parameters);
        object doDelete(string TenBang, string where);
        object doInsert(string TenBang);
        object doInsertWithIdentity(string TenBang);
        object doUpdate(string TenBang, string where);
        int? ExcuteProc(string procName, object parameters = null);
        Task<int?> ExcuteProcAsync(string procName, object parameters = null);
        DataSet ExecutePrcDataSet(string sSQL);
        DataTable ExecutePrcDataTable(string sSQL);
        DataSet ExecuteSQLDataSet(string sSQL);
        DataTable ExecuteSQLDataTable(string sSQL);
        object ExecuteSQLNonQuery(string sSQL);
        object ExecuteSQLScalar(string sSQL);
        IEnumerable<T> GetAll<T>(object whereCondition = null) where T : class;
        Task<IEnumerable<T>> GetAllAsync<T>(object whereCondition = null) where T : class;
        IEnumerable<T> GetAllByWhereCondition<T>(string whereCondition, object parameters = null) where T : class;
        Task<IEnumerable<T>> GetAllByWhereConditionAsync<T>(string whereCondition, object parameters = null) where T : class;
        IEnumerable<T> GetMultiPageList<T>(int pageNumber = 1, int rowsPerPage = 10, string conditions = null, string orderBy = null, object parameters = null);
        Task<IEnumerable<T>> GetMultiPageListAsync<T>(int pageNumber = 1, int rowsPerPage = 10, string conditions = null, string orderBy = null, object parameters = null);
       // T GetSingleEntityById<T>(int Id) where T : class;
        T GetSingleEntityById<T>(long Id) where T : class;
        Task<T> GetSingleEntityByIdAsync<T>(long Id) where T : class;
        T GetSingleEntityByKey<T, K>(K UniqueKey) where T : class;
        Task<T> GetSingleEntityByKeyAsync<T, K>(K UniqueKey) where T : class;
        string getSQLConnectionString();
        int? InsertEntity<T>(T obj) where T : class;
        TKey InsertEntity<TKey, T>(T obj) where T : class;
        int? InsertCompositeKey<T>(T obj) where T : class;
        Task<int?> InsertEntityAsync<T>(T obj) where T : class;
        IEnumerable<T> QueryProc<T>(string procName, object parameters = null);
        Task<IEnumerable<T>> QueryProcAsync<T>(string procName, object parameters = null);
        bool RollBackTransaction();
        int? UpdateEntity<T>(T obj) where T : class;
        Task<int?> UpdateEntityAsync<T>(T obj) where T : class;
        //Task<long> DPExcuteScalar(string sql, DynamicParameters parameters);
    }
}