using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Config_public_char
    {
        public int Pbc_id { get; set; }
        [DisplayName("属性种类")]
        public string Attribute_kind { get; set; }
        [DisplayName("职位分类名称")]
        public string Attribute_name { get; set; }
    }
}
