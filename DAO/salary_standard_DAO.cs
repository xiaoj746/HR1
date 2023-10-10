using Dapper;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class salary_standard_DAO
    {
        string conStr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";
        public async Task<FenYe<salary_standard>> SS_Find(FenYe<salary_standard> fenYe,string str)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@pageSize", fenYe.PageSize);
                dynamicParameters.Add("@keyName", "ssd_id");
                dynamicParameters.Add("@tableName", "salary_standard");
                dynamicParameters.Add("@currentPage", fenYe.CurrentPage);
                dynamicParameters.Add("@where", str);
                dynamicParameters.Add("@rows", direction: ParameterDirection.Output, dbType: DbType.Int32);
                string sql = "exec [dbo].[procFenYe] @pageSize, @keyName, @tableName, @currentPage,@where, @rows out";
                fenYe.List = await connection.QueryAsync<salary_standard>(sql, dynamicParameters);
                fenYe.Rows = dynamicParameters.Get<int>("rows");
                return fenYe;

            }
        }
        public async Task<IEnumerable<salary_standard_details>> SSD_Find(string sid)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();

                string sql = $"select * from [dbo].[salary_standard_details] where [standard_id]='{sid}'";
                return await connection.QueryAsync<salary_standard_details>(sql);
            }
        }

        public async Task<salary_standard> SS_FindByid(string sid)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();

                string sql = $"select * from [dbo].[salary_standard] where [standard_id]='{sid}'";
                return await connection.QueryFirstAsync<salary_standard>(sql);
            }
        }

        public async Task<int> SS_Update(salary_standard ss, string salary, string item_id, string count)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@count", count);
                dynamicParameters.Add("@standard_id", ss.standard_id);
                dynamicParameters.Add("@standard_name", ss.standard_name);
                dynamicParameters.Add("@item_id", item_id);
                dynamicParameters.Add("@Salary", salary);
                dynamicParameters.Add("@SalarySum", ss.salary_sum);
                dynamicParameters.Add("@Designer", ss.designer);
                dynamicParameters.Add("@Checker", ss.checker);
                dynamicParameters.Add("@CheckComment", ss.check_comment);
                dynamicParameters.Add("@Check_status", ss.check_status);
             //   string sql = "exec [SSD_Update] @count, @standard_id, @standard_name, @item_id,@Salary, @SalarySum, @Designer, @Checker, @CheckComment";
                return await connection.ExecuteAsync("[SSD_Update]", dynamicParameters,commandType:CommandType.StoredProcedure);
            }
        }

        public async Task<int> Change_Update(salary_standard ss, string salary, string item_id, string count)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@count", count);
                dynamicParameters.Add("@standard_id", ss.standard_id);
                dynamicParameters.Add("@standard_name", ss.standard_name);
                dynamicParameters.Add("@item_id", item_id);
                dynamicParameters.Add("@Salary", salary);
                dynamicParameters.Add("@SalarySum", ss.salary_sum);
                dynamicParameters.Add("@Designer", ss.designer);
                dynamicParameters.Add("@Changer", ss.changer);;
                dynamicParameters.Add("@Remark", ss.remark);
                //   string sql = "exec [SSD_Update] @count, @standard_id, @standard_name, @item_id,@Salary, @SalarySum, @Designer, @Checker, @CheckComment";
                return await connection.ExecuteAsync("[chage_Update]", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
