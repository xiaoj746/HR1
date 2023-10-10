﻿using Dapper;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class config_file_third_kind_DAO
    {
        string conStr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

        public async Task<int> CFTK_Add(config_file_third_kind cftk)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"insert into [config_file_third_kind]([first_kind_id], [first_kind_name], [second_kind_id], [second_kind_name],[third_kind_id], [third_kind_name], [third_kind_sale_id], [third_kind_is_retail])" +
                    $"values('{cftk.first_kind_id}',(select [first_kind_name] from [dbo].[config_file_first_kind] where [first_kind_id]='{cftk.first_kind_id}')," +
                    $" '{cftk.second_kind_id}',(select [second_kind_name] from [dbo].[config_file_second_kind] where [second_kind_id]='{cftk.second_kind_id}')," +
                    $"(SELECT TOP 1  CASE  WHEN [third_kind_id] + 1 < 10 THEN '0' + CAST([third_kind_id] + 1 AS VARCHAR(2)) ELSE CAST([third_kind_id] + 1 AS VARCHAR(2))  END AS FormattedValue FROM [dbo].[config_file_third_kind] ORDER BY ftk_id DESC)," +
                    $"'{cftk.third_kind_name}','{cftk.third_kind_sale_id}','{cftk.third_kind_is_retail}')";
                return await connection.ExecuteAsync(sql);

            }
        }

        public async Task<int> CFTK_Delete(int ftkID)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"delete from config_file_third_kind where [ftk_id]='{ftkID}'";
                return await connection.ExecuteAsync(sql);

            }
        }

        public async Task<IEnumerable<config_file_third_kind>> CFTK_Find()
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = "SELECT * FROM [dbo].[config_file_third_kind]";
                return await connection.QueryAsync<config_file_third_kind>(sql);

            }
        }

        public async Task<int> CFTK_Update(config_file_third_kind cftk)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"update config_file_third_kind set [third_kind_sale_id]='{cftk.third_kind_sale_id}',[third_kind_is_retail]='{cftk.third_kind_is_retail}' where [ftk_id]='{cftk.ftk_id}'";
                return await connection.ExecuteAsync(sql);

            }
        }

        public async Task<config_file_third_kind> CFTK_Update_ByidShow(int tkID)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"SELECT * FROM [dbo].[config_file_third_kind] where [ftk_id]='{tkID}'";
                return await connection.QueryFirstAsync<config_file_third_kind>(sql);

            }
        }

        public async Task<IEnumerable<config_file_first_kind_cader>> CFTK_FindChidern()
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = "SELECT first_kind_id as value,first_kind_name as label FROM config_file_first_kind";
                List<config_file_first_kind_cader> list_cf = new List<config_file_first_kind_cader> { };
                List<config_file_first_kind_cader> list = connection.Query<config_file_first_kind_cader>(sql).ToList();
                foreach (var item in list)
                {
                    string sql1 = $"select [second_kind_id] as value,[second_kind_name] as label from [dbo].[config_file_second_kind] where [first_kind_id]='{item.value}'";
                    config_file_first_kind_cader cf = new config_file_first_kind_cader()
                    {
                       
                        value=item.value,
                        label=item.label,
                        children = connection.Query<config_file_first_kind_cader>(sql1).ToList()
                    };
                    list_cf.Add(cf);
                };
                return list_cf;
            }
        }

    }
}
