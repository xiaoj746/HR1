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
    public class salary_standard_details_DAO
    {
        string conStr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

        public async Task<int> SSD_Add(salary_standard_details_S s)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {

                string sql = "";
                int res=0;
                for (int i = 0; i < s.salary.Length; i++)
                {
                   sql = $"insert into [dbo].[salary_standard_details]values('{s.standard_id}','{s.standard_name}','{s.count[i].pbc_id}','{s.count[i].attribute_name}','{s.salary[i]}')";
                  res  = await connection.ExecuteAsync(sql);
                }
                if (res >0) {

                    string sql2 = $"insert into [dbo].[salary_standard]([standard_id], [standard_name], [designer], [register],[salary_sum], [regist_time],[remark])values('{s.standard_id}','{s.standard_name}','{s.designer}','{s.register}','{s.salary_sum}',getdate(),'{s.remark}')";
                    return await connection.ExecuteAsync(sql2);

                }
                return 0;
            }
        }
    }
}
