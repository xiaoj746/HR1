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
    public class salary_management_settings_DAO
    {
        string conStr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";
        public async Task<IEnumerable<salary_management_settings>> SMS_Find()
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = "select * from salary_management_settings";
                return await connection.QueryAsync<salary_management_settings>(sql);

            }
        }

      
        public async Task<int> SMS_Delete(int ItemID)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {

                    string sql = $"delete from salary_management_settings where [item_id] ={ItemID}";
                    return await connection.ExecuteAsync(sql);         
            }
        }
    }
}
