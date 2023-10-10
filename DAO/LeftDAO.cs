using Dapper;
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
    public class LeftDAO
    {
        string conStr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

        public async Task<List<Jurisdiction_s>> GetTress(int uid)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sqlID = $"select [RolesID] from [dbo].[UserRoles] where [UserID]={uid}";
                UserRolesFenye userRolesFenye = new UserRolesFenye();
                userRolesFenye = connection.QueryFirst<UserRolesFenye>(sqlID);

                string sql = $"select * from [dbo].[RolesJurisdiction] r  inner join [dbo].[Jurisdiction] j on r.JuriID=j.JuriID where [rolesID]='{userRolesFenye.RolesID}'";
                List<Jurisdiction> juris = connection.Query<Jurisdiction>(sql).ToList();
                List<Jurisdiction_s> trees = GetJurisdiction_s(juris, 0);
                return trees;
            }   

        }

        private List<Jurisdiction_s> GetJurisdiction_s(List<Jurisdiction> list, int v)
        {
            List<Jurisdiction_s> trees = new List<Jurisdiction_s>();
            List<Jurisdiction> plist = list.Where(e => e.Pid == v).ToList();
            foreach (Jurisdiction item in plist)
            {
                Jurisdiction_s trees1 = new Jurisdiction_s()
                {
                    id = item.JuriID,
                    Name = item.JurName,
                    groupID_s = item.GroupID,
                    url = item.JurAddress,
                    children = GetJurisdiction_s(list, item.JuriID)
                };
                trees.Add(trees1);
            }
            return trees;
        }

        public async Task<UserRolesFenye> RUFind(int u_id)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select * from [dbo].[UserRolesFenye]  where [UserID]={u_id}";
                return await connection.QueryFirstAsync<UserRolesFenye>(sql);
            }
        }
    }
}
