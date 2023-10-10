using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Config_major
    {
        public int Mak_id { get; set; }
        [DisplayName("职位分类编号")]
        public string Major_kind_id { get; set; }
        [DisplayName("职位分类名称")]
        public string Major_kind_name { get; set; }
        [DisplayName("职位编号")]
        public string Major_id { get; set; }
        [DisplayName("职位名称")]
        public string Major_name { get; set; }
        public int Test_amount { get; set; }

    }
}
