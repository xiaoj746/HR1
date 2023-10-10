using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class salary_standard
    {
        //[ssd_id], [standard_id], [standard_name], [designer], [register], [checker], [changer], [regist_time], [check_time],
        //[change_time], [salary_sum], [check_status], [change_status], [check_comment], [remark]
        public int ssd_id { get; set; }
        public string  standard_id { get; set; }
        public string standard_name { get; set; }
        public string designer { get; set; }
        public string register { get; set; }
        public string checker { get; set; }
        public string changer { get; set; }
        public DateTime regist_time { get; set; }
        public DateTime check_time { get; set; }
        public DateTime change_time { get; set; }
        public double salary_sum { get; set; }
        public int check_status { get; set; }
        public int change_status { get; set; }
        public string check_comment { get; set; }
        public string remark { get; set; }
    }
}
