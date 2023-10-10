using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Security.Cryptography;
using Dapper;

namespace DAO
{
    public class config_file_first_kind_DAO
    {
      string conStr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";
        public async Task<IEnumerable<config_file_first_kind>> config_file_first_kind_Find()
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = "select * from config_file_first_kind";
               return await connection.QueryAsync<config_file_first_kind>(sql);

            }
        }

        /// <summary>
        /// I级机构添加
        /// </summary>
        /// <returns></returns>
        public async Task<int> CFFK_FindMaxID(config_file_first_kind cffk)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"insert into [dbo].[config_file_first_kind]([first_kind_id], [first_kind_name], [first_kind_salary_id], [first_kind_sale_id])values((SELECT TOP 1  CASE  WHEN [first_kind_id] + 1 < 10 THEN '0' + CAST([first_kind_id] + 1 AS VARCHAR(2)) ELSE CAST([first_kind_id] + 1 AS VARCHAR(2))  END AS FormattedValue FROM [dbo].[config_file_first_kind] ORDER BY ffk_id DESC),'{cffk.first_kind_name}','{cffk.first_kind_salary_id}','{cffk.first_kind_sale_id}')";
                return await connection.ExecuteAsync(sql);

            }
        }

        /// <summary>
        /// I级机构删除
        /// </summary>
        /// <param name="cffk"></param>
        /// <returns></returns>
        public async Task<int> CFFK_Delete(string cffk)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql1 = $"SELECT * FROM [dbo].[config_file_second_kind] WHERE [first_kind_id]='{cffk}'";
                List<config_file_second_kind> list=  connection.Query<config_file_second_kind>(sql1).ToList();
                if (list.Count==0)
                {
                    string sql = $"delete from config_file_first_kind where [first_kind_id] ={cffk}";
                    return await connection.ExecuteAsync(sql);
                }
                else
                {
                      return 2;
                }


            }
        }
        /// <summary>
        /// I级机构查单个数据
        /// </summary>
        /// <param name="cffk"></param>
        /// <returns></returns>

        public async Task<config_file_first_kind> CFFK_Update_show(int CFFFKid)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select * from config_file_first_kind where [ffk_id]={CFFFKid}";
                return await connection.QueryFirstAsync<config_file_first_kind>(sql);

            }
        }

        /// <summary>
        /// I级机构修改
        /// </summary>
        /// <param name="cffk"></param>
        /// <returns></returns>

        public async Task<int> CFFK_Update(config_file_first_kind cffk)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"update config_file_first_kind set [first_kind_salary_id]='{cffk.first_kind_salary_id}', [first_kind_sale_id]='{cffk.first_kind_sale_id}' where [ffk_id]='{cffk.ffk_id}' ";
                return await connection.ExecuteAsync(sql);

            }
        }
    }
}
