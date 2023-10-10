using Dapper;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAO
{
    public class RolesDAO
    {
        string conStr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

        public async Task<List<Jurisdiction_s>> Jurisdiction_Find()
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = "select * from [dbo].[Jurisdiction]";
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

        public async Task<int> rolesDelete(int rid)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql2 = $"select * from [dbo].[RolesJurisdiction]  where [rolesID] ='{rid}' ";
                List<RolesJurisdiction> list = connection.Query<RolesJurisdiction>(sql2).ToList();
                if (list.Count>0)
                {
                    return 2;
                }
                else
                {
                    string sql = $"delete  from [dbo].[roles] where [rolesID]='{rid}'";
                    int result = connection.Execute(sql);
                    if (result > 0)
                    {
                        string sql1 = $"delete from [dbo].[RolesJurisdiction] where [rolesID]='{rid}'";
                        return await connection.ExecuteAsync(sql1);
                    }
                    return result;
                }
              
            }
        }

        public async Task<int> roles_add(RolesXJ rolesXJ)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"insert into [dbo].[roles] values('{rolesXJ.RolesName}','{rolesXJ.RolesInstruction}','{rolesXJ.RolesIf}')";
                return await connection.ExecuteAsync(sql);
            }
        }

        public async Task<FenYe<RolesXJ>> Roles_Find(FenYe<RolesXJ> fenYe, string str)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@pageSize", fenYe.PageSize);
                dynamicParameters.Add("@keyName", "[rolesID]");
                dynamicParameters.Add("@tableName", "[dbo].[roles]");
                dynamicParameters.Add("@currentPage", fenYe.CurrentPage);
                dynamicParameters.Add("@where", str);
                dynamicParameters.Add("@rows", direction: ParameterDirection.Output, dbType: DbType.Int32);
                string sql = "exec [dbo].[procFenYe] @pageSize, @keyName, @tableName, @currentPage,@where, @rows out";
                fenYe.List = await connection.QueryAsync<RolesXJ>(sql, dynamicParameters);
                fenYe.Rows = dynamicParameters.Get<int>("rows");
                return fenYe;

            }
        }

        public async Task<int[]> getJurisByid(int rid)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select ro.[JuriID] from [dbo].[RolesJurisdiction] ro inner join [dbo].[Jurisdiction] jur on jur.JuriID=ro.JuriID where pid!=0 and rolesID ={rid}";
                return   connection.Query<int>(sql).ToArray();
            }
        }

        public async Task<RolesXJ> getJuris(int rid)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select * from [dbo].[roles] where [rolesID]={rid}";
                return await connection.QueryFirstAsync<RolesXJ>(sql);
            }
        }

        public async Task<int> roles_update(RolesXJ rolesXJ, string juriID)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@RoleID", rolesXJ.RolesID);
                dynamicParameters.Add("@RoleName", rolesXJ.RolesName);
                dynamicParameters.Add("@RolesInstruction", rolesXJ.RolesInstruction);
                dynamicParameters.Add("@RolesIf", rolesXJ.RolesIf);
                dynamicParameters.Add("@JuriID", juriID);
                string sql = "exec [dbo].[rolesUpdate] @RoleID,@RoleName,@RolesInstruction,@RolesIf,@JuriID";
                return await connection.ExecuteAsync(sql, dynamicParameters);
            }
        }
    }
}
