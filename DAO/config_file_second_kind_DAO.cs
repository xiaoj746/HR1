using Dapper;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class config_file_second_kind_DAO
    {
        string conStr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

        public async Task<int> CFSK_Add(config_file_second_kind cfsk)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"insert into [dbo].[config_file_second_kind]([first_kind_id], [first_kind_name], [second_kind_id], [second_kind_name], [second_salary_id], [second_sale_id])" +
                    $"values('{cfsk.first_kind_id}',(select [first_kind_name] from [dbo].[config_file_first_kind] where [first_kind_id]='{cfsk.first_kind_id}')," +
                    $"(SELECT TOP 1  CASE  WHEN [second_kind_id] + 1 < 10 THEN '0' + CAST([second_kind_id] + 1 AS VARCHAR(2)) ELSE CAST([second_kind_id] + 1 AS VARCHAR(2))  END AS FormattedValue FROM [dbo].[config_file_second_kind] ORDER BY fsk_id DESC)," +
                    $"'{cfsk.second_kind_name}','{cfsk.second_salary_id}','{cfsk.second_sale_id}')";
                return await connection.ExecuteAsync(sql);
            }
        }

        

        public async Task<IEnumerable<config_file_second_kind>> config_file_second_kind_Find()
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = "SELECT * FROM [dbo].[config_file_second_kind]";
                return await connection.QueryAsync<config_file_second_kind>(sql);

            }
        }

        public async Task<config_file_second_kind> CFSK_Update_ByidShow(int cfskID)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select * from config_file_second_kind where [fsk_id]={cfskID}";
                return await connection.QueryFirstAsync<config_file_second_kind>(sql);

            }
        }

        public async Task<int> CFSK_Update(config_file_second_kind cfsk)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"update config_file_second_kind set [second_salary_id]='{cfsk.second_salary_id}',[second_sale_id]='{cfsk.second_sale_id}' where [fsk_id]='{cfsk.fsk_id}'";
                return await connection.ExecuteAsync(sql);

            }
        }

        public async Task<int> CFSK_Delete(string skID)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql1 = $"SELECT * FROM [dbo].[config_file_third_kind] WHERE [second_kind_id]='{skID}'";
                List<config_file_third_kind> list = connection.Query<config_file_third_kind>(sql1).ToList();
                if (list.Count == 0)
                {
                    string sql = $"delete from config_file_second_kind where [second_kind_id]='{skID}'";
                    return await connection.ExecuteAsync(sql);
                }
                else
                {
                    return 2;
                }
               

            }
        }
    }
}
