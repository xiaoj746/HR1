using Dapper;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class Engage_resumeDAO
    {
        string conStr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";
        public async Task<int> Engage_resumeinsert(Engage_resume e)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $@"insert into [dbo].[engage_resume]
                                    ([human_major_kind_id],[human_major_kind_name],
                                        [human_major_id],[human_major_name],[engage_type],
                                        [human_name],[human_sex],[human_email],
                                        [human_telephone],[human_homephone],[human_mobilephone],
                                        [human_address],[human_postcode],
                                        [human_nationality],[human_birthplace],[human_birthday],
                                        [human_race],[human_religion],[human_party],
                                        [human_idcard],[human_age],[human_college],[human_educated_degree],
                                        human_educated_years,human_educated_major,demand_salary_standard,regist_time,
                                        human_specility,human_hobby,human_history_records,remark,interview_status,pass_checkcomment,pass_passcomment,check_status,human_picture)
                                        values((select major_kind_id from config_major_kind where major_kind_name='{e.Major_kind_name}'),'{e.Major_kind_name}',
                                         (select major_id from config_major where major_name='{e.Major_name}'),'{e.Major_name}',
                                        '{e.Engage_type}','{e.Human_name}','{e.Human_sex}','{e.Human_email}','{e.Human_telephone}','{e.Human_homephone}','{e.Human_mobilephone}',
                                        '{e.Human_address}','{e.Human_postcode}','{e.Human_nationality}','{e.Human_birthplace}','{e.Human_birthday}',
                                        '{e.Human_race}','{e.Human_religion}','{e.Human_party}','{e.Human_idcard}','{e.Human_age}','{e.Human_college}',
                                        '{e.Human_educated_degree}','{e.Human_educated_years}','{e.Human_educated_major}','{e.Demand_salary_standard}',
                                        '{e.Regist_time}','{e.Human_specility}','{e.Human_hobby}','{e.Human_history_records}','{e.Remark}','0','不通过','不通过','0','{e.Human_picture}'
)";

                return await connection.ExecuteAsync(sql);
            }
        }

        public async Task<IEnumerable<Config_major>> Config_majorSelectAsync(string name)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select * from Config_major where  major_kind_name='{name}'";
                return await connection.QueryAsync<Config_major>(sql);
            }
        }
        public async Task<FenYe<Engage_resume>> select(FenYe<Engage_resume> fenYe)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {

                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@pageSize", fenYe.PageSize);
                dynamicParameters.Add("@keyName", "res_id");
                dynamicParameters.Add("@tableName", "Engage_resume");
                dynamicParameters.Add("@currentPage", fenYe.CurrentPage);
                dynamicParameters.Add("@rows", direction: ParameterDirection.Output, dbType: DbType.Int32);
                string sql = "exec [dbo].[proc_FenYe] @pageSize, @keyName, @tableName, @currentPage, @rows out";
                fenYe.List = await con.QueryAsync<Engage_resume>(sql, dynamicParameters);
                fenYe.Rows = dynamicParameters.Get<int>("rows");
                return fenYe;
            }
        }
        public async Task<Engage_resume> selectid(int id)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sql = $"select * from Engage_resume where res_id={id} and check_status='0'";
                return await con.QueryFirstAsync<Engage_resume>(sql);
            }
        }
        //修改
        public async Task<int> update(Engage_resume e)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sql = $@"update [dbo].[engage_resume] 
