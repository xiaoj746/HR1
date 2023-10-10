using Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
   public   class Config_major_kindDAO
    {
        string conStr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";
        //查询
        public async Task<IEnumerable<Config_major_kind>> Config_major_kindSelectAsync()
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = "select * from Config_major_kind";
                return await connection.QueryAsync<Config_major_kind>(sql);
            }
        } 
        //删除
        public async Task<int> Config_major_kindDeleteAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"delete from Config_major_kind where mfk_id={id}";
                return await connection.ExecuteAsync(sql);
            }
        }
        //添加
        public async Task<int> Config_major_kindInsertAsync(Config_major_kind c)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"insert into [dbo].[config_major_kind]([major_kind_id],[major_kind_name])values((SELECT TOP 1  CASE  WHEN  + 1 < 10 THEN '0' + CAST([major_kind_id] + 1 AS VARCHAR(2)) ELSE CAST([major_kind_id] + 1 AS VARCHAR(2))  END AS FormattedValue FROM [dbo].[config_major_kind] ORDER BY [major_kind_id] DESC),'{c.Major_kind_name}')";
                return await connection.ExecuteAsync(sql);
            }
        }
        
    }

}
