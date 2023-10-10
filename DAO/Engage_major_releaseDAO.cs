using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Model;
namespace DAO
{
    public class Engage_major_releaseDAO
    {
        string conStr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";
        public async Task<FenYe<Engage_major_release>> Engage_major_releaseSelectAsync(FenYe<Engage_major_release> fenYe)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                //  string sql = "select * from Trees";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@pageSize", fenYe.PageSize);
                dynamicParameters.Add("@keyName", "mre_id");
                dynamicParameters.Add("@tableName", "Engage_major_release");
                dynamicParameters.Add("@currentPage", fenYe.CurrentPage);
                dynamicParameters.Add("@rows", direction: ParameterDirection.Output, dbType: DbType.Int32);
                string sql = "exec [dbo].[proc_FenYe] @pageSize, @keyName, @tableName, @currentPage, @rows out";
                fenYe.List = await connection.QueryAsync<Engage_major_release>(sql, dynamicParameters);
                fenYe.Rows = dynamicParameters.Get<int>("rows");
                return fenYe;
            }
        }
        //删除
        public async Task<int> Engage_major_releaseDeleteAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"delete from Engage_major_release where mre_id={id}";
                return await connection.ExecuteAsync(sql);
            }
        }
        //总条数
        public async Task<IEnumerable<Engage_major_release>> Engage_major_releaseSelectztsAsync()
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = "select count(*) from Engage_major_release";
                return await connection.QueryAsync<Engage_major_release>(sql);
            }
        }
        //根据id查询
        public async Task<Engage_major_release> Engage_major_releaseSelectidAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select * from Engage_major_release where mre_id={id}";
                return await connection.QueryFirstAsync<Engage_major_release>(sql);
            }
        }
        public async Task<Engage_resume> Engage_major_releaseSelectkindidAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select major_kind_id,major_kind_name,major_id,major_name, engage_type from Engage_major_release where mre_id={id}";
                return await connection.QueryFirstAsync<Engage_resume>(sql);
            }
        }
        //修改
        public async Task<int> Engage_major_releaseUpdateAsync(Engage_major_release e)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $@" update [engage_major_release] set 
[first_kind_id]=(select [first_kind_id] from[dbo].[config_file_first_kind] where [first_kind_name]='{e.First_kind_name}'),[first_kind_name]='{e.First_kind_name}',
[second_kind_id]=(select [second_kind_id] from [dbo].[config_file_second_kind] where [second_kind_name]='{e.Second_kind_name}'),[second_kind_name]='{e.Second_kind_name}',
[third_kind_id]=(select [third_kind_id] from [dbo].[config_file_third_kind] where [third_kind_name]='{e.Third_kind_name}'),[third_kind_name]='{e.Third_kind_name}',
[major_kind_id]=(select [major_kind_id] from [dbo].[config_major_kind] where [major_kind_name]='{e.Major_kind_name}'),[major_kind_name]='{e.Major_kind_name}',
[major_id]=(select [major_id] from [dbo].[config_major] where [major_name]='{e.Major_name}'),[major_name]='{e.Major_name}',
[human_amount]='{e.Human_amount}',[engage_type]='{e.Engage_type}',[deadline]='{e.Deadline}',[changer]='{e.Changer}',[change_time]='{e.Change_time}',[major_describe]='{e.Major_describe}',[engage_required]='{e.Engage_required}' where mre_id='{e.Mre_id}'";
                return await connection.ExecuteAsync(sql);

            }
        }
        //I一级机构
        public async Task<IEnumerable<config_file_first_kind>> Iselect()
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $" select * from [config_file_first_kind] ";
                return await connection.QueryAsync<config_file_first_kind>(sql);

            }
        }
        //II 级机构
        public async Task<IEnumerable<config_file_second_kind>> IIselect(string name)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $" select * from [config_file_second_kind]  where [first_kind_name]='{name}'";
                return await connection.QueryAsync<config_file_second_kind>(sql);

            }
        }
        //III级机构
        public async Task<IEnumerable<config_file_third_kind>> IIIselect(string name,string name2)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select * from[dbo].config_file_third_kind where [first_kind_name]='{name}' and [second_kind_name]='{name2}'";
                return await connection.QueryAsync<config_file_third_kind>(sql);

            }
        }
        //职位发布登记添加
        public async Task<int> Engage_major_releaseInsertAsync(Engage_major_release e)
        {
            using (SqlConnection  con=new SqlConnection(conStr))
            {
                string sql = $@"insert into [dbo].[engage_major_release]([first_kind_id],[first_kind_name],[second_kind_id],[second_kind_name],[third_kind_id],[third_kind_name],[major_kind_id],[major_kind_name],[major_id],[major_name],[human_amount],[engage_type],[deadline],[register],[Regist_time],[major_describe],[engage_required]) 
                                        values((select [first_kind_id] from[dbo].[config_file_first_kind] where [first_kind_name]='{e.First_kind_name}'),'{e.First_kind_name}',
                                              (select [second_kind_id] from [dbo].[config_file_second_kind] where [second_kind_name]='{e.Second_kind_name}'),'{e.Second_kind_name}',
                                               (select [third_kind_id] from [dbo].[config_file_third_kind] where [third_kind_name]='{e.Third_kind_name}'),'{e.Third_kind_name}',
                                                (select [major_kind_id] from [dbo].[config_major_kind] where [major_kind_name]='{e.Major_kind_name}'),'{e.Major_kind_name}',
                                                (select [major_id] from [dbo].[config_major] where [major_name]='{e.Major_name}'),'{e.Major_name}',
'{e.Human_amount}','{e.Engage_type}','{e.Deadline}','{e.Register}','{e.Regist_time} ','{e.Major_describe}','{e.Engage_required}')";
                return await con.ExecuteAsync(sql) ;
            }
        }
    }
}
