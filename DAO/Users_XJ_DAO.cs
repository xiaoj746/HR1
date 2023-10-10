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
    public class Users_XJ_DAO
    {
        string conStr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

        public async Task<Users_XJ> Users_Login(Users_XJ users)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {

                string sql = $"SELECT * FROM [dbo].[users] WHERE [u_name]='{users.u_name}' AND [u_password]='{users.u_password}'";
                try
                {
                    return await connection.QueryFirstAsync<Users_XJ>(sql);
                }
                catch (Exception)
                {

                    return null;
                }
            }

        }

        public async Task<IEnumerable<Users_XJ>> Users_Find()
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {

                string sql = $"SELECT * FROM [dbo].[users]";
                  return await connection.QueryAsync<Users_XJ>(sql);              
            }
        }

        public async Task<FenYe<UserRolesFenye>> UserFenye_Find(FenYe<UserRolesFenye> fenYe, string str)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@pageSize", fenYe.PageSize);
                dynamicParameters.Add("@keyName", "UserID");
                dynamicParameters.Add("@tableName", "UserRolesFenye");
                dynamicParameters.Add("@currentPage", fenYe.CurrentPage);
                dynamicParameters.Add("@where", str);
                dynamicParameters.Add("@rows", direction: ParameterDirection.Output, dbType: DbType.Int32);
                string sql = "exec [dbo].[procFenYe] @pageSize, @keyName, @tableName, @currentPage,@where, @rows out";
                fenYe.List = await connection.QueryAsync<UserRolesFenye>(sql, dynamicParameters);
                fenYe.Rows = dynamicParameters.Get<int>("rows");
                return fenYe;

            }
        }
        
        public async Task<IEnumerable<RolesXJ>> RolesFind()
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = "select * from [dbo].[roles] where rolesIf='是'";
                return await connection.QueryAsync<RolesXJ>(sql);

            }
        }

        public async Task<int> userAdd(UserRolesFenye user)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@userName",user.u_name);
                dynamicParameters.Add("@userPwd", user.u_password);
                dynamicParameters.Add("@userTrueName", user.u_true_name);
                dynamicParameters.Add("@rolesID", user.RolesID);

                string sql = "exec [dbo].[UserAdd] @userName, @userPwd, @userTrueName, @rolesID";
                return await connection.ExecuteAsync(sql, dynamicParameters);

            }
        }

        public async Task<UserRolesFenye> UserRolesByid(int id)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select * from  [dbo].[UserRolesFenye] where u_id={id}";
                return await connection.QueryFirstAsync<UserRolesFenye>(sql);

            }
        }

        public async Task<int> userUpdate(UserRolesFenye user)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@uid", user.u_id);
                dynamicParameters.Add("@userName", user.u_name);
                dynamicParameters.Add("@userPwd", user.u_password);
                dynamicParameters.Add("@userTrueName", user.u_true_name);
                dynamicParameters.Add("@rolesID", user.RolesID);

                string sql = "exec [dbo].[UserUpdate] @uid,@userName, @userPwd, @userTrueName, @rolesID";
                return await connection.ExecuteAsync(sql, dynamicParameters);

            }
        }

        public async Task<int> userDelete(int uid)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@uid", uid);          
                string sql = "exec [dbo].[UserDelete] @uid";
                return await connection.ExecuteAsync(sql, dynamicParameters);
            }
        }
    }
}
