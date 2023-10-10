using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Engage_major_release
    {
        public int Mre_id { get; set; }
        public string First_kind_id { get; set; }
        public string First_kind_name { get; set; }
        public string Second_kind_id { get; set; }
        public string Second_kind_name { get; set; }
        public string Third_kind_id { get; set; }
        public string Third_kind_name { get; set; }
        [DisplayName("职位分类编号")]
        public string Major_kind_id { get; set; }
        [DisplayName("职位分类名称")]
        public string Major_kind_name { get; set; }
        [DisplayName("职位编号")]
        public string Major_id { get; set; }
        [DisplayName("职位名称")]
        public string Major_name { get; set; }
        public int Human_amount { get; set; }
        public string Engage_type { get; set; }
        public DateTime Deadline { get; set; }
        public string Register { get; set; }
        public string Changer { get; set; }

        public DateTime Regist_time { get; set; }
        public DateTime Change_time { get; set; }
        public string Major_describe { get; set; }
        public string Engage_required { get; set; }
        public string Human_major_kind_id { get; set; }
        public string Human_major_kind_name { get; set; }
        public string Human_major_id { get; set; }
        public string Human_major_name { get; set; }
    }
}
