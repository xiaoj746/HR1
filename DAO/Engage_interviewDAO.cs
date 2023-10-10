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
    public class Engage_interviewDAO
    {
        string conStr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";
        public async Task<int> msinsert(Engage_resume e)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sql = $@"insert into engage_interview(human_name,interview_amount,human_major_kind_id,human_major_kind_name,human_major_id,human_major_name,
image_degree,native_language_degree,foreign_language_degree,response_speed_degree,
EQ_degree,IQ_degree,multi_quality_degree,register,checker,registe_time,check_time,
resume_id,result,interview_comment,check_comment,interview_status,check_status)
                                values('{e.Human_name}','{e.Interview_amount}','{e.Human_major_kind_id}','{e.Human_major_kind_name}','{e.Human_major_id}','{e.Human_major_name}','{e.Image_degree}','{e.Native_language_degree}','{e.Foreign_language_degree}','{e.Response_speed_degree}',
'{e.EQ_degree}','{e.IQ_degree}','{e.Multi_quality_degree}','{e.Register}','{e.Checker}','{e.Regist_time1}','{e.Check_time}','{e.Res_id}','{e.Result}','{e.Interview_comment}','{e.Check_comment}','{e.Interview_status}','{e.Check_status}')";
                return await con.ExecuteAsync(sql);
            }
        }
        public async Task<int> msdelete(Engage_resume e)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sql = $"delete from engage_interview where ein_id={e.Ein_id}";
                return await con.ExecuteAsync(sql);
            }
        }
        public async Task<int> msupdate(Engage_resume e)
        {
            using(SqlConnection con = new SqlConnection(conStr))
            {
                string sql = $"update engage_interview set result='{e.Result}' where ein_id='{e.Ein_id}'";
                return await con.ExecuteAsync(sql);
            }
        }
        public async Task<int> sqly(Engage_resume e)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sql = $"update Engage_resume set pass_checkComment='{e.Pass_checkcomment}' where res_id='{e.Res_id}'";
                return await con.ExecuteAsync(sql);
            }
        }
        public async Task<int> sply(Engage_resume e)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sql = $"update Engage_resume set Pass_passcomment='{e.Pass_passcomment}' where res_id='{e.Res_id}'";
                return await con.ExecuteAsync(sql);
            }
        }

    }
}
