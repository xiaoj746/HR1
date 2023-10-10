using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class salary_standard_details_S
    {
        public int sdt_id { get; set; }
        public string standard_id { get; set; }
        public string standard_name { get; set; }
        public int item_id { get; set; }
        public string item_name { get; set; }
       // public float salary { get; set; }
        public string designer { get; set; }
        public string register { get; set; }
        public string remark { get; set; }
        public float salary_sum { get; set; }
        public float[] salary { get; set; }
        public List<config_public_charXJ> count { get; set; }   //循环次数
    }
}
