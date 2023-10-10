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
    public class major_change_DAO
    {
        string conStr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

        public async Task<IEnumerable<config_file_first_kind_cader>> RLFind()
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = "SELECT first_kind_id as value,first_kind_name as label FROM config_file_first_kind";
                List<config_file_first_kind_cader> list_cf = new List<config_file_first_kind_cader> { };
                List<config_file_first_kind_cader> list = connection.Query<config_file_first_kind_cader>(sql).ToList();
                foreach (var item in list)
                {
                    string sql1 = $"select [second_kind_id] as value,[second_kind_name] as label from [dbo].[config_file_second_kind] where [first_kind_id]='{item.value}'";
                    List<config_file_first_kind_cader> list1 = connection.Query<config_file_first_kind_cader>(sql1).ToList();
                    if (list1.Count == 0)
                    {
                        config_file_first_kind_cader cf = new config_file_first_kind_cader()
                        {

                            value = item.value,
                            label = item.label,
                            children = connection.Query<config_file_first_kind_cader>(sql1).ToList()
                        };
                        list_cf.Add(cf);
                    }
                    else
                    {
                        var i = 1;
                        foreach (var item1 in list1)
                        {
                            string sql2 = $"select [third_kind_id] as value,[third_kind_name] as label from [dbo].[config_file_third_kind] where [second_kind_id]='{item1.value}'";
                            config_file_first_kind_cader cf = new config_file_first_kind_cader() { };
                            item1.children = connection.Query<config_file_first_kind_cader>(sql2).ToList(); // 将第三级数据赋值给第二级的children属性
                            if (i == 1)
                            {
                                cf.value = item.value;
                                cf.label = item.label;
                                cf.children = list1;
                                list_cf.Add(cf);
                            }
                            if (list1.Count > 1)
                            {
                                i++;

                            }
                        }
                    }
                };
                return list_cf;
            }

        }

        public async Task<human_file_XJ> HFFindByid(string hid)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select * from [dbo].[human_file] where human_id='{hid}'";
                return await connection.QueryFirstAsync<human_file_XJ>(sql);
            }
        }

        public async Task<IEnumerable<Config_major_cader>> CM_FindChidern()
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = "select [major_kind_id] as value ,major_kind_name as label from [dbo].[config_major_kind]";
                List<Config_major_cader> list_cm = new List<Config_major_cader> { };
                List<Config_major_cader> list = connection.Query<Config_major_cader>(sql).ToList();
                foreach (Config_major_cader item in list)
                {
                    string sql1 = $"select [major_id] as value,[major_name] as label from [dbo].[config_major] where [major_kind_id]='{item.value}'";
                    Config_major_cader cf = new Config_major_cader()
                    {

                        value = item.value,
                        label = item.label,
                        children = connection.Query<Config_major_cader>(sql1).ToList()
                    };
                    list_cm.Add(cf);
                };
                return list_cm;
            }
        }

        public async Task<IEnumerable<salary_standard>> MS_SSFind()
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select * from [dbo].[salary_standard] ";
                return await connection.QueryAsync<salary_standard>(sql);
            }
        }

        public async Task<int> MC_Add(major_change mc)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"insert into [dbo].[major_change]([first_kind_id], [first_kind_name], [second_kind_id], [second_kind_name], [third_kind_id], [third_kind_name], [major_kind_id], [major_kind_name], [major_id], [major_name], [new_first_kind_id], [new_first_kind_name], [new_second_kind_id], [new_second_kind_name], [new_third_kind_id], [new_third_kind_name], [new_major_kind_id], [new_major_kind_name], [new_major_id], [new_major_name], [human_id], [human_name], [salary_standard_id], [salary_standard_name], [salary_sum], [new_salary_standard_id], [new_salary_standard_name], [new_salary_sum], [change_reason],  [register],  [regist_time])" +
                    $"values('{mc.first_kind_id}','{mc.first_kind_name}','{mc.second_kind_id}','{mc.second_kind_name}','{mc.third_kind_id}','{mc.third_kind_name}','{mc.major_kind_id}','{mc.major_kind_name}','{mc.major_id}','{mc.major_name}','{mc.new_first_kind_id}','{mc.new_first_kind_name}','{mc.new_second_kind_id}','{mc.new_second_kind_name}','{mc.new_third_kind_id}','{mc.new_third_kind_name}','{mc.new_major_kind_id}','{mc.new_major_kind_name}','{mc.new_major_id}','{mc.new_major_name}','{mc.human_id}','{mc.human_name}','{mc.salary_standard_id}','{mc.salary_standard_name}','{mc.salary_sum}','{mc.new_salary_standard_id}','{mc.new_salary_standard_name}','{mc.new_salary_sum}','{mc.change_reason}','{mc.register}',getdate())";
                return await connection.ExecuteAsync(sql);
            }
        }


        public async Task<FenYe<major_change>> MCFindFenye(FenYe<major_change> fenYe, string str)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@pageSize", fenYe.PageSize);
                dynamicParameters.Add("@keyName", "[mch_id]");
                dynamicParameters.Add("@tableName", "[dbo].[major_change]");
                dynamicParameters.Add("@currentPage", fenYe.CurrentPage);
                dynamicParameters.Add("@where", str);
                dynamicParameters.Add("@rows", direction: ParameterDirection.Output, dbType: DbType.Int32);
                string sql = "exec [dbo].[procFenYe] @pageSize, @keyName, @tableName, @currentPage,@where, @rows out";
                fenYe.List = await connection.QueryAsync<major_change>(sql, dynamicParameters);
                fenYe.Rows = dynamicParameters.Get<int>("rows");
                return fenYe;

            }
        }

        public async Task<major_change> MCFindByid(string hid)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select * from [dbo].[major_change] where [mch_id]='{hid}'";
                return await connection.QueryFirstAsync<major_change>(sql);
            }
        }

        public async Task<major_change> MCFindBymid(int mid)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select * from [dbo].[major_change] where [mch_id]='{mid}'";
                return await connection.QueryFirstAsync<major_change>(sql);
            }
        }

        public async Task<int> MC_Update(major_change mc)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                if (mc.new_first_kind_id==null)
                {
                    mc.new_first_kind_id = " ";
                    mc.new_first_kind_name = " ";
                }
                if (mc.new_second_kind_id == null)
                {
                    mc.new_second_kind_id = " ";
                    mc.new_second_kind_name = " ";
                }
                if (mc.new_third_kind_id == null)
                {
                    mc.new_third_kind_id = " ";
                    mc.new_third_kind_name = " ";
                }
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@human_id", mc.human_id);
                dynamicParameters.Add("@new_first_kind_id", mc.new_first_kind_id);
                dynamicParameters.Add("@new_first_kind_name", mc.new_first_kind_name);
                dynamicParameters.Add("@new_second_kind_id", mc.new_second_kind_id);
                dynamicParameters.Add("@new_second_kind_name", mc.new_second_kind_name);
                dynamicParameters.Add("@new_third_kind_id", mc.new_third_kind_id);
                dynamicParameters.Add("@new_third_kind_name", mc.new_third_kind_name);
                dynamicParameters.Add("@new_major_kind_id", mc.new_major_kind_id);
                dynamicParameters.Add("@new_major_kind_name", mc.new_major_kind_name);
                dynamicParameters.Add("@new_major_id", mc.new_major_id);
                dynamicParameters.Add("@new_major_name", mc.new_major_name);
                dynamicParameters.Add("@new_salary_standard_id", mc.new_salary_standard_id);
                dynamicParameters.Add("@new_salary_standard_name", mc.new_salary_standard_name);
                dynamicParameters.Add("@new_salary_sum", mc.new_salary_sum);
                dynamicParameters.Add("@check_reason", mc.check_reason);
                dynamicParameters.Add("@check_status", mc.check_status);
                dynamicParameters.Add("@checker", mc.checker);
                return await connection.ExecuteAsync("[MC_Update]", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }


    }
}
