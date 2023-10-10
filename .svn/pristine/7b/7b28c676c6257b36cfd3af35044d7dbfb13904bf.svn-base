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
    public class config_public_char_DAOXJ
    {
        string conStr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";
        public async Task<IEnumerable<config_public_charXJ>> SMS_Find()
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select * from [dbo].[config_public_char] where [attribute_kind]='薪酬设置'";
                return await connection.QueryAsync<config_public_charXJ>(sql);

            }
        }


        public async Task<int> SMS_Delete(int pbcID)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {

                string sql = $"delete from config_public_char where [pbc_id] ={pbcID}";
                return await connection.ExecuteAsync(sql);
            }
        }
    }
}