set [engage_type]='{e.Engage_type}',[human_name]='{e.Human_name}',[human_sex]='{e.Human_sex}',[human_email]='{e.Human_email}',
[human_telephone]='{e.Human_telephone}',[human_homephone]='{e.Human_homephone}',[human_mobilephone]='{e.Human_mobilephone}',
[human_address]='{e.Human_address}',[human_postcode]='{e.Human_postcode}',
[human_nationality]='{e.Human_nationality}',[human_birthplace]='{e.Human_birthplace}',[human_birthday]='{e.Human_birthday}',
[human_race]='{e.Human_race}',[human_religion]='{e.Human_religion}',[human_party]='{e.Human_party}',
[human_idcard]='{e.Human_idcard}',[human_age]='{e.Human_age}',[human_college]='{e.Human_college}',[human_educated_degree]='{e.Human_educated_degree}',
[human_educated_years]='{e.Human_educated_years}',[human_educated_major]='{e.Human_educated_major}',[demand_salary_standard]='{e.Demand_salary_standard}',
[human_specility]='{e.Human_specility}',[human_hobby]='{e.Human_hobby}',[checker]='{e.Checker}',[check_time]='{e.Check_time}',
[human_history_records]='{e.Human_history_records}',[remark]='{e.Remark}',[recomandation]='{e.Recomandation}',[check_status]='1' where [res_id]='{e.Res_id}'";
                return await con.ExecuteAsync(sql);
            }
        }
        public async Task<IEnumerable<Users_XJ>> cxname()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sql = "select u_true_name from users";
                return await con.QueryAsync<Users_XJ>(sql);
            }
            
        }
        public async Task<FenYe<Engage_resume>> tjselect(int pageSize, int currentPage, string human_major_kind_name, string human_major_name,string remark, string begintime,string endtime,int a )
        {
            FenYe<Engage_resume> fenYe=new FenYe<Engage_resume> ();
            fenYe.PageSize= pageSize;
            fenYe.CurrentPage= currentPage;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sum = "1=1 ";
                if (a==0)
                {
                    sum += "AND check_status=0";
                }
                if (a==1)
                {
                    sum += "AND check_status=1";
                }
                if (human_major_kind_name != null)
                {
                    sum += $"AND human_major_kind_name like '{human_major_kind_name}'";
                }
                if (human_major_name != null)
                {
                    sum += $"AND human_major_name like '{human_major_name}'";
                }
                if (begintime != null && endtime != null)
                {
                    sum += $"AND regist_time between'{begintime}'and '{endtime}'";
                }
                if (remark != null)
                {
                    sum += $"AND remark like '%{remark}%'";
                }
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("@pageSize", fenYe.PageSize);
                dynamic.Add("@keyName", "res_id"); 
                dynamic.Add("@tableName", "Engage_resume");
                dynamic.Add("@currentPage", fenYe.CurrentPage);
                dynamic.Add("@where", sum);
                dynamic.Add("@rows", direction: ParameterDirection.Output, dbType: DbType.Int32);
                string sql = "exec [dbo].[procFenYe] @pageSize,@keyName,@tableName,@currentPage,@where,@rows out";
               
                fenYe.List = await con.QueryAsync<Engage_resume>(sql,dynamic);
                fenYe.Rows = dynamic.Get<int>("rows");
                return fenYe;
            }
        }
        public async Task<FenYe<Engage_resume>> yxjl(int pageSize, int currentPage)
        {
            FenYe<Engage_resume> fenYe = new FenYe<Engage_resume>();
            fenYe.PageSize = pageSize;
            fenYe.CurrentPage = currentPage;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sum = "1=1 AND check_status=1 AND interview_status=0";
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("@pageSize", fenYe.PageSize);
                dynamic.Add("@keyName", "res_id");
                dynamic.Add("@tableName", "Engage_resume");
                dynamic.Add("@currentPage", fenYe.CurrentPage);
                dynamic.Add("@where", sum);
                dynamic.Add("@rows", direction: ParameterDirection.Output, dbType: DbType.Int32);
                string sql = "exec [dbo].[procFenYe] @pageSize,@keyName,@tableName,@currentPage,@where,@rows out";
                fenYe.List = await con.QueryAsync<Engage_resume>(sql, dynamic);
                fenYe.Rows = dynamic.Get<int>("rows");
                return fenYe;
            }
        }
        public async Task<FenYe<Engage_resume>> lysq(int pageSize, int currentPage)
        {
            FenYe<Engage_resume> fenYe = new FenYe<Engage_resume>();
            fenYe.PageSize = pageSize;
            fenYe.CurrentPage = currentPage;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sum = "1=1 AND interview_status=2 AND pass_checkComment='不通过'";
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("@pageSize", fenYe.PageSize);
                dynamic.Add("@keyName", "res_id");
                dynamic.Add("@tableName", "Engage_resume");
                dynamic.Add("@currentPage", fenYe.CurrentPage);
                dynamic.Add("@where", sum);
                dynamic.Add("@rows", direction: ParameterDirection.Output, dbType: DbType.Int32);
                string sql = "exec [dbo].[procFenYe] @pageSize,@keyName,@tableName,@currentPage,@where,@rows out";
                fenYe.List = await con.QueryAsync<Engage_resume>(sql, dynamic);
                fenYe.Rows = dynamic.Get<int>("rows");
                return fenYe;
            }
        }
        public async  Task<Engage_resume> msdj(int id)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sql = $"select * from Engage_resume where res_id={id}";
                return await con.QueryFirstAsync<Engage_resume>(sql);
            }
        }
        public async Task<int> xgzt(Engage_resume e)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sql = $@"update Engage_resume set recomandation='推荐', interview_status= 1 where res_id={e.Res_id}";
                return await con.ExecuteAsync(sql);
            }
        }
        
        public async Task<FenYe<Engage_resume>> mssx(int pageSize, int currentPage)
        {
            FenYe<Engage_resume> fenYe = new FenYe<Engage_resume>();
            fenYe.PageSize = pageSize;
            fenYe.CurrentPage = currentPage;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sum = "1=1 AND result = ''";
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("@pageSize", fenYe.PageSize);
                dynamic.Add("@keyName", "ein_id");
                dynamic.Add("@tableName", "engage_interview");
                dynamic.Add("@currentPage", fenYe.CurrentPage);
                dynamic.Add("@where", sum);
                dynamic.Add("@rows", direction: ParameterDirection.Output, dbType: DbType.Int32);
                string sql = "exec [dbo].[procFenYe] @pageSize,@keyName,@tableName,@currentPage,@where,@rows out";
                fenYe.List = await con.QueryAsync<Engage_resume>(sql, dynamic);
                fenYe.Rows = dynamic.Get<int>("rows");
                return fenYe;
            }
        }
        public async Task<Engage_resume> mssx(int id)
        {
            using(SqlConnection con = new SqlConnection(conStr))
            {
                string sql = $"SELECT   dbo.engage_resume.*, dbo.engage_interview.* FROM   dbo.engage_interview CROSS JOIN  dbo.engage_resume where res_id={id} and resume_id={id}";
                return await con.QueryFirstAsync<Engage_resume>(sql);
            }
        }
        public async Task<int> xglysq(Engage_resume e)
        {
            using(SqlConnection con = new SqlConnection(conStr))
            {
                string sql = $"update engage_resume set interview_status=2 where res_id={e.Res_id}";
                return await con.ExecuteAsync(sql);
            }
        }
        public async Task<FenYe<Engage_resume>> lysp(int pageSize, int currentPage)
        {
            FenYe<Engage_resume> fenYe = new FenYe<Engage_resume>();
            fenYe.PageSize = pageSize;
            fenYe.CurrentPage = currentPage;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sum = "1=1 AND pass_checkComment = '通过' AND pass_passcomment='不通过'";
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("@pageSize", fenYe.PageSize);
                dynamic.Add("@keyName", "res_id");
                dynamic.Add("@tableName", "engage_resume");
                dynamic.Add("@currentPage", fenYe.CurrentPage);
                dynamic.Add("@where", sum);
                dynamic.Add("@rows", direction: ParameterDirection.Output, dbType: DbType.Int32);
                string sql = "exec [dbo].[procFenYe] @pageSize,@keyName,@tableName,@currentPage,@where,@rows out";
                fenYe.List = await con.QueryAsync<Engage_resume>(sql, dynamic);
                fenYe.Rows = dynamic.Get<int>("rows");
                return fenYe;
            }
        }
        public async Task<FenYe<Engage_resume>> cxly(int pageSize, int currentPage)
        {
            FenYe<Engage_resume> fenYe = new FenYe<Engage_resume>();
            fenYe.PageSize = pageSize;
            fenYe.CurrentPage = currentPage;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sum = "1=1 AND pass_checkComment = '通过' AND pass_passcomment='通过' AND register is NULL";
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("@pageSize", fenYe.PageSize);
                dynamic.Add("@keyName", "res_id");
                dynamic.Add("@tableName", "engage_resume");
                dynamic.Add("@currentPage", fenYe.CurrentPage);
                dynamic.Add("@where", sum);
                dynamic.Add("@rows", direction: ParameterDirection.Output, dbType: DbType.Int32);
                string sql = "exec [dbo].[procFenYe] @pageSize,@keyName,@tableName,@currentPage,@where,@rows out";
                fenYe.List = await con.QueryAsync<Engage_resume>(sql, dynamic);
                fenYe.Rows = dynamic.Get<int>("rows");
                return fenYe;
            }
        }
    }
}
