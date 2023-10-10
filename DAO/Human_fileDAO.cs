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
    public class Human_fileDAO
    {
        string conStr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";
        public async Task<int> Human_registerInsert(Engage_resume h)
        {
            using (
                
                SqlConnection con = new SqlConnection(conStr))
            {
                string sql = $@"insert into [dbo].[human_file]([human_id],[first_kind_id],[first_kind_name],
[second_kind_id],[second_kind_name],
[third_kind_id],[third_kind_name],
[human_major_kind_id],[human_major_kind_name],
[human_major_id],[hunma_major_name],[human_pro_designation],
[human_name],[human_sex],[human_email],
[human_telephone],[human_qq],[human_mobilephone],
[human_address],[human_postcode],
[human_nationality],[human_birthplace],[human_birthday],[human_race],
[human_religion],[human_party],[human_society_security_id],[human_id_card],
[human_age],[human_educated_degree],[human_educated_years],[human_educated_major],
[salary_standard_id],[salary_standard_name],[salary_sum],[human_bank],[human_account],[register],
[regist_time],[human_speciality],[human_hobby],
[human_histroy_records],[human_family_membership],[remark],human_file_status,human_picture,check_status)
values
(CONCAT('bt', REPLACE(REPLACE(REPLACE(CONVERT(varchar(19), GETDATE(), 120), '-', ''), ':', ''), ' ', ''), RIGHT(CONVERT(varchar(10), ABS(CHECKSUM(NEWID()))), 4)),(select [first_kind_id] from [dbo].[config_file_first_kind] where [first_kind_name]='{h.First_kind_name}'),'{h.First_kind_name}',
(select [second_kind_id] from [dbo].[config_file_second_kind] where [second_kind_name]='{h.Second_kind_name}'),'{h.Second_kind_name}',
(select [third_kind_id] from[dbo].[config_file_third_kind]where[third_kind_name]='{h.Third_kind_name}'),'{h.Third_kind_name}',
(select[major_kind_id]  from  [dbo].[config_major_kind] where[major_kind_name]='{h.Human_major_kind_name}'),'{h.Human_major_kind_name}',
(select [major_id] from [dbo].[config_major]where [major_name]='{h.Human_major_name}'),'{h.Human_major_name}','{h.Human_pro_designation}',
'{h.Human_name}','{h.Human_sex}','{h.Human_email}',
'{h.Human_telephone}','{h.Human_qq}','{h.Human_mobilephone}',
'{h.Human_address}','{h.Human_postcode}',
'{h.Human_nationality}','{h.Human_birthplace}','{h.Human_birthday}','{h.Human_race}',
'{h.Human_religion}','{h.Human_party}','{h.Human_society_security_id}','{h.Human_idcard}',
'{h.Human_age}','{h.Human_educated_degree}','{h.Human_educated_years}','{h.Human_educated_major}',
(select [standard_id] from[dbo].[salary_standard] where [standard_name]='{h.Salary_standard_name}'),'{h.Salary_standard_name}',
(select [salary_sum] from[dbo].[salary_standard] where [standard_name]='{h.Salary_standard_name}'),
'{h.Human_bank}','{h.Human_account}','{h.Register2}',
'{h.Regist_time2}','{h.Human_speciality}','{h.Human_hobby}',
'{h.Human_histroy_records}','{h.Human_family_membership}','{h.Remark1}',0,'{h.Human_picture}',0)";
                con.Execute(sql);
                if (h.Second_kind_name==null && h.Third_kind_name==null)
                {
                   string sql2 = $"update [dbo].[human_file] set [second_kind_id] =' ',[second_kind_name] =' ',[third_kind_id]=' ',[third_kind_name]=' '  where second_kind_id is NULL AND [third_kind_id] IS NULL";
                    con.Execute(sql2);

                }
                if ( h.Third_kind_name == null && h.Second_kind_name !=null)
                {
                    string sql3 = $"update [dbo].[human_file] set [third_kind_id]=' ' ,[third_kind_name]=' ' where  [third_kind_id] IS NULL";
                    con.Execute(sql3);

                }
                string sql1 = $"update [dbo].[engage_resume] set [register] ='{h.Register2}' where [res_id]='{h.Res_id}'";

                return await con.ExecuteAsync(sql1);
            }
        }
        //查薪资
        public async Task<IEnumerable<salary_standard>> cxxz()
        {
            using (SqlConnection  con=new SqlConnection(conStr))
            {
                string sql = "select * from salary_standard";
                return await con.QueryAsync<salary_standard>(sql);
            }
        }
        public async Task<FenYe<Human_file>> rlfh(int pageSize, int currentPage)
        {
            FenYe<Human_file> fenYe = new FenYe<Human_file>();
            fenYe.PageSize = pageSize;
            fenYe.CurrentPage = currentPage;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sum = "1=1 AND check_status=0";
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("@pageSize", fenYe.PageSize);
                dynamic.Add("@keyName", "huf_id");
                dynamic.Add("@tableName", "human_file");
                dynamic.Add("@currentPage", fenYe.CurrentPage);
                dynamic.Add("@where", sum);
                dynamic.Add("@rows", direction: ParameterDirection.Output, dbType: DbType.Int32);
                string sql = "exec [dbo].[procFenYe] @pageSize,@keyName,@tableName,@currentPage,@where,@rows out";
                fenYe.List = await con.QueryAsync<Human_file>(sql, dynamic);
                fenYe.Rows = dynamic.Get<int>("rows");
                return fenYe;
            }
        }

        public async Task<Human_file> fhcx(int id)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sql = $"SELECT  * FROM   Human_file where huf_id={id} ";
                return await con.QueryFirstAsync<Human_file>(sql);
            }
        }
        public async Task<int> humanupdate(Engage_resume e)
        {
            using (SqlConnection con=new SqlConnection(conStr))
            {
                
                string sql = $@"update [dbo].[human_file] set [human_pro_designation]='{e.Human_pro_designation}',[human_name]='{e.Human_name}',[human_sex]='{e.Human_sex}',[human_email]='{e.Human_email}',[human_telephone]='{e.Human_telephone}',
[human_qq]='{e.Human_qq}',[human_mobilephone]='{e.Human_mobilephone}',[human_address]='{e.Human_address}',[human_postcode]='{e.Human_postcode}',[human_nationality]='{e.Human_nationality}',[human_birthplace]='{e.Human_birthplace}',[human_birthday]='{e.Human_birthday}',
[human_race]='{e.Human_race}',[human_religion]='{e.Human_religion}',[human_party]='{e.Human_party}',[human_id_card]='{e.Human_id_card}',[human_society_security_id]='{e.Human_society_security_id}',[human_age]='{e.Human_age}',
[human_educated_degree]='{e.Human_educated_degree}',[human_educated_years]='{e.Human_educated_years}',[human_educated_major]='{e.Human_educated_major}',
[salary_standard_id]=(select standard_id from [dbo].[salary_standard] where [standard_name]='{e.Salary_standard_name}'),
[salary_standard_name]='{e.Salary_standard_name}',[salary_sum]=(select salary_sum from [dbo].[salary_standard] where [standard_name]='{e.Salary_standard_name}'),
[human_bank]='{e.Human_bank}',[human_account]='{e.Human_account}',[checker]='{e.Checker}',[check_time]='{e.Check_time}',[human_speciality]='{e.Human_speciality}',[human_hobby]='{e.Human_hobby}',[human_histroy_records]='{e.Human_history_records}',[human_family_membership]='{e.Human_family_membership}',[remark]='{e.Remark}',check_status=1
where[huf_id]='{e.Huf_id}'";
                return await con.ExecuteAsync(sql);
            }
        }

    }
}
