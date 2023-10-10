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
    public class salary_grantXJ_DAO
    {
        string conStr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";
        public async Task<IEnumerable<human_file_XJ>> SGFind(string str)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"SELECT [first_kind_id],[first_kind_name],[second_kind_id],[second_kind_name],[third_kind_id],[third_kind_name], sum([salary_sum]) as [salary_sum],sum([paid_salary_sum]) as [paid_salary_sum], sum(case when {str} then 1 else 0 end) as Number FROM [dbo].[human_file] where {str} AND [demand_salaray_sum] IS NULL  GROUP BY [first_kind_name],[second_kind_name],[third_kind_name],[first_kind_id],[second_kind_id],[third_kind_id]";
                return await connection.QueryAsync<human_file_XJ>(sql);
            }
        }

        public async Task<FenYe<human_file_XJ>> HFFindFenye(FenYe<human_file_XJ> fenYe, string str)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@pageSize", fenYe.PageSize);
                dynamicParameters.Add("@keyName", "[huf_id]");
                dynamicParameters.Add("@tableName", "[dbo].[human_file]");
                dynamicParameters.Add("@currentPage", fenYe.CurrentPage);
                dynamicParameters.Add("@where", str);
                dynamicParameters.Add("@rows", direction: ParameterDirection.Output, dbType: DbType.Int32);
                string sql = "exec [dbo].[procFenYe] @pageSize, @keyName, @tableName, @currentPage,@where, @rows out";
                fenYe.List = await connection.QueryAsync<human_file_XJ>(sql, dynamicParameters);
                fenYe.Rows = dynamicParameters.Get<int>("rows");
                return fenYe;

            }
        }

        public async Task<IEnumerable<human_file_XJ>> HFFind(string str)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select [salary_standard_id], [first_kind_id],[first_kind_name],[second_kind_id],[second_kind_name],[third_kind_id],[third_kind_name], [human_id],[human_name],[salary_sum],STRING_AGG([item_id], ',') AS item_id ,STRING_AGG([item_name], ',') AS item_name ,STRING_AGG([salary], ',') AS salary from [dbo].[human_file] h" +
                    $"  inner join [dbo].[salary_standard_details] s on h.[salary_standard_id]=s.[standard_id] where " + str + " " +
                    "group by [human_id],[human_name],standard_id ,[salary_sum],[first_kind_name],[second_kind_name],[third_kind_name],[first_kind_id],[second_kind_id],[third_kind_id],[salary_standard_id]";
                return await connection.QueryAsync<human_file_XJ>(sql);
            }
        }

        public async Task<int> SG_Add(salary_grantXJ sg, salary_grant_detailsXJ sgd, int count)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@count", count);
                dynamicParameters.Add("@salary_grant_id", sg.salary_grant_id);
                dynamicParameters.Add("@human_id", sgd.human_id);
                dynamicParameters.Add("@human_name", sgd.human_name);
                dynamicParameters.Add("@bouns_sum", sgd.bouns_sum);
                dynamicParameters.Add("@sale_sum", sgd.sale_sum);
                dynamicParameters.Add("@deduct_sum", sgd.deduct_sum);
                dynamicParameters.Add("@salary_standard_sum", sgd.salary_standard_sum_sgd);
                dynamicParameters.Add("@salary_paid_sum", sgd.salary_paid_sum_sgd);
                dynamicParameters.Add("@first_kind_id", sg.first_kind_id);
                dynamicParameters.Add("@first_kind_name", sg.first_kind_name);
                dynamicParameters.Add("@second_kind_id", sg.second_kind_id);
                dynamicParameters.Add("@second_kind_name", sg.second_kind_name);
                dynamicParameters.Add("@third_kind_id", sg.third_kind_id);
                dynamicParameters.Add("@third_kind_name", sg.third_kind_name);
                dynamicParameters.Add("@salary_standard_sum_Total", sg.salary_standard_sum);
                dynamicParameters.Add("@salary_paid_sum_Total", sg.salary_paid_sum);
                dynamicParameters.Add("@human_amount", sg.human_amount);
                dynamicParameters.Add("@register", sg.register);
                //   string sql = "exec [SSD_Update] @count, @standard_id, @standard_name, @item_id,@Salary, @SalarySum, @Designer, @Checker, @CheckComment";
                return await connection.ExecuteAsync("[SGAdd]", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        //-------------复核-----------------------

        public async Task<FenYe<salary_grantXJ>> SGFindFenye(FenYe<salary_grantXJ> fenYe, string str)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@pageSize", fenYe.PageSize);
                dynamicParameters.Add("@keyName", "[sgr_id]");
                dynamicParameters.Add("@tableName", "[dbo].[salary_grant]");
                dynamicParameters.Add("@currentPage", fenYe.CurrentPage);
                dynamicParameters.Add("@where", str);
                dynamicParameters.Add("@rows", direction: ParameterDirection.Output, dbType: DbType.Int32);
                string sql = "exec [dbo].[procFenYe] @pageSize, @keyName, @tableName, @currentPage,@where, @rows out";
                fenYe.List = await connection.QueryAsync<salary_grantXJ>(sql, dynamicParameters);
                fenYe.Rows = dynamicParameters.Get<int>("rows");
                return fenYe;

            }
        }

        public async Task<IEnumerable<human_file_XJ>> HFFind_FH(string str)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select [salary_standard_id], [first_kind_id],[first_kind_name],[second_kind_id],[second_kind_name],[third_kind_id],[third_kind_name],  h.[human_id],h.[human_name],[salary_sum],[bouns_sum],[sale_sum],[deduct_sum],[salary_paid_sum],[salary_grant_id], STRING_AGG([item_id], ',') AS item_id ,STRING_AGG([item_name], ',') AS item_name ,STRING_AGG([salary], ',') AS salary from [dbo].[human_file] h" +
                    $"  inner join [dbo].[salary_standard_details] s on h.[salary_standard_id]=s.[standard_id]" +
                    $" inner join [dbo].[salary_grant_details] d on h.[human_id]=d.[human_id]" +
                    $" where " + str + " " +
                    "group by h.[human_id],h.[human_name],standard_id ,[salary_sum],[first_kind_name],[second_kind_name],[third_kind_name],[first_kind_id],[second_kind_id],[third_kind_id],[salary_standard_id],[bouns_sum],[sale_sum],[deduct_sum],[salary_paid_sum],[salary_grant_id]";
                return await connection.QueryAsync<human_file_XJ>(sql);
            }
        }

        public async Task<int> SG_Update(salary_grantXJ sg, salary_grant_detailsXJ sgd, int count)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {

                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@count", count);
                dynamicParameters.Add("@salary_grant_id", sg.salary_grant_id);
                dynamicParameters.Add("@human_id", sgd.human_id);
                dynamicParameters.Add("@bouns_sum", sgd.bouns_sum);
                dynamicParameters.Add("@sale_sum", sgd.sale_sum);
                dynamicParameters.Add("@deduct_sum", sgd.deduct_sum);
                dynamicParameters.Add("@salary_standard_sum", sgd.salary_standard_sum_sgd);
                dynamicParameters.Add("@salary_paid_sum", sgd.salary_paid_sum_sgd);
                dynamicParameters.Add("@salary_paid_sum_Total", sg.salary_paid_sum);
                dynamicParameters.Add("@checker", sg.checker);
             
                return await connection.ExecuteAsync("[SGUpdate]", dynamicParameters, commandType: CommandType.StoredProcedure);
            }

        }


        //------------------查询---------------------
        public async Task<salary_grantXJ> SGFindRegister(string sgid)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select * from [dbo].[salary_grant] where [salary_grant_id] = '{sgid}'";
                return await connection.QueryFirstAsync<salary_grantXJ>(sql);

            }
               
        }

        public async Task<IEnumerable<salary_standard_details>> SSDFindByid(string id)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select item_name,salary from [dbo].[salary_standard_details] where [standard_id] = '{id}'";
                return await connection.QueryAsync<salary_standard_details>(sql);

            }
        }
    }
}


